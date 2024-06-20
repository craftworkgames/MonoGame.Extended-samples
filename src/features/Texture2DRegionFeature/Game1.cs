// Copyright (c) Craftwork Games. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Graphics;

namespace Texture2DRegionFeature
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2DRegion _aceOfHearts;
        private Texture2DRegion _aceOfDiamonds;
        private Texture2DRegion _aceOfClubs;
        private Texture2DRegion _aceOfSpades;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
            Window.Title = "Texture2DRegion Feature";
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D cardsTexture = Content.Load<Texture2D>("cards");

            //---------------------------------------------------------------------------------------------------------
            // Create a region for each ace card based on the rectangular boundary of the card within the source
            // texture.
            //---------------------------------------------------------------------------------------------------------
            _aceOfHearts = new Texture2DRegion(cardsTexture, 11, 3, 42, 60);
            _aceOfDiamonds = new Texture2DRegion(cardsTexture, 11, 68, 42, 60);
            _aceOfClubs = new Texture2DRegion(cardsTexture, 11, 133, 42, 60);
            _aceOfSpades = new Texture2DRegion(cardsTexture, 11, 198, 42, 60);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            //---------------------------------------------------------------------------------------------------------
            //  Draw the texture regions just like you would a normal Texture2D.  Since they are all using the same
            //  source texture, there is no texture swapping for each draw call.
            //---------------------------------------------------------------------------------------------------------
            _spriteBatch.Draw(_aceOfHearts, new Vector2(316, 270), Color.White);
            _spriteBatch.Draw(_aceOfDiamonds, new Vector2(358, 270), Color.White);
            _spriteBatch.Draw(_aceOfClubs, new Vector2(400, 270), Color.White);
            _spriteBatch.Draw(_aceOfSpades, new Vector2(442, 270), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
