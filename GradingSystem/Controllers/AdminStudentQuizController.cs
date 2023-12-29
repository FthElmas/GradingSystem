using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.DTOs.Quiz;
using GradingSystem.DAL.DTOs.StudentQuiz;
using Microsoft.AspNetCore.Mvc;

namespace GradingSystem.Controllers
{
    public class AdminStudentQuizController : Controller
    {
        private readonly IStudentQuiz _dal;
        public AdminStudentQuizController(IStudentQuiz dal)
        {
            _dal = dal;
        }
        public IActionResult Index()
        {
            var data = _dal.GetAll();
            return View(data);
        }

        public IActionResult DeleteStudentQuiz(Guid student, int quiz)
        {
            var data = _dal.GetByID(student, quiz);
            if (data != null)
            {
                _dal.DeleteStudentQuizWithMark(data);
                return RedirectToAction("Index", "AdminStudentQuiz");
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult UpdateStudentQuiz(Guid student,int quiz)
        {
            var data = _dal.GetByID(student, quiz);
            if (data != null)
            {
                return View(data);
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult UpdateStudentQuiz([FromForm] StudentQuizAddDTO quizUpdate, int mark)
        {
            if (_dal.UpdateStudentQuizWithMark(quizUpdate, mark))
            {
                return View(quizUpdate);
            }
            else
                return BadRequest();
        }

    }
}
