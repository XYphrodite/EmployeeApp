using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Client.Models;
using Client.Services;


namespace Client.ViewModels;

public partial class EmployeeManagingVM : ObservableObject
{
    public event Action<int?>? OpenEditRequested;

    private readonly ApiService _apiService;

    [ObservableProperty]
    private ObservableCollection<EmployeeListModel> employees = [];

    [ObservableProperty]
    private EmployeeListModel? selectedEmployee;

    public EmployeeManagingVM(ApiService apiService)
    {
        _apiService = apiService;
        LoadEmployeesCommand.Execute(null);
    }

    [RelayCommand]
    public  async Task LoadEmployeesAsync()
    {
        var list = await _apiService.GetEmployeesAsync();
        Employees = new ObservableCollection<EmployeeListModel>(list);
    }

    [RelayCommand]
    private void Add()
    {
        OpenEditRequested?.Invoke(null);
    }

    [RelayCommand(CanExecute = nameof(CanEditOrDelete))]
    private void Edit()
    {
        if (SelectedEmployee is not null)
            OpenEditRequested?.Invoke(SelectedEmployee.Id);
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

    private bool CanEditOrDelete() => SelectedEmployee != null;
}
