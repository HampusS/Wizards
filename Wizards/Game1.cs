using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Wizards.GUI;
using Wizards.Managers;
using Wizards.TileGrid;
using Wizards.Utilities;

namespace Wizards
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public enum GameState
        {
            MainMenu,
            GamePlay,
            WinScreen,
        }
        public static GameState game = GameState.MainMenu;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GamePlayManager gameManager;
        MainMenuManager menuManager;
        TextObject title, winner, credit;
        string gameName = "Push Me Outside", m_sWinner = "The Winner is: ", m_sCredits = "Created by Hampus Stromsholm";

        public static bool ExitGame = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = Settings.windowWidth;
            graphics.PreferredBackBufferHeight = Settings.windowHeight;
            graphics.IsFullScreen = Settings.FullScreen;
            graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureManager.LoadTextures(GraphicsDevice, Content);
            gameManager = new GamePlayManager(Window);
            menuManager = new MainMenuManager();
            title = new TextObject(TextureManager.font, new Vector2(Settings.windowWidth / 2 - (TextureManager.font.MeasureString(gameName).X * 7) / 2, TextureManager.font.MeasureString(gameName).Y * 4), Color.LimeGreen, gameName, 7);
            winner = new TextObject(TextureManager.font, new Vector2(Settings.windowWidth / 2, Settings.windowHeight / 2), Color.LimeGreen, m_sWinner, 5);
            credit = new TextObject(TextureManager.font, new Vector2(TextureManager.font.MeasureString(m_sCredits).X * 1.25f, Settings.windowHeight - TextureManager.font.MeasureString(m_sCredits).Y * 2), Color.LimeGreen, m_sCredits, 2);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyMouseReader.Update();

            if (ExitGame)
                Exit();
            HandleGameState((float)gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            switch (game)
            {
                case GameState.MainMenu:
                    title.Draw(spriteBatch);
                    menuManager.Draw(spriteBatch);
                    break;
                case GameState.GamePlay:
                    gameManager.Draw(spriteBatch);
                    break;
                case GameState.WinScreen:
                    winner.Draw(spriteBatch);
                    credit.Draw(spriteBatch);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }


        void HandleGameState(float time)
        {
            switch (game)
            {
                case GameState.MainMenu:
                    menuManager.Update(time);

                    if (KeyMouseReader.keyState.IsKeyDown(Settings.CancelKey) && KeyMouseReader.oldKeyState.IsKeyUp(Settings.CancelKey))
                        Exit();
                    break;
                case GameState.GamePlay:
                    if (menuManager.flagForNewGame)
                    {
                        ResetGame();
                        menuManager.flagForNewGame = false;
                    }
                    gameManager.Update(time);
                    if (gameManager.GameOver())
                    {
                        string player = "Player 1";
                        if (gameManager.player1.myHP == 0)
                            player = "Player 2";
                        winner.myAppendedText = player;
                        winner.RefreshOrigin();
                        game = GameState.WinScreen;
                    }
                    if (KeyMouseReader.keyState.IsKeyDown(Settings.CancelKey) && KeyMouseReader.oldKeyState.IsKeyUp(Settings.CancelKey))
                        game = GameState.MainMenu;
                    break;
                case GameState.WinScreen:
                    if (KeyMouseReader.keyState.IsKeyDown(Settings.CancelKey) && KeyMouseReader.oldKeyState.IsKeyUp(Settings.CancelKey))
                        game = GameState.MainMenu;
                    break;
            }
        }

        public void ResetGame()
        {
            gameManager = new GamePlayManager(Window);
        }
    }
}
