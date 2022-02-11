using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private CoinSprite[] coins;
        private PumpkinSprite pumpkin;
        SpriteFont spriteFont;
        private int coinsLeft;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            System.Random rand = new System.Random();
            coins = new CoinSprite[]
                {
                new CoinSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height)),
                new CoinSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height)),
                new CoinSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height)),
                new CoinSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height)),
                new CoinSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height)),
                new CoinSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height)),
                new CoinSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height))
                };
            pumpkin = new PumpkinSprite();
            coinsLeft = coins.Length;
            //winningScreen = new WinningScreen();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            foreach (var coin in coins) coin.LoadContent(Content);
            pumpkin.LoadContent(Content);
            spriteFont = Content.Load<SpriteFont>("arial");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            pumpkin.Update(gameTime);
            // TODO: Add your update logic here
            foreach(var coin in coins)
            {
                coin.Update(gameTime, _graphics);
                if (!coin.Collected && coin.Bounds.CollidesWith(pumpkin.Bounds))
                {
                    coin.Collected = true;
                    coinsLeft--;
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (coinsLeft == 0)
            {
                _spriteBatch.DrawString(spriteFont, "You Win!", new Vector2((GraphicsDevice.Viewport.Width / 2) - 110, (GraphicsDevice.Viewport.Height / 2) - 96), Color.White);
            }
            else
            {
                foreach (var coin in coins) coin.Draw(gameTime, _spriteBatch);
                pumpkin.Draw(gameTime, _spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
