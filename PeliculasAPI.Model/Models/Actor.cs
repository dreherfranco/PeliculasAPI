﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PeliculasAPI.Model.Models
{
    public class Actor
    {
        public int Id { get; set; }
        [StringLength(120)]
        [Required]
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Photo { get; set; }
    }
}
