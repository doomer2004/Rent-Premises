using System.ComponentModel.DataAnnotations.Schema;
using Rent_Premises.DAL.Entities.Base;

namespace Rent_Premises.DAL.Entities;

public class Renter : BaseEntity<Guid>
{
    public Guid UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
    public List<Premises> RentedPremises { get; set; } = new List<Premises>();
}