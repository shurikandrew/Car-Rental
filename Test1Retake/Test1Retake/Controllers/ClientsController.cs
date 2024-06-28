using Microsoft.AspNetCore.Mvc;
using Test1Retake.Models;
using Test1Retake.Services;

namespace Test1Retake.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientsService _clientsService;

    public ClientsController(IClientsService clientsService)
    {
        _clientsService = clientsService;
    }

    [HttpGet("{id:int}")]
    public IActionResult GetInfo(int id)
    {
        var result = _clientsService.GetClientInfo(id);
        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddNewRentalInfo(RentalDTO rental)
    {
        _clientsService.AddNewRentalInfo(rental);
        return Ok();
    }
    
}