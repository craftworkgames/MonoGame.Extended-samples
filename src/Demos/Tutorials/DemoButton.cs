// Copyright (c) Craftwork Games. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using Gum.Wireframe;
using Microsoft.Xna.Framework;
using MonoGameGum.Forms.Controls;
using MonoGameGum.GueDeriving;
using RenderingLibrary.Graphics;

namespace Tutorials;

public class DemoButton : InteractiveGue
{
    public TextRuntime TextInstance { get; private set; }
    public NineSliceRuntime NineSlice { get; private set; }
    public string Text
    {
        get => TextInstance.Text;
        set => TextInstance.Text = value;
    }

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


            NineSlice = new NineSliceRuntime();
            NineSlice.Width = 0;
            NineSlice.Height = 0;
            NineSlice.WidthUnits = Gum.DataTypes.DimensionUnitType.RelativeToContainer;
            NineSlice.HeightUnits = Gum.DataTypes.DimensionUnitType.RelativeToContainer;
            NineSlice.Name = "ButtonBackground";
            Children.Add(NineSlice);

            TextInstance = new TextRuntime();
            TextInstance.X = 0;
            TextInstance.Y = 0;
            TextInstance.Width = 0;
            TextInstance.Height = 0;
            TextInstance.Name = nameof(TextInstance);
            TextInstance.Color = Color.Black;
            TextInstance.WidthUnits = Gum.DataTypes.DimensionUnitType.RelativeToContainer;
            TextInstance.HeightUnits = Gum.DataTypes.DimensionUnitType.RelativeToContainer;
            TextInstance.XOrigin = RenderingLibrary.Graphics.HorizontalAlignment.Center;
            TextInstance.YOrigin = RenderingLibrary.Graphics.VerticalAlignment.Center;
            TextInstance.XUnits = Gum.Converters.GeneralUnitType.PixelsFromMiddle;
            TextInstance.YUnits = Gum.Converters.GeneralUnitType.PixelsFromMiddle;
            TextInstance.HorizontalAlignment = RenderingLibrary.Graphics.HorizontalAlignment.Center;
            TextInstance.VerticalAlignment = RenderingLibrary.Graphics.VerticalAlignment.Center;
            Children.Add(TextInstance);

            if (tryCreateFormsObject)
            {
                FormsControlAsObject = new Button(this);
            }
        }
    }
}
