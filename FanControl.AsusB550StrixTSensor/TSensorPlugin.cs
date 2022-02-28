using FanControl.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanControl.AsusB550StrixTSensor
{
    public class TSensorPlugin : FanControl.Plugins.IPlugin
    {
        OpenHardwareMonitor.Hardware.Computer computer;

        public string Name
        {
            get
            {
                return "BLNJ: Asus B550 Strix T-Sensor Plugin";
            }
        }

        public void Initialize()
        {
            this.computer = new OpenHardwareMonitor.Hardware.Computer();
            this.computer.MainboardEnabled = true;
            this.computer.CPUEnabled = false;
            this.computer.RAMEnabled = false;
            this.computer.GPUEnabled = false;
            this.computer.FanControllerEnabled = false;
            this.computer.HDDEnabled = false;

            this.computer.Open();
        }

        public void Load(IPluginSensorsContainer _container)
        {
            OpenHardwareMonitor.Hardware.IHardware mainboard = null;
            for (int i = 0; i < this.computer.Hardware.Length; i++)
            {
                OpenHardwareMonitor.Hardware.IHardware[] hardwareCollection = this.computer.Hardware;
                
                for(int j = 0; j < hardwareCollection.Length; j++)
                {
                    OpenHardwareMonitor.Hardware.IHardware hardware = hardwareCollection[j];
                    if(hardware.HardwareType == OpenHardwareMonitor.Hardware.HardwareType.Mainboard &&
                        hardware.Name.StartsWith("ASUS ROG STRIX B550"))
                    {
                        mainboard = hardware;
                        break;
                    }
                }
            }

            TSensor sensor = new TSensor(mainboard);
            _container.TempSensors.Add(sensor);
        }

        public void Close()
        {
            this.computer.Close();
        }
    }
}
