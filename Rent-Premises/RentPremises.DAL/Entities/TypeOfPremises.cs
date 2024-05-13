using Rent_Premises.DAL.Entities.Base;

namespace Rent_Premises.DAL.Entities;

public class TypeOfPremises : BaseEntity<Guid>
{
    public string Name { get; set; } = string.Empty;
}