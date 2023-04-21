using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecCore.Entities
{
    public class JobCategory
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(128, ErrorMessage = "Max 128 characters")]
        public string Name { get; set; } = "";
        public List<JobRequirement> JobRequirements { get; set; }
    }
}
