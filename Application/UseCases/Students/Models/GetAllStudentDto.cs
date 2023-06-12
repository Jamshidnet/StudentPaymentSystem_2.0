using StudentPaymentSystem.Application.UseCases.Courses.Models;

namespace StudentPaymentSystem.Application.UseCases.Students.Models;

public class GetAllStudentDto : StudentDto
{
    public ICollection<CourseDto>? Courses { get; set; }
}
