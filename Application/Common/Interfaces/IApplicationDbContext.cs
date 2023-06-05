using Microsoft.EntityFrameworkCore;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.Common.Interfaces;

public  interface IApplicationDbContext
{
    DbSet<T> Set<T>() where T : class;
    DbSet<Student> Students { get; }
    DbSet<Course> Courses { get; }
    DbSet<Payment> Payments { get; }
    DbSet<Invoice> Invoices { get; }
    DbSet<Teacher> Teachers { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    ValueTask<T> AddAsync<T>(T @object);
    ValueTask<T?> GetAsync<T>(params object[] objectId) where T : class;
    IQueryable<T> GetAll<T>() where T : class;
    ValueTask<T> UpdateAsync<T>(T @object);
    ValueTask<T> DeleteAsync<T>(T @object);
}
