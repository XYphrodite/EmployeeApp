using System.Windows;
using System.Windows.Input;
using Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Views;

public partial class EmployeeManagingWindow : Window
{
    private readonly EmployeeManagingVM _viewModel;
    private readonly IServiceProvider _services;

    public EmployeeManagingWindow(EmployeeManagingVM viewModel, IServiceProvider services)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = _viewModel;
        _services = services;

        _viewModel.OpenEditRequested += OpenEditWindow;
    }

    private async void OpenEditWindow(int? employeeId)
    {
        var window = _services.GetRequiredService<EditEmployeeWindow>();
        await window.InitializeAsync(employeeId, async () => await _viewModel.LoadEmployeesAsync());
        window.ShowDialog();
    }

    private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (_viewModel.SelectedEmployee != null)
        {
            OpenEditWindow(_viewModel.SelectedEmployee.Id);
        }
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        OpenEditWindow(null);
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        if (_viewModel.SelectedEmployee != null)
        {
            OpenEditWindow(_viewModel.SelectedEmployee.Id);
        }
    }
}
