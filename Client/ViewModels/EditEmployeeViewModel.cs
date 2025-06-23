using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Client.Models;
using Client.Services;
using Shared.DTO;

namespace Client.ViewModels;

public partial class EditEmployeeViewModel : ObservableValidator
{
    private readonly ApiService _apiService;
    private Action? _onSaved;
    private int? _employeeId;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [RegularExpression(@"^(?=.*[A-Za-zА-Яа-я])[A-Za-zА-Яа-я0-9]+$", ErrorMessage = "Имя должно содержать хотя бы одну букву.")]
    private string firstname = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [RegularExpression(@"^(?=.*[A-Za-zА-Яа-я])[A-Za-zА-Яа-я0-9]+$", ErrorMessage = "Фамилия должна содержать хотя бы одну букву.")]
    private string surname = string.Empty;

    [ObservableProperty]
    private string? lastname;

    [ObservableProperty]
    private DateTime? birthday;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    private short positionId;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Зарплата должна быть положительным числом")]
    private int salary;

    [ObservableProperty]
    private bool isActive;

    [ObservableProperty]
    public ObservableCollection<PositionModel> positions = new();

    public bool IsEditMode => _employeeId.HasValue;

    public EditEmployeeViewModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task InitializeAsync(int? employeeId, Action onSaved)
    {
        _employeeId = employeeId;
        _onSaved = onSaved;
        await LoadPositionsAsync();

        if (IsEditMode)
        {
            var e = await _apiService.GetEmployeeAsync(_employeeId!.Value);
            Firstname = e.Firstname;
            Surname = e.Surname;
            Lastname = e.Lastname;
            Birthday = e.Birthday;
            PositionId = e.PositionId;
            Salary = e.Salary;
            IsActive = e.IsActive;
        }
        else
        {
            IsActive = true;
        }
    }

    private async Task LoadPositionsAsync()
    {
        var list = new ObservableCollection<PositionModel>();
        foreach (var position in await _apiService.GetPositionsAsync())
        {
            Positions.Add(new PositionModel { Id = position.Id, PositionName = position.PositionName });
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        ValidateAllProperties();
        if (HasErrors) return;

        var model = new EmployeeDto
        {
            Id = _employeeId ?? 0,
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
