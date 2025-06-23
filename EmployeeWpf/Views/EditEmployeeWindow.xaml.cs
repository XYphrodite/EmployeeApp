using System.Net.Http;
using System.Windows;
using EmployeeWpf.Services;
using EmployeeWpf.ViewModels;

namespace EmployeeWpf.Views;

public partial class EditEmployeeWindow : Window
{
    private readonly EditEmployeeViewModel _viewModel;

    public EditEmployeeWindow(EditEmployeeViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = _viewModel;
    }

    public async Task InitializeAsync(int? employeeId, Action onSaved)
    {
        await _viewModel.InitializeAsync(employeeId, onSaved);
    }
}
