using Dapper;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.Concrete.DapperGenericRepository.Repository;
using GradingSystem.DAL.DTOs.Student;
using GradingSystem.DAL.DTOs.Teacher;
using GradingSystem.DAL.DTOs.User;
using GradingSystem.DAL.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.Logic
{
    public class StudentDAL
    {
        private readonly ConnectionHelper _connectionHelper;
        public StudentDAL()
        {
            _connectionHelper = new ConnectionHelper();
        }


        public List<Student> GetAllStudent(Guid teacherID, int customerCourseID)
        {
            using var conn = _connectionHelper.CreateConnection();
            string query = "select * from Student\r\njoin CustomerCourse on CustomerCourse.ID = Student.CustomerCourseID\r\njoin StudentTeacher on StudentTeacher.StudentID = Student.StudentID\r\njoin Teacher on Teacher.TeacherID = StudentTeacher.TeacherID\r\nwhere Teacher.TeacherID = @TeacherID and CustomerCourse.ID = @CustomerCourseID and Student.IsActive = 1";

            return conn.Query<Student>(query, new { TeacherID = teacherID, CustomerCourseID = customerCourseID}).ToList();
        }

        public List<Student> GetAllStudentOfTeacher(Guid teacherID)
        {
            using var conn = _connectionHelper.CreateConnection();
            string query = "select Student.StudentID, Student.StudentName, Student.StudentSurname, Student.Email, Student.GraduationState, Student.CustomerCourseID, Student.IsEliminated, Student.IsActive from Student\r\njoin CustomerCourse on CustomerCourse.ID = Student.CustomerCourseID\r\njoin StudentTeacher on StudentTeacher.StudentID = Student.StudentID\r\njoin Teacher on Teacher.TeacherID = StudentTeacher.TeacherID\r\nwhere Teacher.TeacherID = @TeacherID";

            return conn.Query<Student>(query, new { TeacherID = teacherID}).ToList();
        }
        public bool AddStudentWithStudentTeacher(Student student, Guid teacherID)
        {
            var conn = _connectionHelper.CreateConnection();
            using (var transaction = conn.BeginTransaction())
            {
                try
                {
                    string studentQuery = @"
                    INSERT INTO [dbo].[Student] 
                    ([StudentID], [StudentName], [StudentSurname], [Email], [GraduationState], [CustomerCourseID], [IsEliminated], [CreatedDate], [CreatedBy], [IsActive])
                    VALUES
                    (@StudentID, @StudentName, @StudentSurname, @Email, @GraduationState, @CustomerCourseID, @IsEliminated, GETDATE(), @CreatedBy, @IsActive)
                    ";
                    conn.Execute(studentQuery, new
                    {
                        StudentID = student.StudentID,
                        StudentName = student.StudentName,
                        StudentSurname = student.StudentSurname,
                        Email = student.Email,
                        GraduationState = student.GraduationState,
                        CustomerCourseID = student.CustomerCourseID,
                        IsEliminated = student.IsEliminated,
                        CreatedBy = teacherID,
                        IsActive = student.IsActive
                    }, transaction);

                    string studentTeacherQuery = "INSERT INTO [StudentTeacher] (StudentID, TeacherID, CreatedBy, IsActive, CreatedDate) VALUES (@StudentID, @TeacherID, @CreatedBy, @IsActive, @CreatedDate)";
                    conn.Execute(studentTeacherQuery, new {StudentID = student.StudentID, TeacherID = teacherID, CreatedBy = teacherID, IsActive = true, CreatedDate = student.CreatedDate }, transaction);
                    transaction.Commit();
                    _connectionHelper.CloseConnection(conn);
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        public bool CheckStudent(Student student)
        {
            using var conn = _connectionHelper.CreateConnection();
            string query = "select * from Student where StudentName = @StudentName";
            if (conn.QueryFirstOrDefault(query, new { StudentName = student.StudentName }))
            {
                return true;
            }
            return false;
        }
        public bool UpdateStudent(Student student)
        {
            //try
            //{
            //    using var conn = _connectionHelper.CreateConnection();
            //    string updateQuery = @"
            //    UPDATE [dbo].[Student]
            //    SET
            //    [StudentName] = @StudentName,
            //    [StudentSurname] = @StudentSurname,
            //    [PicturePath] = @PicturePath,
            //    [IsEliminated] = @IsEliminated,
            //    [IsActive] = @IsActive
            //    WHERE
            //    [StudentID] = @StudentID
            //    ";
            //    conn.Execute(updateQuery, new
            //    {
            //        StudentID = student.StudentID,
            //        StudentName = student.StudentName,
            //        StudentSurname = student.StudentSurname,
            //        PicturePath = student.PicturePath,
            //        IsEliminated = student.IsEliminated,
            //        IsActive = student.IsActive
            //    });
            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}
            try
            {
                GenericRepository<Student> repo = new GenericRepository<Student>();
                repo.Update(student);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool SoftDeleteStudent(StudentSelectDTO student)
        {
            try
            {
                using var conn = _connectionHelper.CreateConnection();
                string updateQuery = @"
                UPDATE [dbo].[Student]
                SET
                [IsActive] = 0
                WHERE
                [StudentID] = @StudentID
                ";
                conn.Execute(updateQuery, new { StudentID = student.StudentID });
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
