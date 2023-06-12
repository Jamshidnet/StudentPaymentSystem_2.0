using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPaymentSystem.Application.UseCases.Courses.Models
{
    public  class CourseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid TeacherId { get; set; }

        public decimal Fee { get; set; }
    }
}
