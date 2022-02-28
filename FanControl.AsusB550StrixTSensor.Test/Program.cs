using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanControl.AsusB550StrixTSensor.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FanControl.AsusB550StrixTSensor.TSensorPlugin plugin = new TSensorPlugin();
            PluginSensorsContainer container = new PluginSensorsContainer();

            plugin.Initialize();
            plugin.Load(container);

            List<Plugins.IPluginSensor> sensors = new List<Plugins.IPluginSensor>();

            sensors.AddRange(container.ControlSensors);
            sensors.AddRange(container.FanSensors);
            sensors.AddRange(container.TempSensors);

            for (int i = 0; i < 10000; i++)
            {
                for(int j = 0; j < sensors.Count; j++)
                {
                    Plugins.IPluginSensor sensor = sensors[j];

                    sensor.Update();
                    Console.WriteLine(string.Format("{0}: {1}", sensor.Name, sensor.Value));
                }

                System.Threading.Thread.Sleep(1000);
            }

            plugin.Close();
        }

        public class PluginSensorsContainer : Plugins.IPluginSensorsContainer
        {
            public List<Plugins.IPluginControlSensor> ControlSensors { get; } = new List<Plugins.IPluginControlSensor>();
            public List<Plugins.IPluginSensor> FanSensors { get; } = new List<Plugins.IPluginSensor>();
            public List<Plugins.IPluginSensor> TempSensors { get; } = new List<Plugins.IPluginSensor>();
        }
    }
}
