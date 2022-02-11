using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameProject1
{
    public class CoinSprite
    {
        public Vector2 MovementVector { get; set; }

        private float scale;

        private Vector2 position;

        private Texture2D texture;

        private BoundingCircle bounds;

        public bool Collected { get; set; } = false;

        public Vector2 Position { get; set; }

        public BoundingCircle Bounds => bounds;

        /// <summary>
        /// constructor for coins
        /// </summary>
        /// <param name="position">position where sprite is drawn in game</param>
        public CoinSprite(Vector2 position)
        {
            this.MovementVector = new Vector2(1, 0);
            this.position = position;
            scale = .2f;
            float temp = (scale * 167)/2;
            this.bounds = new BoundingCircle(position - new Vector2(-temp, -temp), temp);
        }

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("PictoCoin");
        }

        /// <summary>
        /// Updates the sprite's position based on user input
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            position += MovementVector;
            if (position.X >= graphics.GraphicsDevice.Viewport.Width || position.X <= 0)
            {
                MovementVector = new Vector2(MovementVector.X * -1, 0);
            }
            bounds.Center = new Vector2(position.X + ((.2f*167)/2),position.Y);
        }

        /// <summary>
        /// Draws the animated sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Collected)
            {
                return;
            }

            //spriteBatch.Draw(texture, position, null, Color.White);
            spriteBatch.Draw(texture, position, null, Color.White, 0, new Vector2(0,0), scale, SpriteEffects.None, 0);
        }
    }
}
