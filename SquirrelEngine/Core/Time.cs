using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquirrelEngine.Core
{
    internal static class Time
    {
        public static float TotalTime { get; private set; }
        public static float DeltaTime { get; private set; }

        public static void Update(float deltaTime)
        {
            DeltaTime = deltaTime;
            TotalTime += deltaTime;
        }
    }
}
