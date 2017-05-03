using Microsoft.Xna.Framework.Input;
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

        public static bool FullScreen = false;

        // GRID AND TILE
        public static int gridWidth = 36, gridHeight = 27;
        public static int TileSize = 46;
        public static int circleRadius = TileSize / 2;

        public static float ArcaneSpawnTime = 5, BoosterSpawnTime = 8;

        public static float MapFallOffRange = 0.8f;

        // WIZARDS
        public static float WizardStartSpeed = 150;
        public static float WizardStartSpeedLimit = WizardStartSpeed * 2;

        public static int PlayerSpawnRange = 9;


        // KEYBINDS
        public static Keys ActionKey1 = Keys.Space, ActionKey2 = Keys.Enter,
                           AlternateKey1 = Keys.LeftShift, AlternateKey2 = Keys.RightShift,
                           CancelKey = Keys.Escape,
                           UpKey1 = Keys.W, UpKey2 = Keys.Up,
                           DownKey1 = Keys.S, DownKey2 = Keys.Down,
                           LeftKey1 = Keys.A, LeftKey2 = Keys.Left,
                           RightKey1 = Keys.D, RightKey2 = Keys.Right;

        // AI RELATED
        public static float RangeCheck = TileSize * 4;
        public static int ObjectShootingRange = 7;

        // PHYSICS RELATED
        public static float DefaultFriction = 0.96f;
        public static float GroundFriction = 0.95f;
        public static float IceFriction = 0.98f;
        public static float SpellPushBack = 4;
    }
}
