using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace InputExample
{
    class InputManager
    {
        KeyboardState currentKeyboardState;
        KeyboardState priorKeyboardState;
        MouseState currentMouseState;
        MouseState previousMouseState;
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        public Vector2 Direction { get; private set; }

        public bool Warp { get; private set; }

        public bool Exit { get; private set; }

        public void Update(GameTime gameTime)
        {
            priorKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            previousGamePadState = currentGamePadState;
            currentGamePadState = GamePad.GetState(0);

            if(currentGamePadState.Buttons.Back == ButtonState.Pressed || currentKeyboardState.IsKeyDown(Keys.Escape))
            {
                Exit = true;
            }


            #region directional input

            //Gamepad
            Direction = currentGamePadState.ThumbSticks.Left * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;

            /*
            //Mouse
            Direction = new Vector2(
                currentMouseState.X * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds,
                currentMouseState.Y * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds
            );*/
            

            //Keyboard
            if (currentKeyboardState.IsKeyDown(Keys.Left) ||
                currentKeyboardState.IsKeyDown(Keys.A))
            {
                Direction += new Vector2(-100 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right) ||
                currentKeyboardState.IsKeyDown(Keys.D))
            {
                Direction += new Vector2(100 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up) ||
                currentKeyboardState.IsKeyDown(Keys.W))
            {
                Direction += new Vector2(0, -100 * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down) ||
                currentKeyboardState.IsKeyDown(Keys.S))
            {
                Direction += new Vector2(0, 100 * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            #endregion

            #region Warp
            
            Warp = false;
            if (currentKeyboardState.IsKeyDown(Keys.Space) &&
                priorKeyboardState.IsKeyUp(Keys.Space))
            {
                Warp = true;
            }

            if (currentGamePadState.IsButtonDown(Buttons.A) &&
                previousGamePadState.IsButtonUp(Buttons.A))
            {
                Warp = true;
            }

            #endregion
        }

    }
}
