using System.Net.Http;
using System.Net.Http.Json;
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



    public async Task<IEnumerable<EmployeeListModel>> GetEmployeesAsync()
        => await _client.GetFromJsonAsync<IEnumerable<EmployeeListModel>>("employee");

    public async Task<EmployeeModel> GetEmployeeAsync(int id)
        => await _client.GetFromJsonAsync<EmployeeModel>($"employee/{id}");

    public async Task PostEmployeeAsync(EmployeeModel employee)
        => await _client.PostAsJsonAsync("employee", employee);

    public async Task PutEmployeeAsync(EmployeeModel employee)
        => await _client.PutAsJsonAsync($"employee/{employee.Id}", employee);

    public async Task DeleteEmployeeAsync(int id)
        => await _client.DeleteAsync($"employee/{id}");

    public async Task<IEnumerable<PositionDto>> GetPositionsAsync()
        => await _client.GetFromJsonAsync<IEnumerable<PositionDto>>("position");
}
