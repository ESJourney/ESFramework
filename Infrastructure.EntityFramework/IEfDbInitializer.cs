using Infrastructure.BackupManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityFramework
{
    public interface IEfDbInitializer : IRelationalDbBackupCreator
    {
        void EnsureDatabaseExistsAndItsUpdated();
        void DropAndCreateDb();
    }
}
