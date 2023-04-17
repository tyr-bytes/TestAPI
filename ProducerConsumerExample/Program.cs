using Microsoft.Extensions.DependencyInjection;
using ProducerConsumerExample;

internal class Program
{
    private static void Main(string[] args)
    {
        var deviceConfiguration = new DeviceConfiguration
        {
            DeviceType = "Standard"
        };

        var pluginDirectory = "StandardFeatures";
        var plugins = PluginLoader.LoadPlugins(@"C:\Users\micha\source\repos\TestAPI\StandardFeatures\bin\Debug\net6.0\").ToList();
        Console.WriteLine("Loaded plugins");

        var serviceProvider = DependencyInjectionConfig.Configure(deviceConfiguration, plugins);
        using (var scope = serviceProvider.CreateScope())
        {
            var deviceController = scope.ServiceProvider.GetRequiredService<DeviceController>();
            deviceController.ExecutesFeatures();
        }


    }
}