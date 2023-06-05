using System.ComponentModel.DataAnnotations.Schema;
using StudentPaymentSystem.Domein.Common.BaseEntities;

namespace StudentPaymentSystem.Domein.Entities;

public class Payment : BaseAuditableEntity
{
    public Guid StudentID { get; set; }

    public DateTime PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public Guid InvoiceId { get; set; }

    [ForeignKey("InvoiceId")]
    public Invoice Invoice { get; set; }

    [ForeignKey("StudentID")]
    public virtual Student Student { get; set; }
}
