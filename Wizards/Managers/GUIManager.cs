using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizards.GameObjects;
using Wizards.GUI;
using Wizards.Utilities;

namespace Wizards.Managers
{
    class GUIManager
    {
        TextObject m_toPlayer1, m_toPlayer2,
                    gui_p1Life, gui_p2Life,
                    gui_p1ArcanePower,
                    gui_p2ArcanePower;

        Wizard player1, player2;

        Vector2 player1Pos, player2Pos;
        string m_sLife = "Life: ";
        string m_sPower = "Arcane Power: ";

        public Rectangle ArcaneBar(Wizard player, Vector2 position)
        {
            return new Rectangle((int)position.X, (int)position.Y + 15, BarWidth(player), 35);
        }

        public int BarWidth(Wizard player)
        {
            return (int)(100 * player.StrengthPercent());
        }

        public Rectangle BarFrame(Wizard player, Vector2 position)
        {
            return new Rectangle((int)position.X, (int)position.Y, (int)(120), 45);
        }

        public GUIManager(Wizard player1, Wizard player2)
        {
            this.player1 = player1;
            this.player2 = player2;
            InitializeGUI(player1, player2);
            player1Pos = GivePosition(TextureManager.font.MeasureString(m_sPower).X * 3.25f, 6);
            player2Pos = GivePosition(TextureManager.font.MeasureString(m_sPower).X * 3.25f, 54);
        }

        public Vector2 GivePosition(float x, float modifier)
        {
            return new Vector2(x, TextureManager.font.MeasureString("Player 1").Y * modifier);
        }

        private void InitializeGUI(Wizard player1, Wizard player2)
        {
            // Player 1
            m_toPlayer1 = new TextObject(TextureManager.font, GivePosition(10, 1), player1.myColor, "Player 1", 3);
            gui_p1Life = new TextObject(TextureManager.font, GivePosition(10, 4), player1.myColor, m_sLife, 3);
            gui_p1Life.myAppendedText = player1.myHP.ToString();
            gui_p1ArcanePower = new TextObject(TextureManager.font, GivePosition(10, 6), player1.myColor, m_sPower, 3);

            // Player 2
            m_toPlayer2 = new TextObject(TextureManager.font, GivePosition(10, 50), player2.myColor, "Player 2", 3);
            gui_p2Life = new TextObject(TextureManager.font, GivePosition(10, 52), player2.myColor, m_sLife, 3);
            gui_p2Life.myAppendedText = player2.myHP.ToString();
            gui_p2ArcanePower = new TextObject(TextureManager.font, GivePosition(10, 54), player2.myColor, m_sPower, 3);
        }

        public void RefreshGUI(Wizard player1, Wizard player2)
        {
            gui_p1Life.myAppendedText = player1.myHP.ToString();
            gui_p2Life.myAppendedText = player2.myHP.ToString();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            m_toPlayer1.Draw(spriteBatch);
            m_toPlayer2.Draw(spriteBatch);
            gui_p1Life.Draw(spriteBatch);
            gui_p2Life.Draw(spriteBatch);
            gui_p1ArcanePower.Draw(spriteBatch);
            gui_p2ArcanePower.Draw(spriteBatch);
            spriteBatch.Draw(TextureManager.frame, BarFrame(player1, new Vector2(player1Pos.X - 10, player1Pos.Y + 10)), Color.White);
            spriteBatch.Draw(TextureManager.frame, BarFrame(player2, new Vector2(player2Pos.X - 10, player2Pos.Y + 10)), Color.White);

            spriteBatch.Draw(TextureManager.solidSquare, ArcaneBar(player1, player1Pos), Color.MediumPurple);
            spriteBatch.Draw(TextureManager.solidSquare, ArcaneBar(player2, player2Pos), Color.MediumPurple);
        }
    }
}
