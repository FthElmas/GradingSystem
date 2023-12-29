using Dapper;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.Concrete;
using GradingSystem.DAL.Concrete.DapperGenericRepository.Repository;
using GradingSystem.DAL.DTOs.Project;
using GradingSystem.DAL.DTOs.Quiz;
using GradingSystem.DAL.DTOs.Student;
using GradingSystem.DAL.DTOs.StudentMark;
using GradingSystem.DAL.DTOs.StudentProject;
using GradingSystem.DAL.DTOs.StudentQuiz;
using GradingSystem.DAL.DTOs.StudentReport;
using GradingSystem.DAL.Helper;
using GradingSystem.DTO.DTOs.Complex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.Logic
{
    public class QuizDAL
    {
        private readonly ConnectionHelper _connectionHelper;
        public QuizDAL()
        {
            _connectionHelper = new ConnectionHelper();
        }
        public bool AddQuiz(Quiz quiz)
        {
            try
            {
                GenericRepository<Quiz> repo = new GenericRepository<Quiz>();
                repo.Add(quiz);
                return true;
            }
            catch
            {
                return false;
            }
        }
        //studentquiz tablosuna transac ile insert at burda
        



        public bool CheckQuiz(Quiz quiz)
        {
            using var conn = _connectionHelper.CreateConnection();
            string query = "select * from [Quiz] where QuizName = @QuizName";
            if (conn.QueryFirstOrDefault(query, new {QuizName = quiz.QuizName }))
            {
                return true;
            }
            return false;
        }
        public List<StudentQuiz> GetAllQuizInSelectedWeek(int selectedWeek, Guid studentID)
        {
            try
            {
                using var conn = _connectionHelper.CreateConnection();
                string query = "SELECT DATEPART(WEEK, StudentQuiz.CreatedDate) AS WeekNo, StudentQuiz.Description, StudentMark.Mark ,Quiz.QuizDescription as Question ,COUNT(*) AS EntryNo\r\nFROM StudentQuiz\r\nJOIN Student ON Student.StudentID = StudentQuiz.StudentID\r\nJOIN CustomerCourse on Student.CustomerCourseID = CustomerCourse.ID\r\nJOIN Quiz ON Quiz.QuizID = StudentQuiz.QuizID\r\nJOIN StudentMark ON StudentQuiz.StudentMarkID = StudentMark.StudentMarkID\r\nWHERE StudentQuiz.CreatedDate BETWEEN CustomerCourse.CourseStartDate AND CustomerCourse.CourseEndDate\r\nGROUP BY DATEPART(WEEK, StudentQuiz.CreatedDate), StudentQuiz.Description, StudentMark.Mark ,Quiz.QuizDescription\r\nHAVING DATEPART(WEEK, StudentQuiz.CreatedDate) = @SelectedWeek\r\nORDER BY DATEPART(WEEK, StudentQuiz.CreatedDate);";
                return conn.Query<StudentQuiz>(query, new { SelectedWeek = selectedWeek }).ToList();
            }
            catch
            {
                throw new Exception();
            }
        }

        public List<StudentQuiz> GetAllQuizToday()
        {
            try
            {
                using var conn = _connectionHelper.CreateConnection();
                string query = "SELECT\r\n    StudentQuiz.CreatedDate,\r\n    StudentQuiz.Description,\r\n    StudentMark.Mark,\r\n    Quiz.QuizDescription AS Question\r\nFROM\r\n    StudentQuiz\r\nJOIN\r\n    Student ON Student.StudentID = StudentQuiz.StudentID\r\nJOIN\r\n    CustomerCourse ON Student.CustomerCourseID = CustomerCourse.ID\r\nJOIN\r\n    Quiz ON Quiz.QuizID = StudentQuiz.QuizID\r\nJOIN\r\n    StudentMark ON StudentQuiz.StudentMarkID = StudentMark.StudentMarkID\r\nWHERE\r\n    StudentQuiz.CreatedDate BETWEEN CustomerCourse.CourseStartDate AND CustomerCourse.CourseEndDate\r\n    AND CAST(StudentQuiz.CreatedDate AS DATE) = CAST(GETDATE() AS DATE)\r\nORDER BY\r\n    StudentQuiz.CreatedDate;";
                return conn.Query<StudentQuiz>(query).ToList();
            }
            catch
            {
                throw new Exception();
            }
        }
        public bool DeleteQuiz(Quiz quiz)
        {
            try
            {
                GenericRepository<Quiz> repo = new GenericRepository<Quiz>();
                repo.SoftDelete(quiz);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateQuiz(Quiz quiz)
        {
            try
            {
                GenericRepository<Quiz> repo = new GenericRepository<Quiz>();
                repo.Update(quiz);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Quiz> GetAll()
        {
            GenericRepository<Quiz> repo = new GenericRepository<Quiz>();
            return repo.GetAll();
        }

        public Quiz GetById(int ID)
        {
            GenericRepository<Quiz> repository = new GenericRepository<Quiz>();
            return repository.GetById(ID);
        }
    }
}
