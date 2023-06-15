
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateSpProject;
 [Table("employees")]
    public class Employee
    {
        [Required]
        [Column(Order=1)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        [MaxLength(50)]
        
        public string? Name { get; set; }

        [Column("salary")]
        [Required]
        public int Salary { get; set; }
    }