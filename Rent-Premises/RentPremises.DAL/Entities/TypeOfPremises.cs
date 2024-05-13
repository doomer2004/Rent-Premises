using Rent_Premises.DAL.Entities.Base;

namespace Rent_Premises.DAL.Entities
{
    // TypeOfPremises class inherits from BaseEntity<Guid> and represents a type of premises entity
    public class TypeOfPremises : BaseEntity<Guid>
    {
        // Name property represents the name of the type of premises
        public string Name { get; set; } = string.Empty;
    }
}