using System.ComponentModel.DataAnnotations.Schema;
using StudentPaymentSystem.Domein.Common.BaseEntities;

namespace StudentPaymentSystem.Domein.Entities;

public class Invoice : BaseAuditableEntity
{
    public Guid CourseID { get; set; }

    public DateTime IssueDate { get; set; }
     
    public decimal TotalAmount { get; set; }

    public ICollection<Payment> Payments { get; set; }

    
    [ForeignKey("CourseID")]
    public virtual Course Course { get; set; }

}
