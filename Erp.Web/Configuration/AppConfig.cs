﻿using Infrastructure.Configuration.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Web.Configuration
{
    public class AppConfig
    {
        public ConnectionStrings ConnectionStrings { get; set; } = null!;
        public Settings Settings { get; set; } = null!;
    }
}
