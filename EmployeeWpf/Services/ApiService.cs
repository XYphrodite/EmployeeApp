using System.Net.Http;
using EmployeeWpf.Models;
using Shared.DTO;

namespace EmployeeWpf.Services;

public class ApiService
{
    private readonly HttpClient _client;
    const string BASE_ADDRESS = "";
    public ApiService()
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri(BASE_ADDRESS);
    }
    internal async Task DeleteEmployeeAsync(int id)
    {
        var resp = await _client.SendAsync();
        throw new NotImplementedException();
    }

    internal async Task<IEnumerable<EmployeeModel>> GetEmployeesAsync()
    {
        var resp = await _client.SendAsync();
        throw new NotImplementedException();
    }

    internal async Task GetEmployeeAsync(int value)
    {
        throw new NotImplementedException();
    }

    internal async Task<IEnumerable<PositionModel>> GetPositionsAsync()
    {
        throw new NotImplementedException();
    }

    internal async Task PutEmployeeAsync(EmployeeModel model)
    {
        throw new NotImplementedException();
    }

    internal async Task PostEmployeeAsync(EmployeeModel model)
    {
        throw new NotImplementedException();
    }
}
