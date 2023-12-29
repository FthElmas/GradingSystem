using FluentValidation.Results;
using GradingSystem.BLL.Common.Mapper;
using GradingSystem.BLL.Handler.StudentMark;
using GradingSystem.BLL.Handler.StudentQuiz;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.DTOs.Student;
using GradingSystem.DAL.DTOs.StudentMark;
using GradingSystem.DAL.DTOs.StudentQuiz;
using GradingSystem.DAL.DTOs.StudentReport;
using GradingSystem.DAL.Logic;
using GradingSystem.DTO.DTOs.StudentQuiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.BLLLogic
{
    public class StudentQuizBLL : IStudentQuiz
    {
        private StudentQuizDAL _dal;
        private ValidateStudentMark validationMark;
        private ValidateStudentQuiz validationStudentQuiz;
        public StudentQuizBLL()
        {
            _dal = new StudentQuizDAL();
            validationMark = new ValidateStudentMark();
            validationStudentQuiz = new ValidateStudentQuiz();
        }


        public bool AddStudentQuizWithMark(int quizID, StudentMarkAddDTO studentMark, Guid studentID, string description)
        {
            MyMapper<StudentMarkAddDTO, StudentMark> mapper = new MyMapper<StudentMarkAddDTO, StudentMark>();
            var data = mapper.Map(studentMark);
            ValidationResult result = validationMark.Validate(data);
            if (result.IsValid)
            {
                return _dal.AddStudentQuizWithMark(quizID, data, studentID, description);
            }
            else
            {
                throw new Exception();
            }
        }

        public bool DeleteStudentQuizWithMark(StudentQuizAddDTO student)
        {
            MyMapper<StudentQuizAddDTO, StudentQuiz> mapper = new MyMapper<StudentQuizAddDTO, StudentQuiz>();
            var data = mapper.Map(student);
            ValidationResult result = validationStudentQuiz.Validate(data);
            if(result.IsValid)
            {
                return _dal.DeleteStudentQuizWithMark(data);
            }
            else
            {
                throw new Exception();
            }
        }

        public List<StudentQuizSelectDTO> GetAll()
        {
            var data = _dal.GetAll();
            if(data != null)
            {
                return data;
            }
            else
            {
                throw new Exception();
            }
        }

        public List<List<StudentQuizSelectDTO>> GetAllQuizInSelectedWeekOfStudent(List<StudentSelectDTO> students, int selectedWeek)
        {
            var list = new List<List<StudentQuizSelectDTO>>();
            MyMapper<StudentQuiz, StudentQuizSelectDTO> mapper = new MyMapper<StudentQuiz, StudentQuizSelectDTO>();
            foreach (var item in students)
            {
                var data = _dal.GetAllStudentQuizInSelectedWeekOfStudent(selectedWeek, item.StudentID);
                list.Add(data);
            }
            return list;

        }

        public StudentQuizAddDTO GetByID(Guid student, int quiz)
        {
            if(student != Guid.Empty && quiz != 0)
            {
                return _dal.GetByID(student, quiz);
            }
            else
            {
                throw new Exception();
            }
        }

        public bool UpdateStudentQuizWithMark(StudentQuizAddDTO student, int mark)
        {
            MyMapper<StudentQuizAddDTO, StudentQuiz> mapper = new MyMapper<StudentQuizAddDTO, StudentQuiz>();
            var data = mapper.Map(student);
            ValidationResult result = validationStudentQuiz.Validate(data);
            if(result.IsValid)
            {
                return _dal.UpdateStudentQuizWithMark(data, mark);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
