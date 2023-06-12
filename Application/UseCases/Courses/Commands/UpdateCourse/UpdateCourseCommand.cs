using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentPaymentSystem.Application.Common.Exceptions;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.UseCases.Courses.Models;
using StudentPaymentSystem.Domein.Entities;
using System.Collections.Immutable;

namespace StudentPaymentSystem.Application.UseCases.Courses.Commands.UpdateCourse;

public  record UpdateCourseCommand : IRequest<GetallCourseDto>
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid TeacherId { get; set; }

    public decimal Fee { get; set; }

    public ICollection<Guid> StudentIds { get; set; }
}

public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, GetallCourseDto>
{

    IApplicationDbContext _dbContext;

    IMapper _mapper;

    public UpdateCourseCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GetallCourseDto> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
       Course course= await FilterIfCourseExsists( request.Id);
        IEnumerable<Student> students=  FilterifStudentIdsAreAvialible(request.StudentIds);

        course.TeacherId = request.TeacherId;
        course.Description = request.Description;
        course.Name = request.Name;
        course.Fee = request.Fee;
        course.Students = students.ToList();
          _dbContext.Courses.Update(course);
         await  _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<GetallCourseDto>(course);
    }
     
    private  IEnumerable<Student> FilterifStudentIdsAreAvialible(ICollection<Guid> studentIds)
    {
        foreach (var Id in studentIds)
            yield return  _dbContext.Students.Find(Id)
                ?? throw new NotFoundException($" there is no student with this {Id} id. ");
    }
     
    private async  Task<Course> FilterIfCourseExsists( Guid id)
    {
        Course? course = await _dbContext.Courses.Include("Students")
            .FirstOrDefaultAsync(x=>x.Id==id);

        return course
            ?? throw new  NotFoundException(
                " there is no course with this id. ");
    }
}
