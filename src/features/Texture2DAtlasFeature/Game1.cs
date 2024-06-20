// Copyright (c) Craftwork Games. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Graphics;

namespace Texture2DAtlasFeature
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2DAtlas _atlas;
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
            Window.Title = "Texture2DAtlas Feature";
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
            //  Create a new atlas using a Texture2D
            //---------------------------------------------------------------------------------------------------------
            _atlas = new Texture2DAtlas(cardsTexture);

            //---------------------------------------------------------------------------------------------------------
            //  Use the CreateRegion method to create a Texture2DRegion in the atlas.
            //  The resulting region is returned
            //---------------------------------------------------------------------------------------------------------
            _aceOfHearts = _atlas.CreateRegion(11, 3, 42, 60);

            //---------------------------------------------------------------------------------------------------------
            //  Create regions and retrieve them by index. 
            //  Regions are indexed in the atlas in the order they are created and added to the atlas.
            //---------------------------------------------------------------------------------------------------------
            _atlas.CreateRegion(11, 68, 42, 60);
            _atlas.CreateRegion(11, 133, 42, 60);
            _aceOfDiamonds = _atlas[1];
            _aceOfClubs = _atlas.GetRegion(2);

            //---------------------------------------------------------------------------------------------------------
            //  Create a region with a name and retrieve it by name.
            //  Region names must be unique.
            //---------------------------------------------------------------------------------------------------------
            _atlas.CreateRegion(11, 198, 42, 60, "Ace of Spades");
            _aceOfSpades = _atlas.GetRegion("Ace of Spades");
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
