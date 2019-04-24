using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UKAR.Model
{
    public class DrivingTest
    {
        [Key]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public DateTime Date { get; set; }
        public int PracticeScore { get; set; } = 0;
        public int ExamScore { get; set; } = 0;
        public bool Passed { get; set; } = false;
    }
}
