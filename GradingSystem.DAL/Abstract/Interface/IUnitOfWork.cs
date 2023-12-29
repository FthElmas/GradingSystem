using GradingSystem.Core.Entity;
using GradingSystem.DAL.DTOs.Course;
using GradingSystem.DAL.DTOs.Customer;
using GradingSystem.DAL.DTOs.CustomerCourse;
using GradingSystem.DAL.DTOs.Project;
using GradingSystem.DAL.DTOs.Quiz;
using GradingSystem.DAL.DTOs.Student;
using GradingSystem.DAL.DTOs.StudentMark;
using GradingSystem.DAL.DTOs.StudentProject;
using GradingSystem.DAL.DTOs.StudentQuiz;
using GradingSystem.DAL.DTOs.StudentReport;
using GradingSystem.DAL.DTOs.StudentReportMark;
using GradingSystem.DAL.DTOs.StudentTeacher;
using GradingSystem.DAL.DTOs.Teacher;
using GradingSystem.DAL.DTOs.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.Abstract.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Course> CourseRepository { get; }
        IGenericRepository<Customer> CustomerRepository { get; }
        IGenericRepository<CustomerCourse> CustomerCourseRepository { get; }
        IGenericRepository<Project> ProjectRepository { get; }
        IGenericRepository<Quiz> QuizRepository { get; }
        IGenericRepository<Student> StudentRepository { get; }
        IGenericRepository<StudentMark> StudentMarkRepository { get; }
        IGenericRepository<StudentProject> StudentProjectRepository { get; }
        IGenericRepository<StudentQuiz> StudentQuizRepository { get; }
        IGenericRepository<StudentReport> StudentReportRepository { get; }
        IGenericRepository<StudentReportMark> StudentReportMarkRepository { get; }
        IGenericRepository<StudentTeacher> StudentTeacherRepository { get; }
        IGenericRepository<Teacher> TeacherRepository { get; }
        IDbConnection Conn { get; }
        IDbTransaction Transaction { get; }
        void Commit();
        void Rollback();
        void Dispose();
        void BeginTransaction();
    }
}
