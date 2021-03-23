using System;

using OpenHardwareMonitor.Hardware;

namespace temperatures.src.temperature_handlers {
    class sensors_info {

        public static int? GPUTemperature() {
            Computer computer = new Computer();
            computer.GPUEnabled = true;
            computer.Open();
            int sensorsLength = computer.Hardware[0].Sensors.Length;
            for (int i = 0; i < sensorsLength; i++) {
                if (computer.Hardware[0].Sensors[i].SensorType.ToString() == "Temperature") return Convert.ToInt32(computer.Hardware[0].Sensors[i].Value);
            }

            return null;
        }

        public static int? CPUTemperature() {
            Computer computer = new Computer();
            computer.CPUEnabled = true;
            computer.Open();

            int sensorsLength = computer.Hardware[0].Sensors.Length;

            for (int i = 0; i < sensorsLength; i++) {
                if (computer.Hardware[0].Sensors[i].SensorType.ToString() == "Temperature" && computer.Hardware[0].Sensors[i].Name.ToString() == "CPU Package") return Convert.ToInt32(computer.Hardware[0].Sensors[i].Value);
            }

            return null;
        }

    }
}
