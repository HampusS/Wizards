using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizards.GUI;
using Wizards.Utilities;

namespace Wizards.Managers
{
    class MainMenuManager
    {
        List<TextButton> buttons;
        int bttn_Select;

        public bool flagForNewGame
        {
            get;
            set;
        }

        public MainMenuManager()
        {
            buttons = new List<TextButton>();
            bttn_Select = 1;
            buttons.Add(new TextButton(TextureManager.font, new Vector2(Settings.windowWidth / 2, Settings.windowHeight / 2 + 50), Color.DarkGreen, "Exit", 2));
            buttons.Add(new TextButton(TextureManager.font, new Vector2(Settings.windowWidth / 2, Settings.windowHeight / 2), Color.DarkGreen, "Play", 2));
        }

        public void Update(float time)
        {
            HandleSelected();
            for (int i = 0; i < buttons.Count; i++)
            {
                if (i == bttn_Select)
                    buttons[i].isSelected = true;
                else
                    buttons[i].isSelected = false;


                buttons[i].Update(time);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Draw(spriteBatch);
            }
        }

        public void HandleSelected()
        {
            if (KeyMouseReader.keyState.IsKeyDown(Settings.UpKey1) || KeyMouseReader.keyState.IsKeyDown(Settings.UpKey2))
            {
                if (bttn_Select < 1)
                    bttn_Select++;
            }
            else if (KeyMouseReader.keyState.IsKeyDown(Settings.DownKey1) || KeyMouseReader.keyState.IsKeyDown(Settings.DownKey2))
            {
                if (bttn_Select > 0)
                    bttn_Select--;
            }


            if (KeyMouseReader.KeyPressed(Settings.ActionKey1) || KeyMouseReader.KeyPressed(Settings.ActionKey2))
            {
                switch (bttn_Select)
                {
                    case 0:
                        Game1.ExitGame = true;
                        break;
                    case 1:
                        flagForNewGame = true;
                        Game1.game = Game1.GameState.GamePlay;
                        break;
                }
            }
        }
    }
}
