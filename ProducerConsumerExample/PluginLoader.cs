using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumerExample
{
    public static class PluginLoader
    {
        public static IList<IFeature> LoadPlugins(string directory)
        {
            var pluginAssemblies = Directory.GetFiles(directory, "*.dll");
            var features = new List<IFeature>();
            foreach (var file in pluginAssemblies)
            {
                var assembly = Assembly.LoadFrom(file);
                var featureTypes = assembly.GetTypes().Where(t => !t.IsInterface && typeof(IFeature).IsAssignableFrom(t));
                foreach (var featureType in featureTypes)
                {
                    var featureInstance = Activator.CreateInstance(featureType) as IFeature;
                    features.Add(featureInstance);
                }
            }
            return features;
        }
    }
}
