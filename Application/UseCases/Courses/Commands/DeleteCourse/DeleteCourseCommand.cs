using AutoMapper;
using MediatR;
using StudentPaymentSystem.Application.Common.Exceptions;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Application.UseCases.Courses.Models;
using StudentPaymentSystem.Domein.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPaymentSystem.Application.UseCases.Courses.Commands.DeleteCourse
{
    public record DeleteCourseCommand(Guid Id) : IRequest<CourseDto>;

    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, CourseDto>
    {
        private IApplicationDbContext _dbContext;
        private IMapper _mapper;

        public DeleteCourseCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CourseDto> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            Course course = FilterIfCourseExsists(request.Id);

            _dbContext.Courses.Remove(course);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CourseDto>(course);
        }

        private Course FilterIfCourseExsists(Guid id)
        {
            Course? course = _dbContext.Courses.FirstOrDefault(c=>c.Id==id);

            if(course is  null) 
            {
                throw new NotFoundException(" There is no course with id. ");
            }

            return course;
        }
    }
}
