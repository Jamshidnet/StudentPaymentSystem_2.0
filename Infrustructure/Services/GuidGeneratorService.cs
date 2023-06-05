using StudentPaymentSystem.Application.Common.Interfaces;

namespace StudentPaymentSystem.Infrustructure.Services;

public class GuidGeneratorService : IGuidGenerator
{
    public Guid Guid => Guid.NewGuid();
}
