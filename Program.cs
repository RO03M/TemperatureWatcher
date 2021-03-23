using System;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

//using static temperatures.src.utils.math_comparators;
using static temperatures.src.temperature_handlers.sensors_info;
using static temperatures.src.temperature_handlers.temperature_comparator;

namespace temperatures.src {
    class Program : Form {

        public const String APP_NAME = "temp_tracer";
        public Font DEFAULT_FONT = new Font("Arial", 16, FontStyle.Regular);
        public Color TRANSPARENT_COLOR = Color.FromArgb(10, 10, 10);
        public Color FONT_COLOR = Color.FromArgb(0, 0, 0);

        public float delayTime = 2f;//time to get new data from the sensors
        public float? cpuTemp = 69;
        public float? gpuTemp = 69;
        public Label cpuTempLabel = new Label();
        public Label gpuTempLabel = new Label();

        public Program() {
            Application.EnableVisualStyles();
            cpuTemp = CPUTemperature();
            gpuTemp = GPUTemperature();
            this.Text = APP_NAME;
            this.TopMost = true;//some apps like Minecraft on fullscreen make topmost useless, so we need to modify dll code to make always on top -> https://stackoverflow.com/questions/683330/how-to-make-a-window-always-stay-on-top-in-net
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            this.BackColor = TRANSPARENT_COLOR;
            this.TransparencyKey = TRANSPARENT_COLOR;

            cpuTempLabel.Text = "CPU: " + cpuTemp + "°C";
            cpuTempLabel.Font = DEFAULT_FONT;
            cpuTempLabel.ForeColor = FONT_COLOR;
            cpuTempLabel.BackColor = TRANSPARENT_COLOR;
            cpuTempLabel.Size = new Size(150, 50);

            gpuTempLabel.Text = "GPU: " + cpuTemp + "°C";
            gpuTempLabel.Font = DEFAULT_FONT;
            gpuTempLabel.ForeColor = FONT_COLOR;
            gpuTempLabel.BackColor = TRANSPARENT_COLOR;
            gpuTempLabel.Location = new Point(0, 50);
            gpuTempLabel.Size = new Size(150, 50);

            this.Controls.Add(cpuTempLabel);
            this.Controls.Add(gpuTempLabel);

            Thread thread = new Thread(new ThreadStart(this.Loop));
            thread.Start();
            Application.Run(this);
        }

        static void Main() {
            Program program = new Program();
        }

        public void Loop() {
            while (true) {
                Thread.Sleep(TimeSpan.FromSeconds(delayTime));
                cpuTemp = CPUTemperature();
                gpuTemp = GPUTemperature();
                MethodInvoker update = delegate {
                    cpuTempLabel.Text = "CPU: " + cpuTemp + "°C";
                    cpuTempLabel.ForeColor = GetColorByTemperature(cpuTemp);
                    gpuTempLabel.Text = "GPU: " + gpuTemp + "°C";
                    gpuTempLabel.ForeColor = GetColorByTemperature(gpuTemp);
                    this.BringToFront();
                };
                this.Invoke(update);
            }
        }
    }
}
