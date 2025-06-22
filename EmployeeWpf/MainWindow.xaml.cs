using System.Windows;
using EmployeeWpf.ViewModels;

namespace EmployeeWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(EmployeeManagingVM vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}