using StudentPaymentSystem.Application.Common.Interfaces;

namespace StudentPaymentSystem.Infrustructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}
