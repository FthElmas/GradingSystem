using GradingSystem.Core.Entity;
using GradingSystem.DAL.DTOs.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.Abstract.Interface
{
    public interface ICourseDAL
    {
        List<CourseSelectDTO> GetAllCourse();
        CourseAddDTO AddCourse(CourseAddDTO course, Guid teacherID);
        bool UpdateCourse(CourseUpdateDTO course);
        bool CheckCourse(string courseName);
        void SoftDeleteCourse(CourseSelectDTO course);
    }
}
