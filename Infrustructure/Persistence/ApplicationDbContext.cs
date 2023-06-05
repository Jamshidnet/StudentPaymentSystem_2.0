using Microsoft.EntityFrameworkCore;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Infrustructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>? options) : base(options)
        {

        }

        private readonly DbContextOptions<ApplicationDbContext>? options;

        public async ValueTask<T> AddAsync<T>(T @object)
        {
            var context = new ApplicationDbContext(options);
            if (@object is not null)
                context.Entry(@object).State = EntityState.Added;
            await context.SaveChangesAsync();
            return @object;
        }



        public async ValueTask<T?> GetAsync<T>(params object[] objectIds) where T : class
        {
            var context = new ApplicationDbContext(options);
            return await context.FindAsync<T>(objectIds);
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            var context = new ApplicationDbContext(options);
            return context.Set<T>();
        }

        public async ValueTask<T> UpdateAsync<T>(T @object)
        {
            var context = new ApplicationDbContext(options);
            if (@object is not null)
                context.Entry(@object).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return @object;
        }

        public async ValueTask<T> DeleteAsync<T>(T @object)
        {
            var context = new ApplicationDbContext(options);
            if (@object is not null)
                context.Entry(@object).State = EntityState.Deleted;
            await context.SaveChangesAsync();
            return @object;
        }
    }

}

