using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPaymentSystem.Application.UseCases.Courses.Commands.CreateCourse;

public  class CreateCourseCommandValidation : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidation() {

        RuleFor(x => x.Name).NotEmpty().MaximumLength(50).WithMessage(" Invalid Course Name. ");

        RuleFor(x => x.Fee).GreaterThanOrEqualTo(0).WithMessage(" Invalid fee input. ");

        RuleFor(x => x.TeacherId).NotEqual((Guid)default).WithMessage(" invalid Guid format. ");
    }
}
