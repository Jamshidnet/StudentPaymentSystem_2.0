using StudentPaymentSystem.Domein.Common.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentPaymentSystem.Domein.Entities;

public class Course : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    [ForeignKey("TeacherId")]
    public Teacher  Teacher { get; set; }
    public Guid TeacherId { get; set; }
    public decimal Fee { get; set; }

    public ICollection<Student> Students { get; set; }
}
