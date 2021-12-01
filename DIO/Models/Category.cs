using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DIO.Models
{
    public class Category
    {
        public int Id { get; set; }
        //[Display(Name = "Descrição")]
        [Required(ErrorMessage = "field Description is required")]
        public string Description { get; set; }
    }
}
