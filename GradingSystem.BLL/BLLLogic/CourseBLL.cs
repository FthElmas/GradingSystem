using AutoMapper;
using FluentValidation.Results;
using GradingSystem.BLL.Common.Mapper;
using GradingSystem.BLL.Handler;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.DTOs.Course;
using GradingSystem.DAL.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Services
{
    public class CourseBLL : ICourseDAL
    {
        private CourseDAL courseDAL;
        private ValidateCourse validation;
        public CourseBLL()
        {
            courseDAL = new CourseDAL();
            validation = new ValidateCourse();
        }

        public List<CourseSelectDTO> GetAllCourse()
        {
            List<CourseSelectDTO> courses = new List<CourseSelectDTO>();
            var data = courseDAL.GetAllCourse();
            MyMapper<Course, CourseSelectDTO> mapper = new MyMapper<Course, CourseSelectDTO>();
            data.ForEach(a => courses.Add(mapper.Map(a)));
            return courses;
        }

        public CourseAddDTO AddCourse(CourseAddDTO course, Guid teacherID)
        {
            MyMapper<CourseAddDTO, Course> mapper = new MyMapper<CourseAddDTO, Course>();
            var mappedData = mapper.Map(course);
            ValidationResult result = validation.Validate(mappedData);
            if(result.IsValid)
            {
                var data = courseDAL.AddCourse(mappedData, teacherID);
                MyMapper<Course, CourseAddDTO> mapper1 = new MyMapper<Course, CourseAddDTO>();
                return mapper1.Map(data);
            }
            else
            {
                throw new Exception();
            }
            
        }

        public bool UpdateCourse(CourseUpdateDTO course)
        {
            MyMapper<CourseUpdateDTO, Course> mapper = new MyMapper<CourseUpdateDTO, Course>();
            var data = mapper.Map(course);
            ValidationResult result = validation.Validate(data);
            if (result.IsValid)
            {
                courseDAL.UpdateCourse(data);
                return true;
            }
            else
                return false;
        }

        public bool CheckCourse(string course)
        {
            if (course != null)
                return courseDAL.CheckCourse(course);

            return false;
        }

        public void SoftDeleteCourse(CourseSelectDTO course)
        {
            MyMapper<CourseSelectDTO, Course> mapper = new MyMapper<CourseSelectDTO, Course>();
            courseDAL.SoftDeleteCourse(mapper.Map(course));
        }
    }
}
