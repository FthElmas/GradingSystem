using Dapper;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.Concrete;
using GradingSystem.DAL.Concrete.DapperGenericRepository.Repository;
using GradingSystem.DAL.DTOs.Quiz;
using GradingSystem.DAL.DTOs.StudentMark;
using GradingSystem.DAL.DTOs.StudentReport;
using GradingSystem.DAL.DTOs.StudentReportMark;
using GradingSystem.DAL.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.Logic
{
    public class StudentReportDAL
    {
        private readonly ConnectionHelper _connectionHelper;
        public StudentReportDAL()
        {
            _connectionHelper = new ConnectionHelper();
        }
        public bool AddStudentReport(StudentReport studentReport)
        {
            try
            {
                GenericRepository<StudentReport> repo = new GenericRepository<StudentReport>();
                repo.Add(studentReport);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public StudentReport GetById(int ID)
        {
            GenericRepository<StudentReport> repo = new GenericRepository<StudentReport>();
            return repo.GetById(ID);
        }

        public bool AddReportWithMark(StudentMark studentMark, int selectedWeek, Guid StudentID)
        {
            GenericRepository<StudentReport> repo;
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                try
                {
                    unitOfWork.BeginTransaction();
                    var repos = GetAllReportInSelectedWeekOfStudent(selectedWeek, StudentID);
                    var mark = unitOfWork.StudentMarkRepository.ReturnAdd(studentMark);
                    int markID = unitOfWork.Transaction.Connection.QueryFirstOrDefault<int>("select @@IDENTITY", transaction: unitOfWork.Transaction);
                    foreach (var item in repos)
                    {
                        unitOfWork.StudentReportMarkRepository.Add(new StudentReportMark() {StudentMarkID = markID, StudentReportID = item.StudentReportID, CreatedDate = DateTime.Now, IsActive = true });
                    }
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

        public bool DeleteStudentReport(StudentReport student)
        {
            try
            {
                GenericRepository<StudentReport> repo = new GenericRepository<StudentReport>();
                repo.SoftDelete(student);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateStudentReport(StudentReport student)
        {
            try
            {
                GenericRepository<StudentReport> repo = new GenericRepository<StudentReport>();
                repo.Update(student);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<StudentReportSelectDTO> GetAllReportInAllWeeks()
        {
            try
            {
                using var conn = _connectionHelper.CreateConnection();
                string query = "SET DATEFIRST 1; SELECT \r\n    Weeks.WeekCount,\r\n    StudentReport.ReportName,\r\n    StudentReport.Report\r\nFROM (\r\n    SELECT \r\n        number AS WeekCount,\r\n        DATEADD(DAY, (number - 1) * 7, CourseStartDate) AS WeekStart,\r\n        DATEADD(DAY, (number * 7) - 1, CourseStartDate) AS WeekEnd\r\n    FROM master.dbo.spt_values\r\n    JOIN CustomerCourse ON type = 'P'\r\n    WHERE number BETWEEN 1 AND DATEDIFF(DAY, CourseStartDate, CourseEndDate) / 7 + 1\r\n) AS Weeks\r\nJOIN StudentReport ON StudentReport.CreatedDate BETWEEN Weeks.WeekStart AND Weeks.WeekEnd\r\nJOIN Student ON Student.StudentID = StudentReport.StudentID\r\nJOIN CustomerCourse ON CustomerCourse.ID = Student.CustomerCourseID\r\nWHERE StudentReport.IsActive = 1 GROUP BY Weeks.WeekCount, StudentReport.ReportName, StudentReport.Report\r\nORDER BY Weeks.WeekCount;";
                return conn.Query<StudentReportSelectDTO>(query).ToList();
            }
            catch
            {
                throw new Exception();
            }
        }

        public List<StudentReportSelectDTO> GetAllReportInSelectedWeek(int selectedWeek)
        {
            try
            {
                using var conn = _connectionHelper.CreateConnection();
                string query = "SET DATEFIRST 1; SELECT \r\n    Weeks.WeekCount,\r\n  StudentReport.StudentReportID  ,StudentReport.ReportName,\r\n    StudentReport.Report\r\nFROM (\r\n    SELECT \r\n        number AS WeekCount,\r\n        DATEADD(DAY, (number - 1) * 7, CourseStartDate) AS WeekStart,\r\n        DATEADD(DAY, (number * 7) - 1, CourseStartDate) AS WeekEnd\r\n    FROM master.dbo.spt_values\r\n    JOIN CustomerCourse ON type = 'P'\r\n    WHERE number BETWEEN 1 AND DATEDIFF(DAY, CourseStartDate, CourseEndDate) / 7 + 1\r\n) AS Weeks\r\nJOIN StudentReport ON StudentReport.CreatedDate BETWEEN Weeks.WeekStart AND Weeks.WeekEnd\r\nJOIN Student ON Student.StudentID = StudentReport.StudentID\r\nJOIN CustomerCourse ON CustomerCourse.ID = Student.CustomerCourseID\r\nWHERE StudentReport.IsActive = 1 GROUP BY Weeks.WeekCount, StudentReport.StudentReportID,StudentReport.ReportName, StudentReport.Report\r\nHAVING WeekCount = @SelectedWeek\r\nORDER BY Weeks.WeekCount;";
                return conn.Query<StudentReportSelectDTO>(query, new {SelectedWeek = selectedWeek}).ToList();
            }
            catch
            {
                throw new Exception();
            }
        }

        public List<StudentReportSelectDTO> GetAllReportInSelectedWeekOfStudent(int selectedWeek, Guid studentID)
        {
            try
            {
                using var conn = _connectionHelper.CreateConnection();
                string query = "SET DATEFIRST 1; SELECT \r\n    Weeks.WeekCount,\r\n\tStudentReport.StudentReportID,StudentReport.StudentID,\r\n    StudentReport.ReportName,\r\n    StudentReport.Report\r\nFROM (\r\n    SELECT \r\n        number AS WeekCount,\r\n        DATEADD(DAY, (number - 1) * 7, CourseStartDate) AS WeekStart,\r\n        DATEADD(DAY, (number * 7) - 1, CourseStartDate) AS WeekEnd\r\n    FROM master.dbo.spt_values\r\n    JOIN CustomerCourse ON type = 'P'\r\n    WHERE number BETWEEN 1 AND DATEDIFF(DAY, CourseStartDate, CourseEndDate) / 7 + 1\r\n) AS Weeks\r\nJOIN StudentReport ON StudentReport.CreatedDate BETWEEN Weeks.WeekStart AND Weeks.WeekEnd\r\nJOIN Student ON Student.StudentID = StudentReport.StudentID\r\nJOIN CustomerCourse ON CustomerCourse.ID = Student.CustomerCourseID\r\nWHERE StudentReport.IsActive = 1 GROUP BY Weeks.WeekCount, StudentReport.StudentReportID,StudentReport.StudentID,StudentReport.ReportName, StudentReport.Report\r\nHAVING WeekCount = @SelectedWeek and StudentReport.StudentID = @StudentID\r\nORDER BY Weeks.WeekCount;";
                return conn.Query<StudentReportSelectDTO>(query, new { SelectedWeek = selectedWeek, StudentID = studentID }).ToList();
            }
            catch
            {
                throw new Exception();
            }
        }

        public List<StudentReportSelectDTO> GetAllReportInTodayOfStudent(Guid studentID)
        {
            try
            {
                using var conn = _connectionHelper.CreateConnection();
                string query = "SET DATEFIRST 1; SELECT \r\n    Weeks.WeekCount,\r\n StudentReport.StudentReportID   ,StudentReport.StudentID,\r\n    StudentReport.ReportName,\r\n    StudentReport.Report, StudentReport.CreatedDate\r\nFROM (\r\n    SELECT \r\n        number AS WeekCount,\r\n        DATEADD(DAY, (number - 1) * 7, CourseStartDate) AS WeekStart,\r\n        DATEADD(DAY, (number * 7) - 1, CourseStartDate) AS WeekEnd\r\n    FROM master.dbo.spt_values\r\n    JOIN CustomerCourse ON type = 'P'\r\n    WHERE number BETWEEN 1 AND DATEDIFF(DAY, CourseStartDate, CourseEndDate) / 7 + 1\r\n) AS Weeks\r\nJOIN StudentReport ON \r\n    StudentReport.CreatedDate BETWEEN Weeks.WeekStart AND Weeks.WeekEnd\r\n    AND CONVERT(DATE, StudentReport.CreatedDate) = CONVERT(DATE, GETDATE())\r\nJOIN Student ON Student.StudentID = StudentReport.StudentID\r\nJOIN CustomerCourse ON CustomerCourse.ID = Student.CustomerCourseID\r\nWHERE StudentReport.IsActive = 1 GROUP BY Weeks.WeekCount, StudentReport.StudentReportID ,StudentReport.StudentID, StudentReport.ReportName, StudentReport.Report, StudentReport.CreatedDate\r\nHAVING StudentReport.StudentID = @StudentID\r\nORDER BY StudentReport.CreatedDate;";
                return conn.Query<StudentReportSelectDTO>(query, new { StudentID = studentID }).ToList();
            }
            catch
            {
                throw new Exception();
            }
        }


        public List<StudentReportSelectDTO> GetAllReportInThisWeekOfStudent(Guid studentID)
        {
            try
            {
                using var conn = _connectionHelper.CreateConnection();
                string query = "SET DATEFIRST 1; SELECT \r\n    Weeks.WeekCount,\r\n  StudentReport.StudentReportID  ,StudentReport.StudentID,\r\n    StudentReport.ReportName,\r\n    StudentReport.Report\r\nFROM (\r\n    SELECT \r\n        number AS WeekCount,\r\n        DATEADD(DAY, (number - 1) * 7, CourseStartDate) AS WeekStart,\r\n        DATEADD(DAY, (number * 7) - 1, CourseStartDate) AS WeekEnd\r\n    FROM master.dbo.spt_values\r\n    JOIN CustomerCourse ON type = 'P'\r\n    WHERE number BETWEEN 1 AND DATEDIFF(DAY, CourseStartDate, CourseEndDate) / 7 + 1\r\n) AS Weeks\r\nJOIN StudentReport ON StudentReport.CreatedDate BETWEEN Weeks.WeekStart AND Weeks.WeekEnd\r\nJOIN Student ON Student.StudentID = StudentReport.StudentID\r\nJOIN CustomerCourse ON CustomerCourse.ID = Student.CustomerCourseID\r\nWHERE Weeks.WeekStart <= GETDATE() AND Weeks.WeekEnd >= GETDATE()\r\n  AND StudentReport.StudentID = @StudentID AND StudentReport.IsActive = 1\r\nGROUP BY Weeks.WeekCount,StudentReport.StudentReportID ,StudentReport.StudentID, StudentReport.ReportName, StudentReport.Report\r\nORDER BY Weeks.WeekCount;";
                return conn.Query<StudentReportSelectDTO>(query, new { StudentID = studentID }).ToList();
            }
            catch
            {
                throw new Exception();
            }
        }

      

    }
}
