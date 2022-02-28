using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanControl.AsusB550StrixTSensor
{
    public class TSensor : FanControl.Plugins.IPluginSensor
    {
        OpenHardwareMonitor.Hardware.IHardware mainboard;

        public string Id
        {
            get
            {
                return "T-Sensor ID";
            }
        }

        public string Name
        {
            get
            {
                return "T-Sensor";
            }
        }

        private float? value = null;
        public float? Value
        {
            get
            {
                return value;
            }
        }

        internal TSensor(OpenHardwareMonitor.Hardware.IHardware mainboard)
        {
            this.mainboard = mainboard;
        }

        public void Update()
        {
            if(mainboard != null)
            {
                mainboard.SubHardware[0].Update();

                for(int i = 0; i < mainboard.SubHardware[0].Sensors.Length; i++)
                {
                    var sensor = mainboard.SubHardware[0].Sensors[i];

                    if(sensor != null &&
                        sensor.Identifier.ToString() == "/lpc/nct6798d/temperature/5")
                    {
                        value = sensor.Value;
                    }
                }
            }
        }
    }
}
