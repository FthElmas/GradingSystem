using GradingSystem.BLL.Services;
using GradingSystem.DTO.DTOs.Complex;
using Microsoft.AspNetCore.Mvc;

namespace GradingSystem.ViewComponents
{
    public class StudentViewComponent : ViewComponent
    {
        public StudentViewComponent()
        {
            
        }



        public IViewComponentResult Invoke(int data)
        {
            var _courseID = ViewData["courseId"] as int?;
            if(_courseID == null)
            {
                _courseID = data;
            }
            return View(new StudentReportPage() {Student = (new StudentBLL().GetAllStudent(Guid.Parse("73aed8b9-6ec3-4f41-8306-83936a13b073"), (int)_courseID)), StudentReport =  (new StudentReportBLL().GetAllReportInTodayOfStudent(new StudentBLL().GetAllStudent(Guid.Parse("73aed8b9-6ec3-4f41-8306-83936a13b073"), (int)_courseID))), WeekReport = (new StudentReportBLL().GetAllReportInThisWeekOfStudent(new StudentBLL().GetAllStudent(Guid.Parse("73aed8b9-6ec3-4f41-8306-83936a13b073"), (int)_courseID)))});
        }
    }
}
