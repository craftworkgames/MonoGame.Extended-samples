// Copyright (c) Craftwork Games. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Input.InputListeners;

namespace AnimatedSpriteSample;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private AnimatedSprite _adventurer;
    private KeyboardListener _keyboardListener;

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
        _keyboardListener = new KeyboardListener();
        _keyboardListener.KeyPressed += (sender, eventArgs) =>
        {
            if (eventArgs.Key == Keys.Enter && _adventurer.CurrentAnimation == "idle")
            {
                _adventurer.SetAnimation("attack").OnAnimationEvent += (sender, trigger) =>
                {
                    if (trigger == AnimationEventTrigger.AnimationCompleted)
                    {
                        _adventurer.SetAnimation("idle");
                    }
                };
            }
        };
        base.Initialize();
    }


    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        Texture2D adventurerTexture = Content.Load<Texture2D>("adventurer");
        Texture2DAtlas atlas = Texture2DAtlas.Create("Atlas/adventurer", adventurerTexture, 50, 37);
        SpriteSheet spriteSheet = new SpriteSheet("SpriteSheet/adventurer", atlas);

        spriteSheet.DefineAnimation("attack", builder =>
        {
            builder.IsLooping(false)
                   .AddFrame(regionIndex: 0, duration: TimeSpan.FromSeconds(0.1))
                   .AddFrame(1, TimeSpan.FromSeconds(0.1))
                   .AddFrame(2, TimeSpan.FromSeconds(0.1))
                   .AddFrame(3, TimeSpan.FromSeconds(0.1))
                   .AddFrame(4, TimeSpan.FromSeconds(0.1))
                   .AddFrame(5, TimeSpan.FromSeconds(0.1));
        });

        spriteSheet.DefineAnimation("idle", builder =>
        {
            builder.IsLooping(true)
                   .AddFrame(6, TimeSpan.FromSeconds(0.1))
                   .AddFrame(7, TimeSpan.FromSeconds(0.1))
                   .AddFrame(8, TimeSpan.FromSeconds(0.1))
                   .AddFrame(9, TimeSpan.FromSeconds(0.1));
        });

        spriteSheet.DefineAnimation("run", builder =>
        {
            builder.IsLooping(true)
                   .AddFrame(10, TimeSpan.FromSeconds(0.1))
                   .AddFrame(11, TimeSpan.FromSeconds(0.1))
                   .AddFrame(12, TimeSpan.FromSeconds(0.1))
                   .AddFrame(13, TimeSpan.FromSeconds(0.1))
                   .AddFrame(14, TimeSpan.FromSeconds(0.1))
                   .AddFrame(15, TimeSpan.FromSeconds(0.1));
        });

        _adventurer = new AnimatedSprite(spriteSheet, "idle");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        _keyboardListener.Update(gameTime);
        //  Update the animated sprite
        _adventurer.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        //  Draw the animated sprite
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _spriteBatch.Draw(_adventurer, new Vector2(50, 50), 0.0f, new Vector2(3, 3));
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
