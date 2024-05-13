namespace RentPremises.Common.Models.DTOs.Premises;

public class PremisesDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public Guid TypeOfPremisesId{ get; set; }
    
}