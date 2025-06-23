using System.Windows;
using System.Windows.Input;
using EmployeeWpf.Services;
using EmployeeWpf.ViewModels;
using EmployeeWpf.Views;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeWpf.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly EmployeeManagingVM _viewModel;
    private readonly IServiceProvider _services;

    public MainWindow(EmployeeManagingVM vm, IServiceProvider services)
    {
        InitializeComponent();
        DataContext = vm;
        _viewModel = vm;
        _services = services;
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
