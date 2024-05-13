using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Rent_Premises.DAL.Entities.Base;

namespace Rent_Premises.DAL.Entities
{
    // Premises class inherits from BaseEntity<Guid> and represents a premises entity
    public class Premises : BaseEntity<Guid>
    {
        // Name property represents the name of the premises
        public string Name { get; set; } = string.Empty;
        
        // TypeOfPremisesId property represents the foreign key to the associated TypeOfPremises entity
        public Guid TypeOfPremisesId { get; set; }
        
        // Navigation property Type represents the associated TypeOfPremises entity
        [ForeignKey("TypeOfPremisesId")]
        public TypeOfPremises? Type { get; set; }
        
        // IsAvailable property indicates whether the premises are available for rent
        public bool IsAvailable { get; set; }  
        
        // LessorId property represents the foreign key to the associated Lessor entity
        public Guid LessorId { get; set; }
        
        // Navigation property Lessor represents the associated Lessor entity
        [ForeignKey(nameof(LessorId))]
        public Lessor? Lessor { get; set; }
        
        // RenterId property represents the foreign key to the associated Renter entity
        public Guid? RenterId { get; set; } = null;
        
        // Navigation property Renter represents the associated Renter entity
        [ForeignKey(nameof(RenterId))]
        public Renter? Renter { get; set; }
    }
}