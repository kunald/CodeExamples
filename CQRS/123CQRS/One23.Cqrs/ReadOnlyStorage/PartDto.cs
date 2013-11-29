using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insight.Cqrs.ReadOnlyStorage
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
