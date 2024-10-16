using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.Models.Common
{
    public class BaseEntity
    {
        [Key]
        [Required]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Created By")]
        public Guid CreatedBy { get; set; }
    }
}
