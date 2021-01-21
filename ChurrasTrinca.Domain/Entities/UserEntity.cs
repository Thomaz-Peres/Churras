using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChurrasTrica.Domain.Entities
{
    public class UserEntity
    {
        public UserEntity() { }

        public UserEntity(int userId, string name, decimal contribuitonValue, bool isPaid, bool withDrink, bool withoutDrink)
        {
            this.UserID = userId;
            this.Name = name;
            this.ContributionValue = contribuitonValue;
            this.IsPaid = isPaid;
            this.WithDrink = withDrink;
            this.WithoutDrink = withoutDrink;
        }


        [Key]
        public int UserID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal ContributionValue { get; set; }

        [Required]
        public bool IsPaid { get; set; }

        [Required]
        public bool WithDrink { get; set; }
        
        [Required]
        public bool WithoutDrink { get; set; }
        
        
        [Required]
        public int ChurrasID { get; set; }
        public ChurrasEntity Churras { get; set; }
    }
}
