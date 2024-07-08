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
    private AnimationController _attackAnimationController;

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
        Texture2DAtlas atlas = Texture2DAtlas.Create("Atlas/adventurer", adventurerTexture, 50, 37);
        _spriteSheet = new SpriteSheet("SpriteSheet/adventurer", atlas);

        _spriteSheet.DefineAnimation("attack", builder =>
        {
            builder.IsLooping(true)
                   .AddFrame(regionIndex: 0, duration: TimeSpan.FromSeconds(0.1))
                   .AddFrame(1, TimeSpan.FromSeconds(0.1))
                   .AddFrame(2, TimeSpan.FromSeconds(0.1))
                   .AddFrame(3, TimeSpan.FromSeconds(0.1))
                   .AddFrame(4, TimeSpan.FromSeconds(0.1))
                   .AddFrame(5, TimeSpan.FromSeconds(0.1));
        });

        SpriteSheetAnimation idleAnimation = _spriteSheet.GetAnimation("attack");
        _attackAnimationController = new AnimationController(idleAnimation);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        //  Controller needs to be updated each frame.
        _attackAnimationController.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        Texture2DRegion currentFrameTexture = _spriteSheet.TextureAtlas[_attackAnimationController.CurrentFrame];

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _spriteBatch.Draw(currentFrameTexture, Vector2.Zero, Color.White, 0.0f, Vector2.Zero, new Vector2(3, 3), SpriteEffects.None, 0.0f);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
