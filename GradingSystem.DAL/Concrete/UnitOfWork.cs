using GradingSystem.Core.Entity;
using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.Concrete.DapperGenericRepository.Repository;
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
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;

        public UnitOfWork()
        {
            _connection = new SqlConnection("server = .; Database = GradingSystemDB; Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True");
            _connection.Open();
            
            
        }
        public IDbConnection Conn {get { return _connection; } }
        public IDbTransaction Transaction { get { return _transaction; } }
        public IGenericRepository<User> UserRepository { get {return new TransacGenericRepository<User>(_connection, _transaction); } private set { } }
        public IGenericRepository<Course> CourseRepository { get {return new TransacGenericRepository<Course>(_connection, _transaction); } private set { } }
        public IGenericRepository<Customer> CustomerRepository { get {return new TransacGenericRepository<Customer>(_connection, _transaction); } private set { } }
        public IGenericRepository<CustomerCourse> CustomerCourseRepository { get { return new TransacGenericRepository<CustomerCourse>(_connection, _transaction); } private set { } }
        public IGenericRepository<Project> ProjectRepository { get { return new TransacGenericRepository<Project>(_connection, _transaction); } private set { } }
        public IGenericRepository<Quiz> QuizRepository { get { return new TransacGenericRepository<Quiz>(_connection, _transaction); } private set { } }

        public IGenericRepository<Student> StudentRepository { get { return new TransacGenericRepository<Student>(_connection, _transaction); } private set { } }

        public IGenericRepository<StudentMark> StudentMarkRepository { get { return new TransacGenericRepository<StudentMark>(_connection, _transaction); } private set { } }

        public IGenericRepository<StudentProject> StudentProjectRepository { get { return new TransacGenericRepository<StudentProject>(_connection, _transaction); } private set { } }

        public IGenericRepository<StudentQuiz> StudentQuizRepository { get { return new TransacGenericRepository<StudentQuiz>(_connection, _transaction); } private set { } }

        public IGenericRepository<StudentReport> StudentReportRepository { get { return new TransacGenericRepository<StudentReport>(_connection, _transaction); } private set { } }

        public IGenericRepository<StudentReportMark> StudentReportMarkRepository { get { return new TransacGenericRepository<StudentReportMark>(_connection, _transaction); } private set { } }

        public IGenericRepository<StudentTeacher> StudentTeacherRepository { get { return new TransacGenericRepository<StudentTeacher>(_connection, _transaction); } private set { } }

        public IGenericRepository<Teacher> TeacherRepository { get { return new TransacGenericRepository<Teacher>(_connection, _transaction); } private set { } }

        

        public void Commit()
        {
            _transaction?.Commit();
            _transaction = null;
        }
        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction = null;
        }
        public void BeginTransaction()
        {
            if (_transaction == null)
            {
                _transaction = _connection.BeginTransaction();
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }
}
