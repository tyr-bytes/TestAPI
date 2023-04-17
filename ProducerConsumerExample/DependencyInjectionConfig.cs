using Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumerExample
{
    public class DeviceConfiguration
    {
        public string? DeviceType { get; set; } = default;
    }


    public static class DependencyInjectionConfig
    {
        public static ServiceProvider Configure(
            DeviceConfiguration deviceConfig, List<IFeature> features)
        {
            if (deviceConfig == null) { throw new ArgumentNullException(nameof(deviceConfig)); }
            var services = new ServiceCollection();
            services.AddSingleton<List<IFeature>>(features);
            services.AddTransient<DeviceController>();

            return services.BuildServiceProvider();
        }
    }

    public class DeviceController
    {
        private readonly List<IFeature> _features;
        public DeviceController(List<IFeature> features) { _features = features; }
        public void ExecutesFeatures()
        {
            foreach (var feature in _features)
            {
                feature.Execute();
            }
        }
    }
}
