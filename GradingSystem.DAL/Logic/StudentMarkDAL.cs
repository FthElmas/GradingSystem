using Dapper;
using GradingSystem.DAL.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.Logic
{
    public class StudentMarkDAL
    {
        private readonly ConnectionHelper _connectionHelper;
        public StudentMarkDAL()
        {
            _connectionHelper = new ConnectionHelper();
        }
        public bool UpdateStudentProjectMark(int mark, Guid studentID, int projectID)
        {
            try
            {
                using var conn = _connectionHelper.CreateConnection();
                string query = "update StudentMark SET Mark = @Mark from StudentMark\r\njoin StudentProject on StudentProject.StudentMarkID = StudentMark.StudentMarkID\r\nwhere StudentProject.StudentID = @StudentID and StudentProject.ProjectID = @ProjectID";
                conn.Execute(query, new { Mark = mark, StudentID = studentID, ProjectID = projectID });
                return true;
            }
            catch
            {
                throw new Exception();
            }
        }

        public bool UpdateStudentReportMark(int mark, Guid studentID, int reportID)
        {
            try
            {
                using var conn = _connectionHelper.CreateConnection();
                string query = "update StudentMark SET Mark = @Mark from StudentMark\r\njoin StudentReportMark on StudentReportMark.StudentMarkID = StudentMark.StudentMarkID\r\njoin StudentReport on StudentReport.StudentReportID = StudentReportMark.StudentReportID\r\nwhere StudentReport.StudentID = @StudentID and StudentReportMark.StudentReportID = @ReportID";
                conn.Execute(query, new { Mark = mark, StudentID = studentID, ReportID = reportID });
                return true;
            }
            catch
            {
                throw new Exception();
            }
        }

        public bool UpdateStudentQuizMark(int mark, Guid studentID, int quizID)
        {
            try
            {
                using var conn = _connectionHelper.CreateConnection();
                string query = "update StudentMark SET Mark = @Mark from StudentMark\r\njoin StudentQuiz on StudentQuiz.StudentMarkID = StudentMark.StudentMarkID\r\nwhere StudentQuiz.StudentID = @StudentID and StudentQuiz.QuizID = @QuizID";
                conn.Execute(query, new { Mark = mark, StudentID = studentID, QuizID = quizID });
                return true;
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
