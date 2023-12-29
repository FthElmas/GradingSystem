using FluentValidation;
using FluentValidation.Results;
using GradingSystem.BLL.Common.Mapper;
using GradingSystem.BLL.Handler.StudentMark;
using GradingSystem.BLL.Handler.StudentProject;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.DTOs.Student;
using GradingSystem.DAL.DTOs.StudentMark;
using GradingSystem.DAL.DTOs.StudentProject;
using GradingSystem.DAL.Logic;
using GradingSystem.DTO.DTOs.StudentProject;
using GradingSystem.DTO.DTOs.StudentQuiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Services
{
    public class StudentProjectBLL : IStudentProjectDAL
    {
        StudentProjectDAL projectDAL;
        ValidateStudentMark validation;
        ValidateStudentProject validationProject;
        
        public StudentProjectBLL()
        {
            projectDAL = new StudentProjectDAL();
            validation = new ValidateStudentMark();
            validationProject = new ValidateStudentProject();
        }
        public bool AddStudentProjectWithMark(int projectID, StudentMarkAddDTO studentMark, string description)
        {
            MyMapper<StudentMarkAddDTO, StudentMark> mapper = new MyMapper<StudentMarkAddDTO, StudentMark>();
            var data = mapper.Map(studentMark);
            ValidationResult result = validation.Validate(data);
            if(result.IsValid && projectID != 0 && description != null)
            {
                return projectDAL.AddStudentProjectWithMark(projectID, data, description);
            }
            else
            {
                throw new Exception();
            }
        }

        public bool DeleteStudentProjectWithMark(StudentProjectDTO student)
        {
            MyMapper<StudentProjectDTO, StudentProject> mapper = new MyMapper<StudentProjectDTO, StudentProject>();
            var data = mapper.Map(student);
            ValidationResult result = validationProject.Validate(data);
            if (result.IsValid)
            {
                return projectDAL.DeleteStudentProjectWithMark(mapper.Map(student));
            }
            else
            {
                throw new Exception();
            }
        }

        public List<StudentProjectSelectDTO> GetAll()
        {
            var data = projectDAL.GetAll();
            if(data != null)
            {
                return data;
            }
            else
            {
                throw new Exception();
            }
        }

        public List<List<StudentProjectSelectDTO>> GetAllProjectInSelectedWeekOfStudent(List<StudentSelectDTO> students, int selectedWeek)
        {
            var list = new List<List<StudentProjectSelectDTO>>();
            MyMapper<StudentProject, StudentProjectSelectDTO> mapper = new MyMapper<StudentProject, StudentProjectSelectDTO>();
            foreach (var item in students)
            {
                var data = projectDAL.GetAllStudentProjectInSelectedWeekOfStudent(selectedWeek, item.StudentID);
                list.Add(data);
            }
            return list;

        }

        public StudentProjectDTO GetByID(Guid student, int project)
        {
            MyMapper<StudentProject, StudentProjectDTO> mapper = new MyMapper<StudentProject, StudentProjectDTO>();
            if (student != Guid.Empty && project != 0)
            {
                var data = projectDAL.GetByID(student, project);
                return mapper.Map(data);
            }
            else
            {
                throw new Exception();
            }
        }

        public bool UpdateStudentProjectWithMark(StudentProjectDTO student, int mark)
        {
            MyMapper<StudentProjectDTO, StudentProject> mapper = new MyMapper<StudentProjectDTO, StudentProject>();
            var data = mapper.Map(student);
            ValidationResult result = validationProject.Validate(data);
            if(result.IsValid)
            {
                return projectDAL.UpdateStudentProjectWithMark(data, mark);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
