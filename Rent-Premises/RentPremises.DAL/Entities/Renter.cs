using System.ComponentModel.DataAnnotations.Schema;
using Rent_Premises.DAL.Entities.Base;

namespace Rent_Premises.DAL.Entities
{
    // Renter class inherits from BaseEntity<Guid> and represents a renter entity
    public class Renter : BaseEntity<Guid>
    {
        // UserId property represents the foreign key to the associated User entity
        public Guid UserId { get; set; }
        
        // Navigation property User represents the associated User entity
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        
        // RentedPremises property represents the list of premises rented by the renter
        public List<Premises> RentedPremises { get; set; } = new List<Premises>();
    }
}