using Microsoft.EntityFrameworkCore;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Domein.Entities;
using StudentPaymentSystem.Infrustructure.Persistence.Interceptors;
using System.Reflection;

namespace StudentPaymentSystem.Infrustructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Invoice> Invoices { get; set; }


        private readonly AuditableEntitySaveChangesInterceptor _interceptor;


        private readonly DbContextOptions<ApplicationDbContext>? options;
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>? options,
            AuditableEntitySaveChangesInterceptor interceptor) : base(options)
        {
            _interceptor=interceptor;
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly());

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entity.Name).Property(typeof(DateTimeOffset), "CreatedDate")
                    .HasColumnType("timestamptz");

                modelBuilder.Entity(entity.Name).Property(typeof(DateTimeOffset), "UpdatedDate")
                    .HasColumnType("timestamptz");
            }

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_interceptor);
        }
    }

}

