using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Glossary
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Term { get; set; }
        [Required]
        public string Definition { get; set; }
        
        public DateTime Created { get; set; }
        public bool isDeleted { get; set; }

    }
}
