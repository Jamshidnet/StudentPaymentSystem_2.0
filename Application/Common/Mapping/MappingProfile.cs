using AutoMapper;
using StudentPaymentSystem.Application.UseCases.Courses.Models;
using StudentPaymentSystem.Application.UseCases.Invoices.Models;
using StudentPaymentSystem.Application.UseCases.Payments.Models;
using StudentPaymentSystem.Application.UseCases.Students.Models;
using StudentPaymentSystem.Application.UseCases.Teachers.Models;
using StudentPaymentSystem.Domein.Entities;

namespace StudentPaymentSystem.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Course, GetallCourseDto>().ReverseMap();
        CreateMap<Invoice,InvoiceDto>();
        CreateMap<Invoice,GetAllInvoiceDto>().ReverseMap();
        CreateMap<Payment, PaymentDto>().ReverseMap();
        CreateMap<Student, GetAllStudentDto>().ReverseMap();
        CreateMap<Teacher, TeacherDto>().ReverseMap();
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<Student, StudentDto>().ReverseMap();
    }
}

