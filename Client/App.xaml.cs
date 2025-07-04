﻿using Client.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace Client;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private IHost? _host;

    protected override void OnStartup(StartupEventArgs e)
    {
        _host = Host.CreateDefaultBuilder()
                    .ConfigureClientServices()
                    .Build();

        _host.Start();

        var mainWindow = _host.Services.GetRequiredService<EmployeeManagingWindow>();
        mainWindow.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        if (_host != null)
            await _host.StopAsync();

        base.OnExit(e);
    }
}
