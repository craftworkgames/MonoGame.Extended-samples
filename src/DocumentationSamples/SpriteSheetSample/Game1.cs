// Copyright (c) Craftwork Games. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Graphics;

namespace SpriteSheetSample;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private SpriteSheet _spriteSheet;
    private AnimationController _idleAnimationController;

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
        _spriteSheet = new SpriteSheet("SpriteSheet//adventurer", atlas);

        //  Attack frames
        _spriteSheet.TextureAtlas.CreateRegion(0, 0, 50, 37, "attack-00");
        _spriteSheet.TextureAtlas.CreateRegion(50, 0, 50, 37, "attack-01");
        _spriteSheet.TextureAtlas.CreateRegion(100, 0, 50, 37, "attack-02");
        _spriteSheet.TextureAtlas.CreateRegion(150, 0, 50, 37, "attack-03");
        _spriteSheet.TextureAtlas.CreateRegion(200, 0, 50, 37, "attack-04");
        _spriteSheet.TextureAtlas.CreateRegion(0, 37, 50, 37, "attack-05");

        //  idle frames
        _spriteSheet.TextureAtlas.CreateRegion(50, 37, 50, 37, "idle-00");
        _spriteSheet.TextureAtlas.CreateRegion(100, 37, 50, 37, "idle-01");
        _spriteSheet.TextureAtlas.CreateRegion(150, 37, 50, 37, "idle-02");
        _spriteSheet.TextureAtlas.CreateRegion(200, 37, 50, 37, "idle-03");

        // run frames
        _spriteSheet.TextureAtlas.CreateRegion(0, 74, 50, 37, "run-00");
        _spriteSheet.TextureAtlas.CreateRegion(50, 74, 50, 37, "run-01");
        _spriteSheet.TextureAtlas.CreateRegion(100, 74, 50, 37, "run-02");
        _spriteSheet.TextureAtlas.CreateRegion(150, 74, 50, 37, "run-03");
        _spriteSheet.TextureAtlas.CreateRegion(200, 74, 50, 37, "run-04");
        _spriteSheet.TextureAtlas.CreateRegion(0, 111, 50, 37, "run-05");

        _spriteSheet.DefineAnimation("attack", builder =>
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
        _spriteSheet.DefineAnimation("idle", builder =>
        {
            builder.IsLooping(true)
                   .AddFrame("idle-00", TimeSpan.FromSeconds(0.1))
                   .AddFrame("idle-01", TimeSpan.FromSeconds(0.1))
                   .AddFrame("idle-02", TimeSpan.FromSeconds(0.1))
                   .AddFrame("idle-03", TimeSpan.FromSeconds(0.1));
        });

        //  Define the run animation
        _spriteSheet.DefineAnimation("run", builder =>
        {
            builder.IsLooping(true)
                   .AddFrame("run-00", TimeSpan.FromSeconds(0.1))
                   .AddFrame("run-01", TimeSpan.FromSeconds(0.1))
                   .AddFrame("run-02", TimeSpan.FromSeconds(0.1))
                   .AddFrame("run-03", TimeSpan.FromSeconds(0.1))
                   .AddFrame("run-04", TimeSpan.FromSeconds(0.1))
                   .AddFrame("run-05", TimeSpan.FromSeconds(0.1));
        });

        //  Get the idle animation we defined
        SpriteSheetAnimation idleAnimation = _spriteSheet.GetAnimation("idle");

        //  Create the animation controller
        _idleAnimationController = new AnimationController(idleAnimation);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        //  Controller needs to be updated each frame.
        _idleAnimationController.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        //  Get the region of the current frame of animation
        Texture2DRegion currentFrameTexture = _spriteSheet.TextureAtlas[_idleAnimationController.CurrentFrame];

        //  Draw the frame
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _spriteBatch.Draw(currentFrameTexture, Vector2.Zero, Color.White, 0.0f, Vector2.Zero, new Vector2(3, 3), SpriteEffects.None, 0.0f);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
