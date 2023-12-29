using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.Core.Entity
{
    [Table("[CustomerCourse]")]
    public class CustomerCourse
    {
        [Key]
        public int ID { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [ForeignKey("Customer")]
        public Guid CustomerID { get; set; }

        [ForeignKey("Course")]
        public int CourseID { get; set; }

        [Column("CreatedBy")]
        public Guid CreatedBy { get; set; }

        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }

        [Column("CourseStartDate")]
        public DateTime CourseStartDate { get; set; }

        [Column("CourseEndDate")]
        public DateTime CourseEndDate { get; set; }
    }
}
