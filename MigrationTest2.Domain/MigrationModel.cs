using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationTest2.Domain
{
    public class MigrationModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Start { get; set; }

        [Required]
        public int End { get; set; }

        public string Status { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

    }
}
