using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace APP.Models
{
   
    public class SPAHeader
    {
        public SPAHeader(DateTime dateCreated)
        { 
            DateCreated = DateTime.SpecifyKind(dateCreated, DateTimeKind.Local);  
        }

        [Key]
        public int Id { get; set; }

        public string Guid { get; set; }

        [Required]
        public string Creater { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
        public string Status { get; set; }
        public string StatusCode { get; set; }
        public string CFTTeam { get; set; }

        [Required]
        public string DateRequired { get; set; }

        [Required]
        public string ModelandDescription { get; set; }

        [Required]
        public string DeviationInstruction { get; set; }

        [Required]
        public int QtyRequired { get; set; }
        public string ModelDescription { get; set; }
        public string BillofMaterials { get; set; }
        public string SpecialAssemblyInstr { get; set; }
        public string PackagingRequirements { get; set; }
        public string TestRequirements { get; set; }
    }
}
