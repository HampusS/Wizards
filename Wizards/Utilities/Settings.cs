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
        public static int windowWidth = 1024;
        public static int windowHeight = 768;

        // GRID AND TILE
        public static int gridWidth = 32, gridHeight = 24;
        public static int TileSize = 32;
        public static int circleRadius = TileSize / 2;

        // WIZARDS
        public static float WizardStartSpeed = 150;
        public static float WizardStartSpeedLimit = WizardStartSpeed * 6;

        // AI RELATED
        public static float RangeCheck = TileSize * 4;

        // PHYSICS RELATED
        public static float DefaultFriction = 0.96f;
    }
}
