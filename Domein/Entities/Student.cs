using StudentPaymentSystem.Domein.Common.BaseEntities;

namespace StudentPaymentSystem.Domein.Entities;

public class Student : BaseAuditableEntity
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }
    public ICollection<Course> Courses { get; set; }

    public ICollection<Payment> Payments { get; set; }
}
