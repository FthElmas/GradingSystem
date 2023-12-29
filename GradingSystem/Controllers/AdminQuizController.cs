using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.DTOs.Project;
using GradingSystem.DAL.DTOs.Quiz;
using Microsoft.AspNetCore.Mvc;

namespace GradingSystem.Controllers
{
    public class AdminQuizController : Controller
    {
        IQuizDAL _dal;

        public AdminQuizController(IQuizDAL dal)
        {
            _dal = dal;
        }
        public IActionResult Index()
        {
            var data = _dal.GetAll();
            return View(data);
        }


        public IActionResult DeleteQuiz(int quiz)
        {
            if (quiz != 0)
            {
                _dal.DeleteQuiz(new QuizSelectDTO { QuizID = quiz });
                return RedirectToAction("Index", "AdminQuiz");
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult UpdateQuiz(int quiz)
        {
            var data = _dal.GetById(quiz);
            if (data != null)
            {
                return View(data);
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult UpdateQuiz([FromForm] QuizUpdateDTO quizUpdate)
        {
            if (_dal.UpdateQuiz(quizUpdate))
            {
                return View(quizUpdate);
            }
            else
                return BadRequest();
        }
    }
}
