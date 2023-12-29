using GradingSystem.DAL.DTOs.CustomerCourse;
using GradingSystem.DAL.DTOs.Student;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DTO.DTOs.Complex
{
    public class StudentAdd
    {
        public StudentAddDTO Student { get; set; }
        public List<SelectListItem> CustomerCourseList { get; set; }

    }
}
