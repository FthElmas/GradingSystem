using GradingSystem.DAL.DTOs.CustomerCourse;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DTO.DTOs.Complex
{
    public class CustomerCourseAddPage
    {
        public CustomerCourseAddDTO CustomerCourse { get; set; }
        public List<SelectListItem> CustomerList { get; set; }
        public List<SelectListItem> CourseList { get; set; }
    }
}
