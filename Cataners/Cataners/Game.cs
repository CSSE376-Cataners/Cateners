#region Using Statements
using System;
using WaveEngine.Common;
using WaveEngine.Common.Graphics;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;
using System.Collections.Generic;
using WaveEngine.Common.Graphics.VertexFormats;
using WaveEngine.Common.Math;
using WaveEngine.Components.Animation;
using WaveEngine.Framework.Graphics;
using WaveEngine.Materials;
using WaveEngine.Components.Graphics2D;
using System.Diagnostics.CodeAnalysis;
#endregion

namespace Cataners
{
    [ExcludeFromCodeCoverage]
    public class Game : WaveEngine.Framework.Game
    {
        public override void Initialize(IApplication application)
        {
            base.Initialize(application);

            // ViewportManager is used to automatically adapt resolution to fit screen size
            ViewportManager vm = WaveServices.ViewportManager;
            vm.Activate(1910, 1000, ViewportManager.StretchMode.Uniform);
            ScreenContext screenContext = new ScreenContext(new MyScene());
            WaveServices.ScreenContextManager.To(screenContext);
        }
    }
}
