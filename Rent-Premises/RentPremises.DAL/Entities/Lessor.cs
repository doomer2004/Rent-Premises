using System.ComponentModel.DataAnnotations.Schema;
using Rent_Premises.DAL.Entities.Base;

namespace Rent_Premises.DAL.Entities
{
    // Lessor class inherits from BaseEntity<Guid> and represents a lessor entity
    public class Lessor : BaseEntity<Guid>
    {
        // UserId property represents the foreign key to the associated User entity
        public Guid UserId { get; set; }
        
        // Navigation property User represents the associated User entity
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}