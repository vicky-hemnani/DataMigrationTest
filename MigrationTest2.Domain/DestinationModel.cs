using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationTest2.Domain
{
    public class DestinationModel
    {
        public int Id { get; set; }


        public SourceModel Source { get; set; }

        [ForeignKey("SourceId")]
        public int SourceId { get; set; }

        [Required]
        public int Total { get; set; }
    }
}