// Copyright (c) Craftwork Games. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;

namespace ContentManager_Extensions
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // SprintFont is only to demo that the text file was in-fact loaded
        private SpriteFont _font; 

        // For demo of Content.OpenStream extension method
        private string songLyrics;

        // For demo of Content.GetGraphicsDevice extension method
        private int deviceWidth;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // For demo of Content.OpenStream extension method
            // song-lyrics.txt is in the Content Directory
            // And it's set to "copy to output directory" in the project
            var stream = Content.OpenStream("song-lyrics.txt");
            var reader = new StreamReader(stream);
            songLyrics = reader.ReadToEnd();
            reader.Close();
            stream.Close();

            // For demo of Content.GetGraphicsDevice extension method
            var graphicsDevice = Content.GetGraphicsDevice();
            deviceWidth = graphicsDevice.DisplayMode.Width;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("examplefont");
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

            // Draw some text to the screen to show that we've used extension methods to gather data
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, songLyrics, new Vector2(5,5), Color.White);
            _spriteBatch.DrawString(_font, "Graphics Device Width:" + deviceWidth.ToString(), new Vector2(400, 5), Color.Blue);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
