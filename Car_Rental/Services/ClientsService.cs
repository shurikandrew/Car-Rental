using Test1Retake.Models;
using Test1Retake.Repositories;

namespace Test1Retake.Services;

public class ClientsService : IClientsService
{
    private readonly IClientsRepository _clientsRepository;

    public ClientsService(IClientsRepository clientsRepository)
    {
        _clientsRepository = clientsRepository;
    }

    public ClientDTO GetClientInfo(int id)
    {
        return _clientsRepository.GetInfo(id);
    }

    public void AddNewRentalInfo(RentalDTO rental)
    {
        _clientsRepository.AddNewRentalInfo(rental);
    }
}