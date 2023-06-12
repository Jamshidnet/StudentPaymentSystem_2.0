namespace StudentPaymentSystem.Application.UseCases.Invoices.Models;

public class InvoiceDto
{
    public Guid Id { get; set; }
    public Guid CourseID { get; set; }

    public DateTime IssueDate { get; set; }

    public decimal TotalAmount { get; set; }
}
