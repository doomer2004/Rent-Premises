using System.ComponentModel.DataAnnotations;

namespace Rent_Premises.DAL.Entities.Base;

public class BaseEntity<T> where T : struct
{
    [Key]
    public T Id { get; set; }
}