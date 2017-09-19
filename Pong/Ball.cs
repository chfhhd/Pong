using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Pong
{
    public class Ball
    {
        private const float speedCap = 3;
        private Vector2 startPosition;
        private Vector2 position;
        private Texture2D texture;
        private Rectangle collisionBox;
        private Vector2 moveVector;
        private float speedMultiplier;

        public Rectangle CollisionBox
        {
            get => collisionBox;
            set => collisionBox = value;
        }

        public Vector2 Position
        {
            get => position;
            set => position = value;
        }

        public Vector2 MoveVector
        {
            get => moveVector;
            set => moveVector = value;
        }

        public Ball(Vector2 startPosition, Texture2D texture, Vector2 size, Vector2 startVector, float speedMultiplier)
        {
            this.speedMultiplier = speedMultiplier;
            this.startPosition = startPosition;
            position = startPosition;
            moveVector = startVector;
            this.texture = texture;
            collisionBox = new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), Convert.ToInt32(size.X), Convert.ToInt32(size.Y));
        }

        public void Update(GameTime gameTime)
        {
            position += moveVector * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            collisionBox = new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), collisionBox.Width, collisionBox.Height);
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, collisionBox, Color.White);
        }

        /// <summary>
        /// Set moveVector and multiplies it by the speedMultiplier
        /// </summary>
        /// <param name="moveVector">New moveVector</param>
        public void ChangeMoveVector(Vector2 moveVector)
        {
            this.moveVector = moveVector * speedMultiplier;

            if (this.moveVector.X >= speedCap)
                this.moveVector = new Vector2(speedCap, this.moveVector.Y);
            if (moveVector.Y >= speedCap)
                this.moveVector = new Vector2(this.moveVector.X, speedCap);
        }

        /// <summary>
        /// Resets position and moveVector
        /// </summary>
        /// <param name="moveVector">New moveVector</param>
        public void Reset(Vector2 moveVector)
        {
            position = startPosition;
            collisionBox = new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), collisionBox.Width, collisionBox.Height);
            this.moveVector = moveVector;
        }
    }
}
