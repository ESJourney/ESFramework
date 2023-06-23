using Infrastructure.BackupManagement;
using Infrastructure.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityFramework
{
    public class EfDbInitializer<T> : IEfDbInitializer where T : DbContext
    {
        // Campos
        private readonly ILogLite log = LogManager.GetLoggerFor<EfDbInitializer<T>>();
        private readonly TimeSpan backupAndRestoreTimeout = TimeSpan.FromHours(3);
        // Propiedades
        public Func<T> ResolveReadContext { get; } = null!;
        public string VirtualDbName { get; } = null!;
        public string RealDbName { get; } = null!;
        public string Prefix { get; } = null!;
        public RelationalDbType RelationalDbType { get; }

        public async Task CreateBackupToDestination(string destinationPath)
        {
            using (var context = this.ResolveReadContext())
            {
                context.Database.SetCommandTimeout(this.backupAndRestoreTimeout);
                await context.Database.ExecuteSqlRawAsync($@"
                    USE [master]
                    BACKUP DATABASE [{this.RealDbName}] TO  DISK = N'{destinationPath}\{this.VirtualDbName}.bak' WITH NOFORMAT, NOINIT,  
                    NAME = N'{this.VirtualDbName}', NOSKIP, REWIND, NOUNLOAD,  STATS = 10
                ");
            }
        }

        public void DropAndCreateDb()
        {
            var contextName = typeof(T).Name;
            this.log.Warning($"Checking the sql database for {contextName}. If exists it will be DROPED and then created again. I sure hope you know what you are doing...");
            using (var context = this.ResolveReadContext.Invoke())
            {
                if (((RelationalDatabaseCreator)context.GetService<IDatabaseCreator>()).Exists())
                {
                    this.log.Info($"The sql database for {contextName} was found. Deleting the current db...");
                    context.Database.EnsureDeleted();
                    this.log.Info($"The sql database for {contextName} was successfully deleted!");
                }

                this.EnsureDatabaseExistsAndItsUpdated();
            }
        }

        public void EnsureDatabaseExistsAndItsUpdated()
        {
            var contextName = typeof(T).Name;
            this.log.Info($"Checking the sql database for {contextName}. If not exists a new one will be created.");
            using (var context = this.ResolveReadContext.Invoke())
            {
                if (!((RelationalDatabaseCreator)context.GetService<IDatabaseCreator>()).Exists())
                {
                    this.log.Info($"The sql database for {contextName} was not found. Creating a new one...");
                    this.ApplyMigrationsIfApplicable(contextName, context);
                    context.Database.EnsureCreated();
                    this.log.Info($"The database for {contextName} was created successfully!");
                }
                else
                {
                    this.log.Info($"The database for {contextName} was found!");
                    this.ApplyMigrationsIfApplicable(contextName, context);
                    this.log.Info($"Sql database for {contextName} is ready");
                }
            }
        }

        public Task RestoreBackupToDestination(string sourcePath)
        {
            throw new NotImplementedException();
        }

        private void ApplyMigrationsIfApplicable(string contextName, DbContext context)
        {
            this.log.Info($"Getting pending migrations for {contextName}...");
            var migrations = context.Database.GetPendingMigrations();
            var count = migrations.Count();
            this.log.Info($"{count} pending migrations found in {contextName}");
            migrations.ToList().ForEach(x => this.log.Info(x));
            if (count > 0)
            {
                this.log.Info($"Applying pending migrations to {contextName}...");
                context.Database.Migrate();
                this.log.Info($"Migrations applied successfully to {contextName}!");
            }
        }

    }


}
