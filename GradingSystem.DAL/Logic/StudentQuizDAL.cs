using Dapper;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.Concrete;
using GradingSystem.DAL.Concrete.DapperGenericRepository.Repository;
using GradingSystem.DAL.DTOs.StudentQuiz;
using GradingSystem.DAL.Helper;
using GradingSystem.DTO.DTOs.StudentQuiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.Logic
{
    public class StudentQuizDAL
    {
        private readonly ConnectionHelper _connectionHelper;
        public StudentQuizDAL()
        {
            _connectionHelper = new ConnectionHelper();
        }
        public bool AddStudentQuizWithMark(int quizID, StudentMark studentMark, Guid studentID, string description)
        {

            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                try
                {
                    unitOfWork.BeginTransaction();
                    var mark = unitOfWork.StudentMarkRepository.ReturnAdd(studentMark);
                    int markID = unitOfWork.Transaction.Connection.QueryFirstOrDefault<int>("select @@IDENTITY", transaction: unitOfWork.Transaction);
                    unitOfWork.StudentQuizRepository.Add(new StudentQuiz { QuizID = quizID, StudentID = studentID, StudentMarkID = markID, Description = description, IsActive = true, CreatedDate = DateTime.Now });
                    unitOfWork.Commit();
                    return true;
                }
                catch
                {
                    unitOfWork.Rollback();
                    return false;
                }
            }
        }

        public bool UpdateStudentQuizWithMark(StudentQuiz student, int mark)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                try
                {
                    unitOfWork.BeginTransaction();

                    string query = "update StudentMark SET Mark = @Mark from StudentMark\r\njoin StudentQuiz on StudentQuiz.StudentMarkID = StudentMark.StudentMarkID\r\nwhere StudentQuiz.StudentID = @StudentID and StudentQuiz.QuizID = @QuizID";
                    unitOfWork.Conn.Execute(query, new { Mark = mark, StudentID = student.StudentID, QuizID = student.QuizID }, unitOfWork.Transaction);

                    string studenQuiz = "update StudentQuiz set Description = @Description, IsActive = @IsActive where StudentID = @StudentID and QuizID = @QuizID";
                    unitOfWork.Conn.Execute(studenQuiz, new { Description = student.Description, IsActive = student.IsActive, StudentID = student.StudentID, QuizID = student.QuizID }, unitOfWork.Transaction);
                    unitOfWork.Commit();
                    return true;
                }
                catch
                {
                    unitOfWork.Rollback();
                    return false;
                }
            }
        }


        public bool DeleteStudentQuizWithMark(StudentQuiz student)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                try
                {
                    unitOfWork.BeginTransaction();

                    string query = "update StudentMark SET IsActive = 0 from StudentMark\r\njoin StudentQuiz on StudentQuiz.StudentMarkID = StudentMark.StudentMarkID\r\nwhere StudentQuiz.StudentID = @StudentID and StudentQuiz.QuizID = @QuizID";
                    unitOfWork.Conn.Execute(query, new {  StudentID = student.StudentID, QuizID = student.QuizID, IsActive = student.IsActive }, unitOfWork.Transaction);

                    string studenQuiz = "update StudentQuiz set IsActive = 0 where StudentID = @StudentID and QuizID = @QuizID";
                    unitOfWork.Conn.Execute(studenQuiz, new { IsActive = student.IsActive, StudentID = student.StudentID, QuizID = student.QuizID }, unitOfWork.Transaction);
                    unitOfWork.Commit();
                    return true;
                }
                catch
                {
                    unitOfWork.Rollback();
                    return false;
                }
            }
        }

        public List<StudentQuizSelectDTO> GetAll()
        {
            using var conn = _connectionHelper.CreateConnection();
            string query = "select StudentQuiz.QuizID, StudentQuiz.StudentMarkID, StudentMark.Mark, Quiz.QuizName, StudentQuiz.StudentID, Student.StudentName, Student.StudentSurname, StudentQuiz.Description, StudentQuiz.IsActive, StudentQuiz.CreatedDate  from StudentQuiz join StudentMark on StudentMark.StudentMarkID = StudentQuiz.StudentMarkID join Quiz on StudentQuiz.QuizID = Quiz.QuizID join Student on Student.StudentID = StudentQuiz.StudentID where StudentQuiz.IsActive = 1";
            return conn.Query<StudentQuizSelectDTO>(query).ToList();
        }

        public StudentQuizAddDTO GetByID(Guid student, int quiz)
        {
            using var conn = _connectionHelper.CreateConnection();
            string query = "select * from StudentQuiz where IsActive = 1 and StudentID = @StudentID and QuizID = @QuizID";
            return conn.QueryFirstOrDefault<StudentQuizAddDTO>(query, new { StudentID = student, QuizID = quiz });
        }

        public List<StudentQuizSelectDTO> GetAllStudentQuizInSelectedWeekOfStudent(int selectedWeek, Guid studentID)
        {
            using var conn = _connectionHelper.CreateConnection();
            string query = "SET DATEFIRST 1;\r\nSELECT\r\n    Weeks.WeekCount,\r\n Quiz.QuizName   ,StudentQuiz.StudentID,\r\n    StudentQuiz.Description,\r\n    StudentMark.Mark\r\nFROM\r\n    (\r\n        SELECT\r\n            number AS WeekCount,\r\n            DATEADD(DAY, (number - 1) * 7, CourseStartDate) AS WeekStart,\r\n            DATEADD(DAY, (number * 7) - 1, CourseStartDate) AS WeekEnd\r\n        FROM\r\n            master.dbo.spt_values\r\n            JOIN CustomerCourse ON type = 'P'\r\n        WHERE\r\n            number BETWEEN 1 AND DATEDIFF(DAY, CourseStartDate, CourseEndDate) / 7 + 1\r\n    ) AS Weeks\r\n    JOIN StudentQuiz ON StudentQuiz.CreatedDate BETWEEN Weeks.WeekStart AND Weeks.WeekEnd\r\n    JOIN Student ON Student.StudentID = StudentQuiz.StudentID\r\n    JOIN CustomerCourse ON CustomerCourse.ID = Student.CustomerCourseID\r\n\tJOIN StudentMark ON StudentMark.StudentMarkID = StudentQuiz.StudentMarkID\r\njoin Quiz on Quiz.QuizID = StudentQuiz.QuizID WHERE\r\n    StudentQuiz.IsActive = 1\r\nGROUP BY\r\n    Weeks.WeekCount,\r\n  Quiz.QuizName   ,StudentQuiz.StudentID,\r\n    StudentQuiz.Description,\r\n    StudentMark.Mark\r\nHAVING\r\n    WeekCount = @SelectedWeek\r\n    AND StudentQuiz.StudentID = @StudentID\r\nORDER BY\r\n    Weeks.WeekCount;";

            return conn.Query<StudentQuizSelectDTO>(query, new { SelectedWeek = selectedWeek, StudentID = studentID }).ToList();
        }
    }
}
