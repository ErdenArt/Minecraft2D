using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Minecraft2D_GameJam
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        GameManager gameManager;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = GLOBALS.WindowSize.X;
            _graphics.PreferredBackBufferHeight = GLOBALS.WindowSize.Y;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GLOBALS.Graphics = _graphics;
            GLOBALS.Content = Content;
            GLOBALS.GraphicsDevice = GraphicsDevice;
            GLOBALS.SpriteBatch = _spriteBatch;
            GLOBALS.Pixel = GLOBALS.CreateWhitePixel(GraphicsDevice);
            GLOBALS.Font = Content.Load<SpriteFont>("Font");
            GLOBALS.Game = this;
            gameManager = new GameManager(this);
            // TODO: use this.Content to load your game content here
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            GLOBALS.Time = gameTime;
            Input.Update();
            gameManager.Update(gameTime);

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            gameManager.Draw(_spriteBatch, gameTime);

            base.Draw(gameTime);
        }
    }
}
