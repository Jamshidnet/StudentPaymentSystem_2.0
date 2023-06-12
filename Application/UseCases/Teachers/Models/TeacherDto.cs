using StudentPaymentSystem.Application.UseCases.Courses.Models;
using StudentPaymentSystem.Domein.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPaymentSystem.Application.UseCases.Teachers.Models
{
    public  class TeacherDto 
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<CourseDto> Courses { get; set; }
    }
}
