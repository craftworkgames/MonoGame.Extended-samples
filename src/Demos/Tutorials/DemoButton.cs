// Copyright (c) Craftwork Games. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Gum.DataTypes.Variables;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGum.Forms.Controls;
using MonoGameGum.GueDeriving;
using RenderingLibrary.Graphics;

namespace Tutorials;

public class DemoButton : InteractiveGue
{
    private readonly TextRuntime _textRuntime;
    private readonly NineSliceRuntime _nineSliceRuntime;
    private readonly VariableSave _highlitedTextColor;
    private readonly VariableSave _pushedTextColor;
    private readonly VariableSave _enabledTextColor;

    public Button FormsControl => FormsControlAsObject as Button;

    public DemoButton(bool fullInstantiation = true, bool tryCreateFormsObject = true)
        : base(new InvisibleRenderable())
    {
        if (fullInstantiation)
        {
            WidthUnits = Gum.DataTypes.DimensionUnitType.RelativeToContainer;
            HeightUnits = Gum.DataTypes.DimensionUnitType.RelativeToContainer;
            Width = -4;
            Height = -4;


            _nineSliceRuntime = new NineSliceRuntime();
            _nineSliceRuntime.Width = 0;
            _nineSliceRuntime.Height = 0;
            _nineSliceRuntime.WidthUnits = Gum.DataTypes.DimensionUnitType.RelativeToContainer;
            _nineSliceRuntime.HeightUnits = Gum.DataTypes.DimensionUnitType.RelativeToContainer;
            _nineSliceRuntime.Name = "ButtonBackground";
            Children.Add(_nineSliceRuntime);

            _textRuntime = new TextRuntime();
            _textRuntime.X = 0;
            _textRuntime.Y = 0;
            _textRuntime.Width = 0;
            _textRuntime.Height = 0;
            _textRuntime.Name = "TextInstance";
            _textRuntime.Color = Color.Black;
            _textRuntime.WidthUnits = Gum.DataTypes.DimensionUnitType.RelativeToContainer;
            _textRuntime.HeightUnits = Gum.DataTypes.DimensionUnitType.RelativeToContainer;
            _textRuntime.XOrigin = RenderingLibrary.Graphics.HorizontalAlignment.Center;
            _textRuntime.YOrigin = RenderingLibrary.Graphics.VerticalAlignment.Center;
            _textRuntime.XUnits = Gum.Converters.GeneralUnitType.PixelsFromMiddle;
            _textRuntime.YUnits = Gum.Converters.GeneralUnitType.PixelsFromMiddle;
            _textRuntime.HorizontalAlignment = RenderingLibrary.Graphics.HorizontalAlignment.Center;
            _textRuntime.VerticalAlignment = RenderingLibrary.Graphics.VerticalAlignment.Center;
            Children.Add(_textRuntime);

            _highlitedTextColor = new VariableSave();
            _highlitedTextColor.Name = "TextInstance.Color";
            _highlitedTextColor.Value = Color.Red;


            _pushedTextColor = new VariableSave();
            _pushedTextColor.Name = "TextInstance.Color";
            _pushedTextColor.Value = Color.Green;

            _enabledTextColor = new VariableSave();
            _enabledTextColor.Name = "TextInstance.Color";
            _enabledTextColor.Value = Color.Black;


            StateSaveCategory buttonCategory = new();
            buttonCategory.Name = "ButtonCategory";
            buttonCategory.States.Add(new()
            {
                Name = "Highlighted",
                Variables = new List<VariableSave>() { _highlitedTextColor }

            });
            buttonCategory.States.Add(new()
            {
                Name = "Pushed",
                Variables = new List<VariableSave>() { _pushedTextColor }
            });
            buttonCategory.States.Add(new()
            {
                Name = "Enabled",
                Variables = new List<VariableSave>() { _enabledTextColor }
            });

            AddCategory(buttonCategory);

            if (tryCreateFormsObject)
            {
                FormsControlAsObject = new Button(this);
            }
        }
    }

    public void SetText(string text) => _textRuntime.Text = text;

    public void SetTexture(Texture2D texture) => _nineSliceRuntime.SourceFile = texture;

    public void SetTextColor(Color color) => _textRuntime.Color = color;
    public void SetHighlightedTextColor(Color color) => _highlitedTextColor.Value = color;
    public void SetPushedTextColor(Color color) => _pushedTextColor.Value = color;
}
