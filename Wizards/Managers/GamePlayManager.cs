using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizards.GameObjects;
using Wizards.GameObjects.Environment;
using Wizards.Interfaces;
using Wizards.TileGrid;
using Wizards.Utilities;

namespace Wizards.Managers
{
    class GamePlayManager
    {
        Grid grid;
        PlayerWizard player1, player2;

        List<PowerUp> powerUps = new List<PowerUp>();
        List<Spell> spells = new List<Spell>();
        GameWindow window;

        public GamePlayManager(GameWindow window)
        {
            grid = new Grid(TextureManager.square, Settings.TileSize, Settings.gridWidth, Settings.gridHeight);
            powerUps.Add(new PowerUp(TextureManager.circle, new Vector2(300, 250), Settings.circleRadius));
            powerUps[0].myPosition = grid.ReturnTileCenter(powerUps[0].myPosition);
            player1 = new PlayerWizard(TextureManager.circle, new Vector2(50, window.ClientBounds.Height / 2), Settings.circleRadius);
            player1.InitializeKeyBindings(Keys.Space, Keys.Q, Keys.E, Keys.W, Keys.A, Keys.S, Keys.D, Color.LightPink);
            player1.InitializePhysics(10, 1);
            player2 = new PlayerWizard(TextureManager.circle, new Vector2(window.ClientBounds.Width - 50, window.ClientBounds.Height / 2), Settings.circleRadius);
            player2.InitializeKeyBindings(Keys.M, Keys.K, Keys.L, Keys.Up, Keys.Left, Keys.Down, Keys.Right, Color.LightGreen);
            player2.InitializePhysics(30, 1);
            this.window = window;
        }

        public void Update(float time)
        {
            player1.Update(time);
            player2.Update(time);

            foreach (PowerUp pup in powerUps)
            {
                PlayerWizard tempWiz = player1;
                if (Vector2.Distance(player1.myPosition, pup.myPosition) > Vector2.Distance(player2.myPosition, pup.myPosition))
                    tempWiz = player2;
                if (pup.isInRange(tempWiz.myPosition, 4))
                {
                    if (pup.isReadyToShoot())
                        CreateSpell(pup.myPosition, tempWiz.myFuturePosition);

                    pup.AnimateMe(time);
                }
                if (player1.CheckCircleCollision(pup))
                    pup.isAlive = false;
                if (player2.CheckCircleCollision(pup))
                    pup.isAlive = false;
            }

            foreach (Spell spell in spells)
            {
                if (player1.CheckCircleCollision(spell))
                    spell.GiveImpulse(player1);
                if (player2.CheckCircleCollision(spell))
                    spell.GiveImpulse(player2);
            }

            foreach (PowerUp pup in powerUps)
            {
                pup.Update(time);
            }

            foreach (Spell spell in spells)
            {
                spell.Update(time);
            }

            for (int i = spells.Count - 1; i >= 0; i--)
            {
                if (!spells[i].isAlive)
                    spells.RemoveAt(i);
            }

            for (int i = powerUps.Count - 1; i >= 0; i--)
            {
                if (!powerUps[i].isAlive)
                    powerUps.RemoveAt(i);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            grid.DrawOccupiedTile(player1.myPosition, player1.myFuturePosition);
            grid.DrawOccupiedTile(player2.myPosition, player2.myFuturePosition);
            grid.Draw(spriteBatch);
            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);
            foreach (PowerUp pUp in powerUps)
            {
                pUp.Draw(spriteBatch);
            }
            foreach (Spell spell in spells)
            {
                spell.Draw(spriteBatch);
            }
            player1.DrawOutOfBoundsArrow(spriteBatch, window);
            player2.DrawOutOfBoundsArrow(spriteBatch, window);

        }

        public void CreateSpell(Vector2 position, Vector2 target)
        {
            Vector2 direction = target - position;
            direction.Normalize();
            spells.Add(new Spell(TextureManager.circle, position, 16, direction));
        }
    }
}
