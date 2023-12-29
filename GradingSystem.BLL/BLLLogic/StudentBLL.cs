using FluentValidation.Results;
using GradingSystem.BLL.Common.Mapper;
using GradingSystem.BLL.Handler.Student;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.DTOs.Student;
using GradingSystem.DAL.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Services
{
    public class StudentBLL : IStudentDAL
    {
        StudentDAL studentDAL;
        ValidateStudent validations;
        public StudentBLL()
        {
            studentDAL = new StudentDAL();
            validations = new ValidateStudent();
        }

        public bool AddStudentWithStudentTeacher(StudentAddDTO student, Guid teacherID)
        {
            MyMapper<StudentAddDTO, Student> mapper = new MyMapper<StudentAddDTO, Student>();
            ValidationResult result = validations.Validate(student);
            if(result.IsValid)
            {
                studentDAL.AddStudentWithStudentTeacher(mapper.Map(student), teacherID);
                return true;
            }
            else
            {

                throw new Exception();
            }
        }

        public bool CheckStudent(StudentAddDTO student)
        {
            MyMapper<StudentAddDTO, Student> mapper = new MyMapper<StudentAddDTO, Student>();
            return studentDAL.CheckStudent(mapper.Map(student));
        }

        public List<StudentSelectDTO> GetAllStudent(Guid teacherID, int customerCourseID)
        {
            MyMapper<Student, StudentSelectDTO> mapper = new MyMapper<Student, StudentSelectDTO>();
            var data = studentDAL.GetAllStudent(teacherID, customerCourseID);
            List<StudentSelectDTO> students = new List<StudentSelectDTO>();
            data.ForEach(a => students.Add(mapper.Map(a)));
            return students;
        }

        public List<StudentSelectDTO> GetAllStudentOfTeacher(Guid teacherID)
        {
            MyMapper<Student, StudentSelectDTO> mapper = new MyMapper<Student, StudentSelectDTO>();
            var data = studentDAL.GetAllStudentOfTeacher(teacherID);
            List<StudentSelectDTO> students = new List<StudentSelectDTO>();
            data.ForEach(a => students.Add(mapper.Map(a)));
            return students;
        }

        public bool SoftDeleteStudent(StudentSelectDTO student)
        {
            MyMapper<StudentSelectDTO, Student> mapper = new MyMapper<StudentSelectDTO, Student>();
            return studentDAL.SoftDeleteStudent(student);
        }

        public bool UpdateStudent(StudentUpdateDTO student)
        {
            MyMapper<StudentUpdateDTO, Student> mapper = new MyMapper<StudentUpdateDTO, Student>();
            return studentDAL.UpdateStudent(mapper.Map(student));
        }
    }
}
