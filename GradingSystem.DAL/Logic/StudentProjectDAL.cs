using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.Concrete.DapperGenericRepository.Repository;
using GradingSystem.DAL.Concrete;
using GradingSystem.DAL.DTOs.StudentReport;
using GradingSystem.DAL.DTOs.StudentReportMark;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradingSystem.DAL.DTOs.StudentProject;
using GradingSystem.DAL.DTOs.StudentMark;
using GradingSystem.DAL.DTOs.Project;
using Dapper;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.DTOs.StudentQuiz;
using GradingSystem.DAL.Helper;
using GradingSystem.DTO.DTOs.StudentQuiz;
using GradingSystem.DTO.DTOs.StudentProject;

namespace GradingSystem.DAL.Logic
{
    public class StudentProjectDAL
    {
        private readonly ConnectionHelper _connectionHelper;
        public StudentProjectDAL()
        {
            _connectionHelper = new ConnectionHelper();
        }
        public bool AddStudentProjectWithMark(int projectID, StudentMark studentMark, string description)
        {
            
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                try
                {
                    unitOfWork.BeginTransaction();
                    var mark = unitOfWork.StudentMarkRepository.ReturnAdd(studentMark);
                    int markID = unitOfWork.Transaction.Connection.QueryFirstOrDefault<int>("select @@IDENTITY", transaction: unitOfWork.Transaction);
                    unitOfWork.StudentProjectRepository.Add(new StudentProject() {ProjectID = projectID, StudentID = mark.StudentID, StudentMarkID = markID, Description = description, IsActive = true, CreatedDate = DateTime.Today });
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

        public bool UpdateStudentProjectWithMark(StudentProject student, int mark)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                try
                {
                    unitOfWork.BeginTransaction();

                    string query = "update StudentMark SET Mark = @Mark from StudentMark\r\njoin StudentProject on StudentProject.StudentMarkID = StudentMark.StudentMarkID\r\nwhere StudentProject.StudentID = @StudentID and StudentProject.ProjectID = @ProjectID";
                    unitOfWork.Conn.Execute(query, new { Mark = mark, StudentID = student.StudentID, ProjectID = student.ProjectID }, unitOfWork.Transaction);

                    string studenQuiz = "update StudentProject set Description = @Description, IsActive = @IsActive where StudentID = @StudentID and ProjectID = @ProjectID";
                    unitOfWork.Conn.Execute(studenQuiz, new { Description = student.Description, IsActive = student.IsActive, StudentID = student.StudentID, ProjectID = student.ProjectID }, unitOfWork.Transaction);
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


        public bool DeleteStudentProjectWithMark(StudentProject student)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                try
                {
                    unitOfWork.BeginTransaction();

                    string query = "update StudentMark SET IsActive = 0 from StudentMark\r\njoin StudentProject on StudentProject.StudentMarkID = StudentMark.StudentMarkID\r\nwhere StudentProject.StudentID = @StudentID and StudentProject.ProjectID = @ProjectID";
                    unitOfWork.Conn.Execute(query, new { StudentID = student.StudentID, ProjectID = student.ProjectID }, unitOfWork.Transaction);

                    string studenQuiz = "update StudentProject set IsActive = 0 where StudentID = @StudentID and ProjectID = @ProjectID";
                    unitOfWork.Conn.Execute(studenQuiz, new { StudentID = student.StudentID, ProjectID = student.ProjectID }, unitOfWork.Transaction);
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

        public List<StudentProjectSelectDTO> GetAll()
        {
            using var conn = _connectionHelper.CreateConnection();
            string query = "select StudentProject.ProjectID, StudentProject.StudentMarkID, StudentMark.Mark, Project.ProjectName, StudentProject.StudentID, Student.StudentName, Student.StudentSurname, StudentProject.Description, StudentProject.IsActive, StudentProject.CreatedDate  from StudentProject join StudentMark on StudentMark.StudentMarkID = StudentProject.StudentMarkID join Project on StudentProject.ProjectID = Project.ProjectID join Student on Student.StudentID = StudentProject.StudentID where StudentProject.IsActive = 1";
            return conn.Query<StudentProjectSelectDTO>(query).ToList();
        }

        public List<StudentProjectSelectDTO> GetAllStudentProjectInSelectedWeekOfStudent(int selectedWeek, Guid studentID)
        {
            using var conn = _connectionHelper.CreateConnection();
            string query = "SET DATEFIRST 1;\r\nSELECT\r\n    Weeks.WeekCount,\r\n Project.ProjectName   ,StudentProject.StudentID,\r\n    StudentProject.Description,\r\n    StudentMark.Mark\r\nFROM\r\n    (\r\n        SELECT\r\n            number AS WeekCount,\r\n            DATEADD(DAY, (number - 1) * 7, CourseStartDate) AS WeekStart,\r\n            DATEADD(DAY, (number * 7) - 1, CourseStartDate) AS WeekEnd\r\n        FROM\r\n            master.dbo.spt_values\r\n            JOIN CustomerCourse ON type = 'P'\r\n        WHERE\r\n            number BETWEEN 1 AND DATEDIFF(DAY, CourseStartDate, CourseEndDate) / 7 + 1\r\n    ) AS Weeks\r\n    JOIN StudentProject ON StudentProject.CreatedDate BETWEEN Weeks.WeekStart AND Weeks.WeekEnd\r\n    JOIN Student ON Student.StudentID = StudentProject.StudentID\r\n    JOIN CustomerCourse ON CustomerCourse.ID = Student.CustomerCourseID\r\n\tJOIN StudentMark ON StudentMark.StudentMarkID = StudentProject.StudentMarkID\r\nJOIN Project ON Project.ProjectID = StudentProject.ProjectID  WHERE\r\n    StudentProject.IsActive = 1\r\nGROUP BY\r\n    Weeks.WeekCount,\r\n Project.ProjectName   ,StudentProject.StudentID,\r\n    StudentProject.Description,\r\n    StudentMark.Mark\r\nHAVING\r\n    WeekCount = @SelectedWeek\r\n    AND StudentProject.StudentID = @StudentID\r\nORDER BY\r\n    Weeks.WeekCount;";

            return conn.Query<StudentProjectSelectDTO>(query, new { SelectedWeek = selectedWeek, StudentID = studentID }).ToList();
        }

        public StudentProject GetByID(Guid student, int project)
        {
            using var conn = _connectionHelper.CreateConnection();
            string query = "select * from StudentProject where IsActive = 1 and StudentID = @StudentID and ProjectID = @ProjectID";
            return conn.QueryFirstOrDefault<StudentProject>(query, new { StudentID = student, ProjectID = project });
        }
    }
}
