using Dapper;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.DTOs.Course;
using GradingSystem.DAL.DTOs.Customer;
using GradingSystem.DAL.Helper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.Logic
{
    public class CourseDAL
    {
        private readonly ConnectionHelper _connectionHelper;
        public CourseDAL()
        {
            _connectionHelper = new ConnectionHelper();
        }

        public List<Course> GetAllCourse()
        {
            using var conn = _connectionHelper.CreateConnection();
            string query = "select * from Course";
            return conn.Query<Course>(query).ToList();
        }

        public Course AddCourse(Course course, Guid teacherID)
        {
            try
            {
                using var conn = _connectionHelper.CreateConnection();
                string insertQuery = @"
                INSERT INTO [dbo].[Course] 
                ([CourseName], [IsActive], [CreatedDate], [CreatedBy]) OUTPUT INSERTED.*
                VALUES
                (@CourseName, @IsActive, @CreatedDate, @CreatedBy)
                ";
                return conn.QueryFirstOrDefault<Course>(insertQuery, new
                {
                    CourseName = course.CourseName,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    CreatedBy = teacherID
                });
            }
            catch
            {
                return null;
            }
        }

        public void UpdateCourse(Course course)
        {
            using var conn = _connectionHelper.CreateConnection();

                string updateQuery = @"
                UPDATE [dbo].[Course]
                SET
                [CourseName] = @CourseName,
                [IsActive] = @IsActive
                WHERE
                [CourseID] = @CourseID
                ";

                conn.Execute(updateQuery, new
                {
                    CourseID = course.CourseID,
                    CourseName = course.CourseName,
                    IsActive = course.IsActive
                });
            
        }
        public bool CheckCourse(string courseName)
        {
            using var conn = _connectionHelper.CreateConnection();
            string query = "select * from Course where CourseName = @CourseName";
            if (conn.QueryFirstOrDefault(query, new { CourseName = courseName }))
            {
                return true;
            }
            return false;
        }

        public void SoftDeleteCourse(Course course)
        {
            using var conn = _connectionHelper.CreateConnection();

            string updateQuery = @"
                UPDATE [dbo].[Course]
                SET
                [IsActive] = 0
                WHERE
                [CourseID] = @CourseID
                ";

            conn.Execute(updateQuery, new
            {
                CourseID = course.CourseID
            });
        }


    }
}
