using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizards.Utilities
{
    public static class Settings
    {
        // GRID AND TILE
        public static int gridWidth = 25, gridHeight = 15;
        public static int TileSize = 32;
        public static int circleRadius = TileSize / 2;

        // WIZARDS
        public static float WizardStartSpeed = 30;
        public static float WizardStartSpeedLimit = WizardStartSpeed * 6;

        // AI RELATED
        public static float RangeCheck = TileSize * 4;

        // PHYSICS RELATED
        public static float DefaultFriction = 0.02f;
    }
}
