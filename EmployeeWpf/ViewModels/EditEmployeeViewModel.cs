using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EmployeeWpf.Models;
using EmployeeWpf.Services;

namespace EmployeeWpf.ViewModels;

public partial class EditEmployeeViewModel : ObservableValidator
{
    private readonly ApiService _apiService;
    private readonly Action _onSaved;

    public bool IsEditMode { get; }

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    private string firstname = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    private string surname = string.Empty;

    [ObservableProperty]
    private string? lastname;

    [ObservableProperty]
    private DateTime? birthday;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    private short positionId;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    private int salary;

    [ObservableProperty]
    private bool isActive;

    [ObservableProperty]
    public ObservableCollection<PositionModel> positions = [];

    public int? EmployeeId { get; }

    public EditEmployeeViewModel(ApiService apiService, Action onSaved, int? employeeId = null)
    {
        _apiService = apiService;
        _onSaved = onSaved;
        EmployeeId = employeeId;
        IsEditMode = employeeId.HasValue;
        LoadDataCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        Positions = new ObservableCollection<PositionModel>(await _apiService.GetPositionsAsync());

        if (IsEditMode && EmployeeId.HasValue)
        {
            var e = await _apiService.GetEmployeeAsync(EmployeeId.Value);
            Firstname = e.Firstname;
            Surname = e.Surname;
            Lastname = e.Lastname;
            Birthday = e.Birthday;
            PositionId = e.PositionId;
            Salary = e.Salary;
            IsActive = e.IsActive;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        ValidateAllProperties();
        if (HasErrors) return;

        var model = new EmployeeModel
        {
            Id = EmployeeId ?? 0,
            Firstname = Firstname,
            Surname = Surname,
            Lastname = Lastname,
            Birthday = Birthday,
            PositionId = PositionId,
            Salary = Salary,
            IsActive = IsActive
        };

        if (IsEditMode)
            await _apiService.PutEmployeeAsync(model);
        else
            await _apiService.PostEmployeeAsync(model);

        _onSaved?.Invoke();
    }

    [RelayCommand]
    private void Cancel()
    {
        _onSaved?.Invoke();
    }
}
