using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPaymentSystem.Application.UseCases.Students.Commands.UpdateStudent
{
    public  class UpdateCommandValidation : AbstractValidator<UpdateStudentCommand>
    {
        public UpdateCommandValidation() 
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage(" Name is required . ");
        }


    }
}
