using GradingSystem.DAL.DTOs.Quiz;
using GradingSystem.DAL.DTOs.StudentMark;
using GradingSystem.DAL.DTOs.StudentQuiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.Abstract.Interface
{
    public interface IQuizDAL
    {
        bool AddQuiz(QuizAddDTO quiz);
        List<StudentQuizAddDTO> GetAllQuizInSelectedWeek(int selectedWeek, Guid studentID);
        List<StudentQuizAddDTO> GetAllQuizToday();
        bool DeleteQuiz(QuizSelectDTO quiz);
        bool UpdateQuiz(QuizUpdateDTO quiz);
        List<QuizSelectDTO> GetAll();
        QuizUpdateDTO GetById(int ID);
    }
}
