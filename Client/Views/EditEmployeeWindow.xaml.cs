using System.Net.Http;
using System.Windows;
using Client.Services;
using Client.ViewModels;

namespace Client.Views;

public partial class EditEmployeeWindow : Window
{
    private readonly EditEmployeeViewModel _viewModel;

    public EditEmployeeWindow(EditEmployeeViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = _viewModel;
    }

    public async Task InitializeAsync(int? employeeId, Func<Task> onSaved)
    {
        await _viewModel.InitializeAsync(employeeId, async () =>
        {
            await onSaved();
            this.Close();
        });
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
