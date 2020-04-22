namespace SoftUniJobPlatform.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using SoftUniJobPlatform.Data.Common.Repositories;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class CoursesService : ICoursesService
    {
        private readonly IDeletableEntityRepository<Course> courseRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IRepository<StudentCourse> studentCourseRepository;

        public CoursesService(IDeletableEntityRepository<Course> courseRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IRepository<StudentCourse> studentCourseRepository)
        {
            this.courseRepository = courseRepository;
            this.userRepository = userRepository;
            this.studentCourseRepository = studentCourseRepository;
        }

        public IEnumerable<T> GetAll<T>(int? take = null, int skip = 0)
        {
            IQueryable<Course> query =
                this.courseRepository.All().OrderByDescending(x => x.CreatedOn);

            if (!query.Any())
            {
                throw new ArgumentNullException("No courses");
            }

            return query.To<T>().ToList();
        }

        public IQueryable<Course> GetCategories()
        {
            throw new NotImplementedException();
        }

        public T GetByName<T>(string name)
        {
            throw new NotImplementedException();
        }

        public T GetById<T>(int id)
        {
            var courses = this.courseRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            if (courses == null)
            {
                throw new ArgumentNullException();
            }

            return courses;
        }

        public Course GetById(int id)
        {
            return this.courseRepository.All().FirstOrDefault(x => x.Id == id);
        }

        public Task CreateAsync(string title, string description, string imageUrl)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(int id, string title, string description, string imageUrl)
        {
            throw new NotImplementedException();
        }

        public async Task AddCourseAsync(int courseId, string userId, double rate, int credit)
        {
            var user = this.userRepository.All()
                .FirstOrDefault(x => x.Id == userId);
            var course = this.GetById(courseId);
            course.Credit = credit;
            course.Rate = rate;
            await this.courseRepository.SaveChangesAsync();

            if (user == null || course == null)
            {
                throw new ArgumentNullException("Not correct input");
            }

            var exist = this.studentCourseRepository.All().FirstOrDefault(x => x.CourseId == courseId && x.ApplicationUserId == userId);
            if (exist == null)
            {
                var studentCourse = new StudentCourse
                {
                    ApplicationUserId = userId,
                    ApplicationUser = user,
                    CourseId = courseId,
                    Course = course,
                };
                course.StudentCourses.Add(studentCourse);
                user.Courses.Add(studentCourse);
                await this.courseRepository.SaveChangesAsync();
                await this.userRepository.SaveChangesAsync();

            }
            else
            {
                throw new ArgumentNullException("Your candidate in this job offer");
            }
        }
    }
}
