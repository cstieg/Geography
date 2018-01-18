﻿using System.ComponentModel.DataAnnotations;

namespace Cstieg.Geography
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(2)]
        public string IsoCode2 { get; set; }
    }
}
