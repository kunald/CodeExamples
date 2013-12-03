using System;
using System.ComponentModel.DataAnnotations;

namespace Insight123.Dto
{
    public class PartDto
    {
        public Guid Id { get; set; }
        [Required]
        public string PartNumber { get; set; }
        public string PartDescription { get; set; }
        [Required]
        public int UnitOfMeasure { get; set; }
        public int SalesLeadTime { get; set; }
    }
}
