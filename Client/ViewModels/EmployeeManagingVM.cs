using Client.Models;
using Client.Services;
using Client.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;


namespace Client.ViewModels;

public partial class EmployeeManagingVM : ObservableObject
{
    private readonly ApiService _apiService;
    private readonly IServiceProvider _services;
    [ObservableProperty]
    private ObservableCollection<EmployeeListModel> employees = [];

    [ObservableProperty]
    private EmployeeListModel? selectedEmployee;

    public EmployeeManagingVM(ApiService apiService, IServiceProvider services)
    {
        _apiService = apiService;
        _services = services;
        LoadEmployeesCommand.Execute(null);
    }

    [RelayCommand]
    public  async Task LoadEmployeesAsync()
    {
        Employees = new ObservableCollection<EmployeeListModel>(await _apiService.GetEmployeesAsync());
    }

    [RelayCommand]
    private void Add()
    {
        OpenEditWindow(null);
    }

    [RelayCommand(CanExecute = nameof(CanEditOrDelete))]
    private void Edit()
    {
        if (SelectedEmployee is not null)
            OpenEditWindow(SelectedEmployee.Id);
    }


    [RelayCommand(CanExecute = nameof(CanEditOrDelete))]
    private async Task DeleteAsync()
    {
        if (SelectedEmployee != null)
        {
            await _apiService.DeleteEmployeeAsync(SelectedEmployee.Id);
            await LoadEmployeesAsync();
        }
    }

    partial void OnSelectedEmployeeChanged(EmployeeListModel? value)
    {
        EditCommand.NotifyCanExecuteChanged();
        DeleteCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand] //double click
    private void OpenEdit(EmployeeListModel employee)
    {
        OpenEditWindow(employee.Id);
    }

    private bool CanEditOrDelete() => SelectedEmployee != null;

    private async void OpenEditWindow(int? employeeId)
    {
        var window = _services.GetRequiredService<EditEmployeeWindow>();
        await window.InitializeAsync(employeeId, LoadEmployeesAsync);
        window.ShowDialog();
    }
}
