using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecCore.Entities
{
    public class Candidate
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string FirstName { get; set; } = "";
        public string? MiddleName { get; set; } = "";
        [Required(ErrorMessage = "Required")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string LastName { get; set; } = "";
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b")]
        public string Email { get; set; } = "";
        [Required(ErrorMessage = "Required")]
        public string ResumeURL { get; set; } = "";
        public List<Submission>? Submissions { get; set; }

    }
}
