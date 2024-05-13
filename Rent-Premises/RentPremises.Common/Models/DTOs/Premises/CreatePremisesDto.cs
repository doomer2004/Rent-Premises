namespace RentPremises.Common.Models.DTOs.Premises;

public class CreatePremisesDto
{
    public string Name { get; set; }
    public Guid TypeOfPremisesId { get; set; }
    public Guid LessorId { get; set; }
}