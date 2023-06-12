using StudentPaymentSystem.Application.UseCases.Students.Models;

namespace StudentPaymentSystem.Application.UseCases.Courses.Models;

public  class GetallCourseDto : CourseDto
{
    public ICollection<StudentDto>? Students { get; set; }

}
