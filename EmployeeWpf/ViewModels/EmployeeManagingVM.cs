using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EmployeeWpf.Models;
using EmployeeWpf.Services;


namespace EmployeeWpf.ViewModels;

public partial class EmployeeManagingVM : ObservableObject
{
    private readonly ApiService _apiService;

    [ObservableProperty]
    private ObservableCollection<EmployeeModel> employees = [];

    [ObservableProperty]
    private EmployeeModel? selectedEmployee;

    public EmployeeManagingVM(ApiService apiService)
    {
        _apiService = apiService;
        LoadEmployeesCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadEmployeesAsync()
    {
        var list = await _apiService.GetEmployeesAsync();
        Employees = new ObservableCollection<EmployeeModel>(list);
    }

    [RelayCommand(CanExecute = nameof(CanEditOrDelete))]
    private void Edit()
    {
        // Открыть окно редактирования
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

    private bool CanEditOrDelete() => SelectedEmployee != null;
}
