using GradingSystem.Core.Entity;
using GradingSystem.DAL.DTOs.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.Abstract.Interface
{
    public interface IStudentDAL
    {
        List<StudentSelectDTO> GetAllStudent(Guid teacherID, int customerCourseID);
        List<StudentSelectDTO> GetAllStudentOfTeacher(Guid teacherID);
        bool AddStudentWithStudentTeacher(StudentAddDTO student, Guid teacherID);
        bool CheckStudent(StudentAddDTO student);
        bool UpdateStudent(StudentUpdateDTO student);
        bool SoftDeleteStudent(StudentSelectDTO student);

    }
}
