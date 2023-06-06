using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPaymentSystem.Application.UseCases.Payments.Models
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public Guid StudentID { get; set; }

        public DateTime PaymentDate { get; set; }

        public decimal Amount { get; set; }

        public Guid InvoiceId { get; set; }
    }
}
