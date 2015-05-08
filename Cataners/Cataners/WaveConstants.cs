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
        public static float PLATFORM_WIDTH;
        public static float PLATFORM_HEIGHT;
        public static float WIDTH_TO_HEIGHT;
        public static float HEX_WIDTH;
        public static float HEX_SCALE_X;
        public static float HEX_SCALE_Y;
        public static float HEX_HEIGHT;
        public static float TRIANGLE_HEIGHT;
        public static float HEX_START_X;
        public static float HEX_START_Y;
        public static float ROLL_NUMBER_SCALE;
        public static float ROLL_NUMBER_WIDTH;
        public static float ROLL_NUMBER_HEIGHT;
        public static float SETTLEMENT_SCALE_X;
        public static float SETTLEMENT_SCALE_Y;
        public static float SETTLEMENT_WIDTH;
        public static float SETTLEMENT_HEIGHT;
        public static float CENTERWIDTH;
        public static float CENTERHEIGHT;


        public static void setWaveValues()
        {
             PLATFORM_WIDTH = (float)WaveServices.Platform.ScreenWidth;
             PLATFORM_HEIGHT = (float)WaveServices.Platform.ScreenHeight;
             WIDTH_TO_HEIGHT = (PLATFORM_WIDTH) / (PLATFORM_WIDTH);
             HEX_WIDTH = ((PLATFORM_WIDTH) / 8.0f) / WIDTH_TO_HEIGHT;
             HEX_SCALE_X = HEX_WIDTH / 220.0f;
             HEX_SCALE_Y = HEX_WIDTH * (1.1681818181f) / 257.0f;
             HEX_HEIGHT = (HEX_WIDTH * 1.168181818f);
             TRIANGLE_HEIGHT = HEX_HEIGHT * 0.2723735409f;
             HEX_START_X = ((PLATFORM_WIDTH/ 2.0f) - ((HEX_WIDTH * 3) / 2));
             HEX_START_Y = ((PLATFORM_HEIGHT) / 2.0f) - ((3 * HEX_HEIGHT) - (4 * TRIANGLE_HEIGHT));
             ROLL_NUMBER_SCALE = HEX_WIDTH / (float)(2 * 50);
             ROLL_NUMBER_WIDTH = 50 * ROLL_NUMBER_SCALE;
             ROLL_NUMBER_HEIGHT = 50 * ROLL_NUMBER_SCALE;
             SETTLEMENT_SCALE_X = (HEX_WIDTH / 10) / 684;
             SETTLEMENT_SCALE_Y = (HEX_HEIGHT / 10) / 559;
             SETTLEMENT_WIDTH = 684 * SETTLEMENT_SCALE_X;
             SETTLEMENT_HEIGHT = 684 * SETTLEMENT_SCALE_Y;
             CENTERWIDTH = PLATFORM_WIDTH/2;
             CENTERHEIGHT = PLATFORM_HEIGHT/2;
        }
    }

}
