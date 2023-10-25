﻿using Microsoft.EntityFrameworkCore;

namespace LogAPI.Models
{
    public class LogContext : DbContext
    {
        public LogContext(DbContextOptions<LogContext> options) : base(options)
        {
        }
        public DbSet<Log>? Logs { get; set; }

    }
}
