using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationTest2.Domain
{
    public class SourceModel
    {
        public int ID { get; set; }

        [Required]
        public int FirstNumner { get; set; }

        [Required]
        public int SecondNumber { get; set; }

        public DestinationModel Destination { get; set; }

    }
}