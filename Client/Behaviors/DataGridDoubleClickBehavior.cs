using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.Behaviors;

public class DataGridDoubleClickBehavior : Behavior<DataGrid>
{
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(DataGridDoubleClickBehavior));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.MouseDoubleClick += AssociatedObject_MouseDoubleClick;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.MouseDoubleClick -= AssociatedObject_MouseDoubleClick;
    }

    private void AssociatedObject_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var dataGrid = sender as DataGrid;
        if (dataGrid?.SelectedItem != null)
        {
            if (Command?.CanExecute(dataGrid.SelectedItem) ?? false)
                Command.Execute(dataGrid.SelectedItem);
        }
    }
}
