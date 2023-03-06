// See https://aka.ms/new-console-template for more information





using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

using DemoConfiguration.Console;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");


var envioroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");



using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((_, configuration) =>
    {
        configuration.Sources.Clear();

        configuration
        .SetBasePath(Directory.GetCurrentDirectory())

        .AddJsonFile("appsettings.json", false)

        .AddJsonFile($"appsettings.{envioroment}.json", true)

        .AddEnvironmentVariables();


        string urlKeyVault = "";


        if (!string.IsNullOrWhiteSpace(urlKeyVault))
        {
            var secretClient = new SecretClient(new(urlKeyVault), new DefaultAzureCredential());
            configuration.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
        }


        



    })
    .ConfigureServices((builder, services) =>
    {

        var connectionString = builder.Configuration.GetConnectionString("Default");


        services.AddTransient<IUserService, UserService>();

    })
    .Build();


var userService = host.Services.GetRequiredService<IUserService>();


var defaultPassword = userService.GetDefaultPassword();


Console.WriteLine("Hello World!");

