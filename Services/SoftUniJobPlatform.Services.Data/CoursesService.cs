using SoftUniJobPlatform.Data.Models.Enum;

namespace SoftUniJobPlatform.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SoftUniJobPlatform.Data.Common.Repositories;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class CoursesService : ICoursesService
    {
        private readonly IDeletableEntityRepository<Course> courseRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public CoursesService(
            IDeletableEntityRepository<Course> courseRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.courseRepository = courseRepository;
            this.userRepository = userRepository;
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

        public IEnumerable<T> GetAllByUserId<T>(string id)
        {

            IQueryable<Course> query =
                this.courseRepository.All().Where(x => x.ApplicationUserId == id);

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

        public void Create(string userId, string title, string description, int categoryId, string imageUrl)
        {
            var user = this.userRepository.All().FirstOrDefault(x => x.Id == userId);
            var course = new Course
            {
                ApplicationUserId = user.Id,
                CategoryId = categoryId,
                CourseProgress = CourseProgressType.Finished,
                Title = title,
                Description = description,
                ImageUrl = imageUrl ?? "https://arbikas.com/pub/media/brands/asi.jpg",
            };
            user.Courses.Add(course);
            this.userRepository.SaveChangesAsync().GetAwaiter().GetResult();
            this.courseRepository.AddAsync(course).GetAwaiter().GetResult();
            this.courseRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(int id, string title, string description, string imageUrl)
        {
            throw new NotImplementedException();
        }

        public void AddCourseAsync(int courseId, string userId, double rate, int credit)
        {
            var user = this.userRepository.All()
                .FirstOrDefault(x => x.Id == userId);
            var course = this.GetById(courseId);

            this.courseRepository.SaveChangesAsync();

            if (user == null || course == null)
            {
                throw new ArgumentNullException("Not correct input");
            }

            if (!user.Courses.Contains(course))
            {
                user.Courses.Add(course);
            }

            this.userRepository.SaveChangesAsync();

        }
    }
}
