using StudentPaymentSystem.Application.UseCases.Payments.Models;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.UseCases.Students.Models;

public class GetStudentDtoWithPayments : GetAllStudentDto
{
    public ICollection<PaymentDto> Payments { get; set; }
}
