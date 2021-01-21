using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChurrasTrica.Domain.Entities
{
    public class ChurrasEntity
    {
        public ChurrasEntity()
        { }

        public ChurrasEntity(int churrasId, string name, DateTime dateChurras, string description, decimal withDrink, decimal withoutDrink)
        {
            this.ChurrasID = churrasId;
            this.Name = name;
            this.Date = dateChurras;
            this.Description = description;
            this.WithDrink = withDrink;
            this.WithoutDrink = withoutDrink;
        }

        [Key]
        public int ChurrasID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Data")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; }
        
        public string Observations { get; set; }

        [Required]
        public decimal WithDrink { get; set; }

        [Required]
        public decimal WithoutDrink { get; set; }
        public virtual List<UserEntity> Participants { get; set; }
    }
}
