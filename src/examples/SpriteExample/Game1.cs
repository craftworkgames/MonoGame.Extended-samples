// Copyright (c) Craftwork Games. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;

namespace SpriteExample;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2DAtlas _atlas;
    private Sprite _aceOfHearts;
    private Sprite _aceOfDiamonds;
    private Sprite _aceOfClubs;
    private Sprite _aceOfSpades;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = 800;
        _graphics.PreferredBackBufferHeight = 600;
        _graphics.ApplyChanges();
        Window.Title = "Sprite Example";
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

        //  Create the atlas and add regions to it for each ace card
        _atlas = new Texture2DAtlas(cardsTexture);
        _atlas.CreateRegion(11, 68, 42, 60, "Ace of Hearts");
        _atlas.CreateRegion(11, 68, 42, 60, "Ace of Diamonds");
        _atlas.CreateRegion(11, 133, 42, 60, "Ace of Clubs");
        _atlas.CreateRegion(11, 198, 42, 60, "Ace of Spades");


        //  Create a sprite from the atlas
        _aceOfHearts = _atlas.CreateSprite("Ace of Hearts");
        _aceOfDiamonds = _atlas.CreateSprite("Ace of Diamonds");
        _aceOfClubs = _atlas.CreateSprite("Ace of Clubs");
        _aceOfSpades = _atlas.CreateSprite("Ace of Spades");

        //  Change the color mask of the heart and diamond to red
        _aceOfHearts.Color = Color.Red;
        _aceOfDiamonds.Color = Color.Red;

        //  Change the Alpha transparency of the club and spade to half
        _aceOfClubs.Alpha = 0.5f;
        _aceOfSpades.Alpha = 0.5f;
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

        _spriteBatch.Begin();
        _spriteBatch.Draw(_aceOfHearts, new Vector2(316, 270));
        _spriteBatch.Draw(_aceOfDiamonds, new Vector2(358, 270));
        _spriteBatch.Draw(_aceOfClubs, new Vector2(400, 270));
        _spriteBatch.Draw(_aceOfSpades, new Vector2(442, 270));
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
