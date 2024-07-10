// Copyright (c) Craftwork Games. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Graphics;

namespace SpriteSample;


public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2DAtlas _atlas;
    private Sprite _aceOfSpadesSprite;
    private Sprite _aceOfDiamondsSprite;
    private Sprite _aceOfHeartsSprite;
    private Sprite _aceOfClubsSprite;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = 800;
        _graphics.PreferredBackBufferHeight = 600;
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

        Texture2D cardsTexture = Content.Load<Texture2D>("cards");
        _atlas = Texture2DAtlas.Create("Atlas/Cards", cardsTexture, 32, 32);

        _aceOfClubsSprite = _atlas.CreateSprite(regionIndex: 12);
        _aceOfDiamondsSprite = _atlas.CreateSprite(regionIndex: 25);
        _aceOfHeartsSprite = _atlas.CreateSprite(regionIndex: 38);
        _aceOfSpadesSprite = _atlas.CreateSprite(regionIndex: 51);

        //  Change the color mask of the heart and diamond to red
        _aceOfHeartsSprite.Color = Color.Red;
        _aceOfDiamondsSprite.Color = Color.Red;

        //  Change the Alpha transparency of the club and spade to half
        _aceOfClubsSprite.Alpha = 0.5f;
        _aceOfSpadesSprite.Alpha = 0.5f;

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

        _spriteBatch.Draw(_aceOfClubsSprite, new Vector2(336, 284));
        _spriteBatch.Draw(_aceOfDiamondsSprite, new Vector2(368, 284));
        _spriteBatch.Draw(_aceOfHeartsSprite, new Vector2(400, 284));
        _spriteBatch.Draw(_aceOfSpadesSprite, new Vector2(432, 284));

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
