using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pong
{
    public class Player
    {
        private PlayerIndex playerInex;
        private Keys upKey;
        private Keys downKey;
        private Slider slider;
        private int points;
        private Rectangle endZone;

        public event EventHandler OnLeft; 

        public int Points
        {
            get => points;
            set => points = value;
        }

        public Slider Slider
        {
            get => slider;
        }

        public Player(PlayerIndex playerIndex, Vector2 position, Texture2D texture, Vector2 size, Keys upKey, Keys downKey, Rectangle endZone, float fieldMinY, float fieldMaxY)
        {
            this.playerInex = playerIndex;
            this.slider = new Slider(position, texture, size, fieldMinY, fieldMaxY);
            this.points = 0;
            this.upKey = upKey;
            this.downKey = downKey;
            this.endZone = endZone;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            GamePadState padState = GamePad.GetState(playerInex);

            slider.ResetMoveVector();
            if(state.IsKeyDown(upKey) || padState.ThumbSticks.Left.Y < 0)
            {
                slider.MoveUp();
            }
            if(state.IsKeyDown(downKey) || padState.ThumbSticks.Right.Y > 0)
            {
                slider.MoveDown();
            }

            slider.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            slider.Draw(spriteBatch);
        }

        /// <summary>
        /// Checks Collision between ball and player slider
        /// </summary>
        /// <param name="ball">Ball ball</param>
        public void Collision(Ball ball)
        {
            if (ball.CollisionBox.Intersects(slider.CollisionBox))
            {
                float width = ball.MoveVector.X > 0 ? -ball.CollisionBox.Width : ball.CollisionBox.Width; //prevents the ball from stucking in a slider

                ball.Position = new Vector2(slider.CollisionBox.X + width, ball.Position.Y);
                Vector2 sideVec = slider.GetSideVector(ball.CollisionBox);
                ball.ChangeMoveVector((ball.MoveVector * -1) + (slider.MoveVector / 2) + sideVec);
            }

            if (ball.CollisionBox.Intersects(endZone)) //ball out of field
            {
                OnLeft?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
