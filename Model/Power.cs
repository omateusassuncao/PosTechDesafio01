using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PosTechDesafio01.Model
{
    public class Power
    {
        [Key]
        //public int Id { get; set; }
        public string Name { get; set; }
    }
}
