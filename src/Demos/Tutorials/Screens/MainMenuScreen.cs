// Copyright (c) Craftwork Games. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using MonoGameGum.Forms;
using MonoGameGum.Forms.Controls;
using MonoGameGum.GueDeriving;
using RenderingLibrary;

namespace Tutorials.Screens;

public class MainMenuScreen : GameScreen
{
    private ContainerRuntime _root;
    private ViewportAdapter _viewportAdapter;
    private new GameMain Game => (GameMain)base.Game;

    public MainMenuScreen(Game game) : base(game) { }

    public override void LoadContent()
    {
        base.LoadContent();

        Texture2D buttonTexture = Game.Content.Load<Texture2D>("Gui/button_rectangle_border");
        _viewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, 800, 480);

        _root = new ContainerRuntime()
        {
            Width = _viewportAdapter.VirtualWidth,
            Height = _viewportAdapter.ViewportHeight,
            WidthUnits = Gum.DataTypes.DimensionUnitType.Absolute,
            HeightUnits = Gum.DataTypes.DimensionUnitType.Absolute,
        };
        _root.AddToManagers();

        ContainerRuntime _menu = new ContainerRuntime()
        {
            Width = -4,
            Height = -4,
            X = 4,
            Y = 4,
            WidthUnits = Gum.DataTypes.DimensionUnitType.RelativeToContainer,
            HeightUnits = Gum.DataTypes.DimensionUnitType.RelativeToContainer,
            ChildrenLayout = Gum.Managers.ChildrenLayout.AutoGridHorizontal,
            XOrigin = RenderingLibrary.Graphics.HorizontalAlignment.Center,
            YOrigin = RenderingLibrary.Graphics.VerticalAlignment.Center,
            XUnits = Gum.Converters.GeneralUnitType.PixelsFromMiddle,
            YUnits = Gum.Converters.GeneralUnitType.PixelsFromMiddle,
            AutoGridHorizontalCells = 4,
            WrapsChildren = true
        };
        _root.Children.Add(_menu);

        foreach (var screen in (ScreenName[])Enum.GetValues<ScreenName>())
        {
            if(screen == ScreenName.MainMenu)
            {
                continue;
            }

            DemoButton runtime = new DemoButton();
            runtime.NineSlice.SourceFile = buttonTexture;
            Button button = runtime.FormsControl;
            button.Text = screen.ToString();
            button.Click += (_, _) =>
            {
                Game.LoadScreen(screen);
            };
            _menu.Children.Add(button.Visual);
        };

        DemoButton exitRuntime = new DemoButton();
        exitRuntime.NineSlice.SourceFile = buttonTexture;
        Button exitButton = exitRuntime.FormsControl;
        exitButton.Text = "Exit";
        exitButton.Click += (_, _) =>
        {
            Game.Exit();
        };
        _menu.Children.Add(exitButton.Visual);
    }
    public override void Update(GameTime gameTime)
    {
        FormsUtilities.Update(gameTime, _root);
        SystemManagers.Default.Activity(gameTime.TotalGameTime.TotalSeconds);
    }

    public override void Draw(GameTime gameTime)
    {
        float scaleFactor = _viewportAdapter.GetScaleMatrix().M11;
        SystemManagers.Default.Renderer.Camera.Zoom = scaleFactor;
        SystemManagers.Default.Draw();
    }
}
