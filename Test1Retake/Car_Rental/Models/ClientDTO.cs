namespace Test1Retake.Models;

public class ClientDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public List<CarRentalDTO> CarsList { get; set; }
}