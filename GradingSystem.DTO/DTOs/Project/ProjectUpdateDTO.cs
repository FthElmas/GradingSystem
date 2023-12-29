using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.DTOs.Project
{
    public class ProjectUpdateDTO
    {
        [Key]
        public int ProjectID { get; set; }
        [Column("ProjectName")]
        public string ProjectName { get; set; }
        [Column("ProjectDescription")]
        public string ProjectDescription { get; set; }
        [Column("ProjectStartDate")]
        public DateTime? ProjectStartDate { get; set; }
        [Column("ProjectEndDate")]
        public DateTime? ProjectEndDate { get; set; }
        [Column("IsActive")]
        public bool IsActive { get; set; }
    }
}
