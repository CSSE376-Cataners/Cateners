using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveEngine.Framework.Services;

namespace Cataners
{
    public static class WaveConstants
    {
        public static float WIDTH_TO_HEIGHT = ((float)WaveServices.Platform.ScreenWidth) / ((float)WaveServices.Platform.ScreenHeight);
        public static float HEX_WIDTH = (((float)WaveServices.Platform.ScreenWidth) / 8.0f) / WIDTH_TO_HEIGHT;
        public static float HEX_SCALE_X = HEX_WIDTH / 220.0f;
        public static float HEX_SCALE_Y = HEX_WIDTH * ((float)1.1681818181) / 257.0f;
        public static float HEX_HEIGHT = (HEX_WIDTH * (float)1.168181818);
        public static float TRIANGLE_HEIGHT = HEX_HEIGHT * (float)0.2723735409;
        public static float HEX_START_X = (((float)WaveServices.Platform.ScreenWidth) / 2.0f) - ((HEX_WIDTH * 3) / 2);
        public static float HEX_START_Y = (((float)WaveServices.Platform.ScreenHeight) / 2.0f) - ((3 * HEX_HEIGHT) - (4 * TRIANGLE_HEIGHT));
        public static float ROLL_NUMBER_SCALE = HEX_WIDTH / (2 * 50);
        public static float ROLL_NUMBER_WIDTH = 50 * ROLL_NUMBER_SCALE;
        public static float ROLL_NUMBER_HEIGHT = 50 * ROLL_NUMBER_SCALE;
        public static float SETTLEMENT_SCALE_X = (HEX_WIDTH / 10) / 684;
        public static float SETTLEMENT_SCALE_Y = (HEX_HEIGHT / 10) / 559;
        public static float SETTLEMENT_WIDTH = 684 * SETTLEMENT_SCALE_X;
        public static float SETTLEMENT_HEIGHT = 684 * SETTLEMENT_SCALE_Y;
    }
}
