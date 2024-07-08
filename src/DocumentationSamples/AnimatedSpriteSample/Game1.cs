// Copyright (c) Craftwork Games. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Graphics;

namespace AnimatedSpriteSample;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private AnimatedSprite _adventurer;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.ApplyChanges();
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        Texture2D adventurerTexture = Content.Load<Texture2D>("adventurer");
        Texture2DAtlas atlas = new Texture2DAtlas("Atlas//adventurer", adventurerTexture);
        SpriteSheet spriteSheet = new SpriteSheet("SpriteSheet//adventurer", atlas);

        //  Attack frames
        spriteSheet.TextureAtlas.CreateRegion(0, 0, 50, 37, "attack-00");
        spriteSheet.TextureAtlas.CreateRegion(50, 0, 50, 37, "attack-01");
        spriteSheet.TextureAtlas.CreateRegion(100, 0, 50, 37, "attack-02");
        spriteSheet.TextureAtlas.CreateRegion(150, 0, 50, 37, "attack-03");
        spriteSheet.TextureAtlas.CreateRegion(200, 0, 50, 37, "attack-04");
        spriteSheet.TextureAtlas.CreateRegion(0, 37, 50, 37, "attack-05");

        //  idle frames
        spriteSheet.TextureAtlas.CreateRegion(50, 37, 50, 37, "idle-00");
        spriteSheet.TextureAtlas.CreateRegion(100, 37, 50, 37, "idle-01");
        spriteSheet.TextureAtlas.CreateRegion(150, 37, 50, 37, "idle-02");
        spriteSheet.TextureAtlas.CreateRegion(200, 37, 50, 37, "idle-03");

        // run frames
        spriteSheet.TextureAtlas.CreateRegion(0, 74, 50, 37, "run-00");
        spriteSheet.TextureAtlas.CreateRegion(50, 74, 50, 37, "run-01");
        spriteSheet.TextureAtlas.CreateRegion(100, 74, 50, 37, "run-02");
        spriteSheet.TextureAtlas.CreateRegion(150, 74, 50, 37, "run-03");
        spriteSheet.TextureAtlas.CreateRegion(200, 74, 50, 37, "run-04");
        spriteSheet.TextureAtlas.CreateRegion(0, 111, 50, 37, "run-05");

        spriteSheet.DefineAnimation("attack", builder =>
        {
            builder.IsLooping(true)
                   .AddFrame("attack-00", TimeSpan.FromSeconds(0.1))
                   .AddFrame("attack-01", TimeSpan.FromSeconds(0.1))
                   .AddFrame("attack-02", TimeSpan.FromSeconds(0.1))
                   .AddFrame("attack-03", TimeSpan.FromSeconds(0.1))
                   .AddFrame("attack-04", TimeSpan.FromSeconds(0.1))
                   .AddFrame("attack-05", TimeSpan.FromSeconds(0.1));
        });

        //  Define the idle animation
        spriteSheet.DefineAnimation("idle", builder =>
        {
            builder.IsLooping(true)
                   .AddFrame("idle-00", TimeSpan.FromSeconds(0.1))
                   .AddFrame("idle-01", TimeSpan.FromSeconds(0.1))
                   .AddFrame("idle-02", TimeSpan.FromSeconds(0.1))
                   .AddFrame("idle-03", TimeSpan.FromSeconds(0.1));
        });

        //  Define the run animation
        spriteSheet.DefineAnimation("run", builder =>
        {
            builder.IsLooping(true)
                   .AddFrame("run-00", TimeSpan.FromSeconds(0.1))
                   .AddFrame("run-01", TimeSpan.FromSeconds(0.1))
                   .AddFrame("run-02", TimeSpan.FromSeconds(0.1))
                   .AddFrame("run-03", TimeSpan.FromSeconds(0.1))
                   .AddFrame("run-04", TimeSpan.FromSeconds(0.1))
                   .AddFrame("run-05", TimeSpan.FromSeconds(0.1));
        });

        _adventurer = new AnimatedSprite(spriteSheet);
        _adventurer.SetAnimation("idle");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        //  Update the animated sprite
        _adventurer.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        //  Draw the animated sprite
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _spriteBatch.Draw(_adventurer, Vector2.Zero);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
