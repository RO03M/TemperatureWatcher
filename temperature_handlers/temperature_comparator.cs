using System.Drawing;

namespace temperatures.src.temperature_handlers {
    class temperature_comparator {
        public static object[,] temperatureColors = new object[,] {
            { Color.LightGreen, 30 },
            { Color.Green, 45 },
            { Color.Orange, 50 },
            { Color.OrangeRed, 55 },
            { Color.Red, 60 }
        };

        public static Color GetColorByTemperature(float? temperature) {
            if (temperature == null) return Color.HotPink;
            for (int i = 0; i < temperatureColors.Length; i++) {
                if (temperature < (int)temperatureColors[i, 1]) {
                    int index = i - 1;
                    if (index < 0) break;
                    else return (Color)temperatureColors[index, 0];
                }
            }
            return Color.LightSkyBlue;
        }

    }
}
