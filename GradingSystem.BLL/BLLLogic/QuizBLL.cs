using FluentValidation.Results;
using GradingSystem.BLL.Common.Mapper;
using GradingSystem.BLL.Handler.Quiz;
using GradingSystem.BLL.Handler.StudentMark;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.DTOs.Quiz;
using GradingSystem.DAL.DTOs.StudentMark;
using GradingSystem.DAL.DTOs.StudentQuiz;
using GradingSystem.DAL.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Services
{
    public class QuizBLL : IQuizDAL
    {
        QuizDAL quizDAL;
        ValidateQuiz validation;
        ValidateStudentMark validationMark;
        public QuizBLL()
        {
            quizDAL = new QuizDAL();
            validation = new ValidateQuiz();
            validationMark = new ValidateStudentMark();
        }

        public bool AddQuiz(QuizAddDTO quiz)
        {
            ValidationResult result = validation.Validate(quiz);
            MyMapper<QuizAddDTO, Quiz> mapper = new MyMapper<QuizAddDTO, Quiz>();
            if (result.IsValid)
            {
                return quizDAL.AddQuiz(mapper.Map(quiz));
            }
            else
            {
                throw new Exception();
            }
        }


        public bool DeleteQuiz(QuizSelectDTO quiz)
        {
            MyMapper<QuizSelectDTO, Quiz> mapper = new MyMapper<QuizSelectDTO, Quiz>();
            return quizDAL.DeleteQuiz(mapper.Map(quiz));
        }

        public List<QuizSelectDTO> GetAll()
        {
            MyMapper<Quiz, QuizSelectDTO> mapper = new MyMapper<Quiz, QuizSelectDTO>();
            List<QuizSelectDTO> quizzes = new List<QuizSelectDTO>();
            var data = quizDAL.GetAll().ToList();
            data.ForEach(a => quizzes.Add(mapper.Map(a)));
            return quizzes;
        }

        public List<StudentQuizAddDTO> GetAllQuizInSelectedWeek(int selectedWeek, Guid studentID)
        {
            MyMapper<StudentQuiz, StudentQuizAddDTO> mapper = new MyMapper<StudentQuiz, StudentQuizAddDTO>();
            var data = quizDAL.GetAllQuizInSelectedWeek(selectedWeek, studentID);
            List<StudentQuizAddDTO> studentQuizzes = new List<StudentQuizAddDTO>();
            data.ForEach(a => studentQuizzes.Add(mapper.Map(a)));
            return studentQuizzes;
        }

        public List<StudentQuizAddDTO> GetAllQuizToday()
        {
            MyMapper<StudentQuiz, StudentQuizAddDTO> mapper = new MyMapper<StudentQuiz, StudentQuizAddDTO>();
            var data = quizDAL.GetAllQuizToday();
            List<StudentQuizAddDTO> studentQuizzes = new List<StudentQuizAddDTO>();
            data.ForEach(a => studentQuizzes.Add(mapper.Map(a)));
            return studentQuizzes;
        }

        public QuizUpdateDTO GetById(int ID)
        {
            MyMapper<Quiz, QuizUpdateDTO> mapper = new MyMapper<Quiz, QuizUpdateDTO>();
            if(ID != 0)
            {
                var data = quizDAL.GetById(ID);
                return mapper.Map(data);
            }
            else
            {
                throw new Exception();
            }
            
        }

        public bool UpdateQuiz(QuizUpdateDTO quiz)
        {
            MyMapper<QuizUpdateDTO, Quiz> mapper = new MyMapper<QuizUpdateDTO, Quiz>();
            return quizDAL.UpdateQuiz(mapper.Map(quiz));
        }
    }
}
