using Test1Retake.Models;

namespace Test1Retake.Repositories;

public interface IClientsRepository
{
    public ClientDTO GetInfo(int id);
    public void AddNewRentalInfo(RentalDTO rental);
}