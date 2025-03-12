namespace Test1Retake.Models;

public class RentalDTO
{
    public Client Client { get; set; }
    public int CarId { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}