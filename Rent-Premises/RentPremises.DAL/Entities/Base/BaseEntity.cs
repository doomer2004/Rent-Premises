using System.ComponentModel.DataAnnotations;

namespace Rent_Premises.DAL.Entities.Base
{
    // BaseEntity<T> class serves as a base class for all entities with a generic type parameter T representing the type of the entity's primary key
    public class BaseEntity<T> where T : struct
    {
        // Id property represents the primary key of the entity
        [Key]
        public T Id { get; set; }
    }
}