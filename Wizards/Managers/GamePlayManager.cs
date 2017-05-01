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
using Wizards.GUI;
using Wizards.Interfaces;
using Wizards.TileGrid;
using Wizards.Utilities;

namespace Wizards.Managers
{
    class GamePlayManager
    {
        GameWindow window;
        Grid grid;
        public PlayerWizard player1, player2;

        TextObject m_toPlayer1, m_toPlayer2,
            gui_p1Life, gui_p2Life,
            gui_p1ArcanePower, gui_p2ArcanePower;

        List<PickUp> pickUps = new List<PickUp>();
        List<Spell> spells = new List<Spell>();
        List<Obstacle> obstacles = new List<Obstacle>();
        Random rnd;
        float pickupTimer;

        public GamePlayManager(GameWindow window)
        {
            rnd = new Random();
            grid = new Grid(TextureManager.square, Settings.TileSize, Settings.gridWidth, Settings.gridHeight);
            grid.SetObstacles(obstacles);
            AddArcaneObject();

            InitializePlayers();
            SetPlayerGUI();
            this.window = window;
        }

        public void Update(float time)
        {
            Perception(time);

            RemoveDeadObjects();

            UpdateObjects(time);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            grid.Draw(spriteBatch);
            foreach (PickUp pUp in pickUps)
            {
                pUp.Draw(spriteBatch);
            }
            foreach (Spell spell in spells)
            {
                spell.Draw(spriteBatch);
            }

            foreach (Obstacle obst in obstacles)
            {
                obst.Draw(spriteBatch);
            }
            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);
            m_toPlayer1.Draw(spriteBatch);
            m_toPlayer2.Draw(spriteBatch);
            gui_p1Life.Draw(spriteBatch);
            gui_p2Life.Draw(spriteBatch);
            gui_p1ArcanePower.Draw(spriteBatch);
            gui_p2ArcanePower.Draw(spriteBatch);
        }

        private void AddArcaneObject()
        {
            pickUps.Add(new PickUp(TextureManager.circle, grid.RandomizePosAwayFromCenter(rnd.Next(2, 10)), Settings.circleRadius, rnd.Next(5, 10), PickUp.myType.ArcaneBoost));
        }

        private void AddRandomBooster()
        {
            int temp = rnd.Next(0, 5);
            switch (temp)
            {
                case 2:
                    pickUps.Add(new PickUp(TextureManager.circle, grid.RandomizePosAwayFromCenter(rnd.Next(2, 10)), Settings.circleRadius, rnd.Next(3, 6), PickUp.myType.MassBoost));
                    break;
                case 3:
                    pickUps.Add(new PickUp(TextureManager.circle, grid.RandomizePosAwayFromCenter(rnd.Next(2, 10)), Settings.circleRadius, rnd.Next(3, 6), PickUp.myType.RapidFireBoost));
                    break;
                case 4:
                    pickUps.Add(new PickUp(TextureManager.circle, grid.RandomizePosAwayFromCenter(rnd.Next(2, 10)), Settings.circleRadius, rnd.Next(3, 6), PickUp.myType.SpeedBoost));
                    break;
            }
        }

        private void SetPlayerGUI()
        {
            string m_sLife = "Life: ";
            string m_sPower = "Arcane: ";

            // Player 1
            m_toPlayer1= new TextObject(TextureManager.font, new Vector2(10, TextureManager.font.MeasureString("Player 1").Y), player1.myColor, "Player 1", 3);
            gui_p1Life = new TextObject(TextureManager.font, new Vector2(10, TextureManager.font.MeasureString(m_sLife).Y * 4), player1.myColor, m_sLife, 3);
            gui_p1Life.myAppendedText = player1.myHP.ToString();
            gui_p1ArcanePower = new TextObject(TextureManager.font, new Vector2(10, TextureManager.font.MeasureString(m_sPower).Y * 6), player1.myColor, m_sPower, 3);
            gui_p1ArcanePower.myAppendedText = player1.myStrength.ToString();

            // Player 2
            m_toPlayer2 = new TextObject(TextureManager.font, new Vector2(10, Settings.windowHeight - TextureManager.font.MeasureString("Player 2").Y * 8), player2.myColor, "Player 2", 3);
            gui_p2Life = new TextObject(TextureManager.font, new Vector2(10, Settings.windowHeight - TextureManager.font.MeasureString(m_sLife).Y * 6), player2.myColor, m_sLife, 3);
            gui_p2Life.myAppendedText = player2.myHP.ToString();
            gui_p2ArcanePower = new TextObject(TextureManager.font, new Vector2(10, Settings.windowHeight - TextureManager.font.MeasureString(m_sPower).Y * 4), player2.myColor, m_sPower, 3);
            gui_p2ArcanePower.myAppendedText = player2.myStrength.ToString();
        }

        private void InitializePlayers()
        {
            player1 = new PlayerWizard(TextureManager.circle, grid.RandomizePosAwayFromCenter(9), Settings.circleRadius);
            player1.TurnToVector(grid.getGridCenter());
            player1.SetKeyBindings(Keys.Space, Keys.W, Keys.A, Keys.S, Keys.D, Color.LightPink);

            player2 = new PlayerWizard(TextureManager.circle, grid.RandomizePosAwayFromCenter(9), Settings.circleRadius);
            player2.TurnToVector(grid.getGridCenter());
            player2.SetKeyBindings(Keys.M, Keys.Up, Keys.Left, Keys.Down, Keys.Right, Color.LightGreen);
        }

        public void WizardDeathSequence(Wizard obj, TextObject objGUI)
        {
            if (obj.isInVoid)
            {
                obj.myPosition = grid.RandomizePosAwayFromCenter(9);
                obj.TurnToVector(grid.getGridCenter());
                obj.myVelocity *= 0;
                obj.myHP--;

                objGUI.myAppendedText = obj.myHP.ToString();
                obj.isInVoid = false;
            }
        }

        private void PlayerShoots(PlayerWizard player)
        {
            if (player.flagInputShot())
            {
                if (player.isReadyToShoot())
                {
                    spells.Add(new Spell(TextureManager.circle, player.myPosition, 8, player));
                }
            }
        }

        private void ObtainObjectAttribute(Wizard obj, PickUp pup)
        {
            if (Calculate.CheckCircleCollision(pup, obj))
            {
                pup.isAlive = false;

                switch (pup.m_mtType)
                {
                    case PickUp.myType.ArcaneBoost:
                        obj.AddStrength();
                        gui_p1ArcanePower.myAppendedText = player1.myStrength.ToString();
                        break;
                    case PickUp.myType.MassBoost:
                        obj.BoostMass();
                        obj.myTaunts = Wizard.Taunts.Solid;
                        obj.DisplayTaunt = true;
                        break;
                    case PickUp.myType.RapidFireBoost:
                        obj.BoostFireRate();
                        obj.DisplayTaunt = true;
                        obj.myTaunts = Wizard.Taunts.Spray;
                        break;
                    case PickUp.myType.SpeedBoost:
                        obj.BoostSpeed();
                        obj.DisplayTaunt = true;
                        obj.myTaunts = Wizard.Taunts.Speed;
                        break;
                }
            }
        }

        public bool GameOver()
        {
            if (player1.myHP == 0 || player2.myHP == 0)
                return true;
            return false;
        }

        private void Perception(float time)
        {
            if (Calculate.CheckCircleCollision(player1, player2))
                Calculate.SolveToMovingCircleCollision(player1, player2, 1);
            grid.UpdatePlayerTile(player1);
            grid.UpdatePlayerTile(player2);
            WizardDeathSequence(player1, gui_p1Life);
            WizardDeathSequence(player2, gui_p2Life);
            SpawnPickUps(time);

            PlayerShoots(player1);
            PlayerShoots(player2);

            foreach (PickUp pup in pickUps)
            {
                ObjectShootsPlayer(pup);
                ObtainObjectAttribute(player1, pup);
                ObtainObjectAttribute(player2, pup);
            }

            foreach (Obstacle obst in obstacles)
            {
                StaticCollision(player1, obst);
                StaticCollision(player2, obst);
            }

            foreach (Spell spell in spells)
            {
                if (spell.getMyParent() != null)
                {
                    if (spell.getMyParent() == player2)
                    {
                        if (MovingCollision(player1, spell, player2.myStrength))
                        {
                            spell.isAlive = false;
                            player2.ResetStrength();
                            gui_p2ArcanePower.myAppendedText = player2.myStrength.ToString();
                            player2.DisplayTaunt = true;
                            player2.myTaunts = Wizard.Taunts.HowBout;
                        }
                    }
                    else if (spell.getMyParent() == player1)
                    {
                        if (MovingCollision(player2, spell, player1.myStrength))
                        {
                            spell.isAlive = false;
                            player1.ResetStrength();
                            gui_p1ArcanePower.myAppendedText = player1.myStrength.ToString();
                            player1.DisplayTaunt = true;
                            player1.myTaunts = Wizard.Taunts.HowBout;
                        }
                    }
                }
                else
                {
                    if (MovingCollision(player1, spell, 1))
                        spell.isAlive = false;
                    else if (MovingCollision(player2, spell, 1))
                        spell.isAlive = false;
                }

                foreach (Obstacle obst in obstacles)
                {
                    if (StaticCollision(spell, obst))
                        spell.m_gOwner = null;
                }
            }
        }

        private bool MovingCollision(MovingObject lhs, MovingObject rhs, float strength)
        {
            if (Calculate.CheckCircleCollision(lhs, rhs))
            {
                Calculate.SolveToMovingCircleCollision(lhs, rhs, strength);
                return true;
            }
            return false;
        }

        private bool StaticCollision(MovingObject moving, GameObject still)
        {
            if (Calculate.CheckCircleCollision(moving, still))
            {
                Calculate.SolveToStaticCircleCollision(moving, still);
                return true;
            }
            return false;
        }

        private void SpawnPickUps(float time)
        {
            pickupTimer += time;

            if (pickupTimer > rnd.Next(8, 15))
            {
                pickupTimer = 0;
                AddRandomBooster();
            }

            if (pickUps.Count == 0)
            {
                if (rnd.Next(0, 2) == 1)
                {
                    AddArcaneObject();
                    AddArcaneObject();
                }
                else
                    AddArcaneObject();
            }
        }

        private void ObjectShootsPlayer(GameObject obj)
        {
            if (obj.CanShoot)
            {
                PlayerWizard tempWiz = player1;

                if (Vector2.Distance(player1.myPosition, obj.myPosition) > Vector2.Distance(player2.myPosition, obj.myPosition))
                    tempWiz = player2;

                //Set a range in # of tiles
                if (obj.isInRange(tempWiz.myPosition, 7))
                {
                    obj.TurnToVector(tempWiz.myPosition);
                    if (obj.isReadyToShoot())
                        spells.Add(new Spell(TextureManager.circle, obj.myPosition, 16, tempWiz.myPosition));
                }
            }
        }

        private void UpdateObjects(float time)
        {
            player1.Update(time);
            player2.Update(time);
            foreach (PickUp pup in pickUps)
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

            for (int i = pickUps.Count - 1; i >= 0; i--)
            {
                if (!pickUps[i].isAlive)
                    pickUps.RemoveAt(i);
            }
        }
    }
}
