using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizards.Utilities
{
    public static class Settings
    {
        // Window Back Buffer
        public static int windowWidth = 1600;
        public static int windowHeight = 1200;

        // GRID AND TILE
        public static int gridWidth = 36, gridHeight = 27;
        public static int TileSize = 46;
        public static int circleRadius = TileSize / 2;

        // WIZARDS
        public static float WizardStartSpeed = 150;
        public static float WizardStartSpeedLimit = WizardStartSpeed * 2;

        // AI RELATED
        public static float RangeCheck = TileSize * 4;

        // PHYSICS RELATED
        public static float DefaultFriction = 0.96f;
    }
}
