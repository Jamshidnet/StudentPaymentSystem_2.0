using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPaymentSystem.Application.UseCases.Courses.Queries.GetCourseQuery
{
    public  class GetCourseQueryValidation :AbstractValidator<GetCourseQuery>
    {
        public GetCourseQueryValidation()
        {
            RuleFor(x => x.Id).NotEmpty().NotEqual((Guid)default).WithMessage(" invalid Guid Format. ");
        }
    }
}
