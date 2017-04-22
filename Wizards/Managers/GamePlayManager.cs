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
        GameWindow window;
        Grid grid;
        PlayerWizard player1, player2;

        List<PowerUp> powerUps = new List<PowerUp>();
        List<Spell> spells = new List<Spell>();

        public GamePlayManager(GameWindow window)
        {
            grid = new Grid(TextureManager.square, Settings.TileSize, Settings.gridWidth, Settings.gridHeight);
            powerUps.Add(new PowerUp(TextureManager.circle, grid.getGridCenter(), Settings.circleRadius));
            player1 = new PlayerWizard(TextureManager.circle, grid.RandomizePosFromCenter(9), Settings.circleRadius);
            player1.TurnToVector(grid.getGridCenter());
            player1.SetKeyBindings(Keys.Space, Keys.W, Keys.A, Keys.S, Keys.D, Color.LightPink);
            player1.SetPhysics(1, 0.8f);
            player2 = new PlayerWizard(TextureManager.circle, grid.RandomizePosFromCenter(9), Settings.circleRadius);
            player2.TurnToVector(grid.getGridCenter());
            player2.SetKeyBindings(Keys.M, Keys.Up, Keys.Left, Keys.Down, Keys.Right, Color.LightGreen);
            player2.SetPhysics(1, 0.8f);
            this.window = window;
        }

        public void Update(float time)
        {
            UpdateObjects(time);

            Collisions(time);

            RemoveDeadObjects();
        }

        private void Collisions(float time)
        {
            grid.SetFrictionToObject(player1);
            grid.SetFrictionToObject(player2);
            if (Calculate.CheckCircleCollision(player1, player2))
                Calculate.SolveToMovingCircleCollision(player1, player2);

            if(player1.flagInputShot())
                if(player1.isReadyToShoot())
                {
                    spells.Add(new Spell(TextureManager.circle, player1.myPosition, 16, player1));
                }

            foreach (PowerUp pup in powerUps)
            {
                PlayerWizard tempWiz = player1;
                if (Vector2.Distance(player1.myPosition, pup.myPosition) > Vector2.Distance(player2.myPosition, pup.myPosition))
                    tempWiz = player2;
                //Set a range in # of tiles
                if (pup.isInRange(tempWiz.myPosition, 7))
                {
                    pup.TurnToVector(tempWiz.myPosition);
                    if (pup.isReadyToShoot())
                        CreateSpell(pup, pup.myPosition);
                    pup.AnimateMe(time);
                }
                else
                    pup.ResetScale();
                if (Calculate.CheckCircleCollision(pup, player1))
                {
                    //pup.isAlive = false;
                    Calculate.SolveToStaticCircleCollision(player1, pup);
                }
                if (Calculate.CheckCircleCollision(pup, player2))
                    Calculate.SolveToStaticCircleCollision(player2, pup);
            }

            foreach (Spell spell in spells)
            {
                if (spell.getMyParent() != player1)
                {
                    if (Calculate.CheckCircleCollision(spell, player1))
                    {
                        Calculate.SolveToMovingCircleCollision(player1, spell);
                        spell.isAlive = false;
                    }
                }
                if (Calculate.CheckCircleCollision(spell, player2))
                {
                    Calculate.SolveToMovingCircleCollision(player2, spell);
                    spell.isAlive = false;
                }
            }
        }

        private void UpdateObjects(float time)
        {
            player1.Update(time);
            player2.Update(time);

            foreach (PowerUp pup in powerUps)
            {
                pup.Update(time);
            }

            foreach (Spell spell in spells)
            {
                spell.Update(time);
            }
        }

        private void RemoveDeadObjects()
        {
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

        public void CreateSpell(GameObject parent, Vector2 spawnPos)
        {
            spells.Add(new Spell(TextureManager.circle, spawnPos, 16, parent));
        }
    }
}
