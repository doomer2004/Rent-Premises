using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Rent_Premises.DAL.Entities.Base;

namespace Rent_Premises.DAL.Entities;

public class Premises : BaseEntity<Guid>
{
    public string Name { get; set; } = String.Empty;
    
    
    public Guid TypeOfPremisesId { get; set; }
    [ForeignKey("TypeOfPremisesId")]
    public TypeOfPremises? Type { get; set; }
    public bool IsAvailable { get; set; }  
    
    public Guid LessorId { get; set; }
    [ForeignKey(nameof(LessorId))]
    public Lessor? Lessor { get; set; }
    
    public Guid? RenterId { get; set; } = null;
    
    [ForeignKey(nameof(RenterId))]
    public Renter? Renter { get; set; }
   
}