using System.Data.SqlClient;
using Test1Retake.Models;

namespace Test1Retake.Repositories;

public class ClientsRepository : IClientsRepository
{
    private IConfiguration _configuration;

    public ClientsRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public ClientDTO GetInfo(int id)
    {
        using SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        using SqlCommand command = new SqlCommand();
        connection.Open();
        command.Connection = connection;
        command.CommandText = "SELECT c.ID, c.FirstName, c.LastName, c.Address, car.VIN, color.Name AS Color, model.Name AS Model, cr.DateFrom, cr.DateTo, cr.TotalPrice " +
                              "FROM clients c  " +
                              "JOIN car_rentals cr ON cr.ClientID = c.ID  " +
                              "JOIN cars car ON car.ID = cr.CarID " +
                              "JOIN colors color ON car.ColorID = color.ID " +
                              "JOIN models model ON car.ModelID = model.ID " +
                              "WHERE c.ID = @Id";
        command.Parameters.AddWithValue("@Id", id);
        var reader = command.ExecuteReader();
        var result = new ClientDTO();
        var flag = true;
        while (reader.Read())
        {
            if (flag)
            {
                result = new ClientDTO()
                {
                    Id = id,
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    Address = reader["Address"].ToString(),
                    CarsList = new List<CarRentalDTO>()
                };
                flag = false;
            }

            var carRent = new CarRentalDTO()
            {
                Vin = reader["VIN"].ToString(),
                Color = reader["Color"].ToString(),
                Model = reader["Model"].ToString(),
                DateFrom = (DateTime)reader["DateFrom"],
                DateTo = (DateTime)reader["DateTo"],
                TotalPrice = (int)reader["TotalPrice"]
            };
            result.CarsList.Add(carRent);
        }

        return result;
    }

    public void AddNewRentalInfo(RentalDTO rental)
    {
        using SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        using SqlCommand command = new SqlCommand();
        connection.Open();
        command.Connection = connection;
        command.CommandText = "INSERT INTO clients(FirstName, LastName, Address) VALUES(@FirstName, @LastName, @Address)";
        command.Parameters.AddWithValue("@FirstName", rental.Client.FirstName);
        command.Parameters.AddWithValue("@LastName", rental.Client.LastName);
        command.Parameters.AddWithValue("@Address", rental.Client.Address);
        var rows = command.ExecuteNonQuery();
        connection.Close();
        
        connection.Open();
        command.Connection = connection;
        command.CommandText = "SELECT PricePerDay " +
                              "FROM cars c " +
                              "WHERE c.ID = @Id";
        command.Parameters.AddWithValue("@Id", rental.CarId);
        var car = command.ExecuteReader();
        car.Read();
        var TotalValue = (int)car["PricePerDay"] * (rental.DateTo.Day - rental.DateFrom.Day);
        connection.Close();
        
        connection.Open();
        command.Connection = connection;
        command.CommandText = "SELECT ID FROM clients WHERE ID >= ALL(SELECT ID FROM clients)";

        var client = command.ExecuteReader();
        client.Read();
        var ID = (int)client["ID"];
        connection.Close();
    }
}