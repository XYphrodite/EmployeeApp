using System.Windows;
using Client.ViewModels;

namespace Client.Views;

public partial class EmployeeManagingWindow : Window
{
    private readonly EmployeeManagingVM _viewModel;

    public EmployeeManagingWindow(EmployeeManagingVM viewModel, IServiceProvider services)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = _viewModel;
    }

}
