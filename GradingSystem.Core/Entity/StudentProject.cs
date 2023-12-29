using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.Core.Entity
{
    [Table("[StudentProject]")]
    public class StudentProject
    {
        [ForeignKey("Student")]
        public Guid StudentID { get; set; }

        [ForeignKey("Project")]
        public int ProjectID { get; set; }

        [ForeignKey("StudentMark")]
        public int StudentMarkID { get; set; }

        [Column("Description")]
        public string Description { get; set; }
        [Column("IsActive")]
        public bool IsActive { get; set; }

        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }



    }
}
