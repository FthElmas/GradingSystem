using GradingSystem.DAL.DTOs.Project;
using GradingSystem.DAL.DTOs.Quiz;
using GradingSystem.DAL.DTOs.StudentMark;
using GradingSystem.DAL.DTOs.StudentProject;
using GradingSystem.DAL.DTOs.StudentQuiz;
using GradingSystem.DAL.DTOs.StudentReport;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GradingSystem.DTO.DTOs.Complex
{
    public class StudentPageDTO
    {
        public StudentProjectDTO StudentProject { get; set; } = new StudentProjectDTO();
        public StudentQuizAddDTO StudentQuiz { get; set; } = new StudentQuizAddDTO();
        public ProjectAddDTO Project { get; set; } = new ProjectAddDTO();
        public StudentMarkAddDTO StudentMark { get; set; } = new StudentMarkAddDTO();
        public StudentReportAddDTO StudentReport { get; set; } = new StudentReportAddDTO();
        public QuizAddDTO Quiz { get; set; } = new QuizAddDTO();

        public List<SelectListItem> QuizList { get; set; }
        public List<SelectListItem> ProjectList { get; set; }

    }
}
