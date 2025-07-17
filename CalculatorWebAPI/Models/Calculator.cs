using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculatorWebAPI.Models
{
    [Table("Calculator")]
    public class Calculator
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int operand1 {  get; set; }
        [Required]
        public int operand2 { get; set; }
        [Required]
        public string operation {  get; set; }=string.Empty;
        public int result {  get; set; }
    }
}
