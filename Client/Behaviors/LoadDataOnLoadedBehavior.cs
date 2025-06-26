using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace Client.Behaviors
{
    public class LoadDataOnLoadedBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(LoadDataOnLoadedBehavior));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += OnLoaded;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Loaded -= OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (Command?.CanExecute(null) == true)
                Command.Execute(null);
        }
    }
} 