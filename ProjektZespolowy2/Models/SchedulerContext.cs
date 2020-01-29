using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjektZespolowy2.Models
{
        public partial class SchedulerContext : DbContext
        {
            public SchedulerContext() : base("name=DefaultConnection") { }
            public virtual DbSet<Event> Events { get; set; }
        }
}