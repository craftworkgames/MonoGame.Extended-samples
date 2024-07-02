// Copyright (c) Craftwork Games. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.ViewportAdapters;
using MonoGameGum.Forms;
using MonoGameGum.Forms.Controls;
using MonoGameGum.GueDeriving;
using RenderingLibrary;
using Tutorials.Demos;

namespace Tutorials.Screens;

public class MainMenuScreen : GameScreen
{
    private enum Demo
    {
        Animation,
        Batching,
        BitmapFonts,
        Camera,
        Collision,
        InputListener,
        Particles,
        Shapes,
        Sprites,
        TiledMaps,
        ViewportAdapter
    };


    private ContainerRuntime _menu;
    private ViewportAdapter _viewportAdapter;
    private new GameMain Game => (GameMain)base.Game;



    public MainMenuScreen(Game game) : base(game) { }

    public override void LoadContent()
    {
        base.LoadContent();

        Texture2D buttonTexture = Game.Content.Load<Texture2D>("Gui/button_rectangle_border");
        _viewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, 800, 480);

        _menu = new ContainerRuntime()
        {
            Width = -4,
            Height = -4,
            X = 4,
            Y = 4,
            WidthUnits = Gum.DataTypes.DimensionUnitType.RelativeToContainer,
            HeightUnits = Gum.DataTypes.DimensionUnitType.RelativeToContainer,
            ChildrenLayout = Gum.Managers.ChildrenLayout.AutoGridHorizontal,
            AutoGridHorizontalCells = 4,
            WrapsChildren = true
        };

        _menu.AddToManagers();

        foreach (var demo in (Demo[])Enum.GetValues<Demo>())
        {
            DemoButton runtime = new DemoButton();
            runtime.NineSlice.SourceFile = buttonTexture;
            Button button = runtime.FormsControl;
            button.Text = demo.ToString();
            button.Click += (_, _) =>
            {
                LoadDemo(demo);
            };
            _menu.Children.Add(button.Visual);
        };
    }

    private void LoadDemo(Demo demo)
    {
        GameScreen screen = demo switch
        {
            //Demo.Animation => new AnimationsDemo(Game),
            //Demo.Batching => new BatchingDemo(Game),
            //Demo.BitmapFonts => new BitmapFontsDemo(Game),
            //Demo.Camera => new CameraDemo(Game),
            Demo.InputListener => new InputListenersScreen(Game),
            Demo.Particles => new ParticlesScreen(Game),
            //Demo.Shapes => new ShapesDemo(Game),
            //Demo.TiledMaps => new TiledMapsDemo(Game),
            //Demo.ViewportAdapter => new ViewportAdaptersDemo(Game),
            _ => null
        };

        if (screen is not null)
        {
            Game.ScreenManager.LoadScreen(screen, new FadeTransition(Game.GraphicsDevice, Color.White, 1.0f));
        }
    }

    public override void Update(GameTime gameTime)
    {
        FormsUtilities.Update(gameTime, _menu);
        SystemManagers.Default.Activity(gameTime.TotalGameTime.TotalSeconds);
    }

    public override void Draw(GameTime gameTime)
    {
        float scaleFactor = _viewportAdapter.GetScaleMatrix().M11;
        SystemManagers.Default.Renderer.Camera.Zoom = scaleFactor;
        SystemManagers.Default.Draw();
    }
}
