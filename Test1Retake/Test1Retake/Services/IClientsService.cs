using Test1Retake.Models;

namespace Test1Retake.Services;

public interface IClientsService
{
    public ClientDTO GetClientInfo(int id);
    public void AddNewRentalInfo(RentalDTO rental);
}