using StudentPaymentSystem.Domein.Common.BaseEntities;

namespace StudentPaymentSystem.Domein.Entities;

public  class Teacher : BaseAuditableEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Course> Courses { get; set; }
}
