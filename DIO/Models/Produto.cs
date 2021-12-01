using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DIO.Models
{
    public class Produto
    {
        public int Id { get; set; }
        //[Display(Name = "Descrição")]
        public string Description { get; set; }
        [Range(1,10,ErrorMessage ="Value out of range (1 - 10)")]
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        
        public Category Category { get; set; }

    }
}
