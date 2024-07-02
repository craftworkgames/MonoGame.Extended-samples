using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
//using MonoGame.Extended.Gui;
//using MonoGame.Extended.Gui.Controls;
using MonoGame.Extended.Screens;
using MonoGameGum.Forms;
using MonoGameGum.Forms.Controls;
using MonoGameGum.GueDeriving;
using RenderingLibrary;
using Tutorials.Demos;

namespace Tutorials
{
    public class SelectDemoScreen : Screen
    {
        private readonly IDictionary<string, DemoBase> _demos;
        private readonly Action<string> _loadDemo;
        private ContainerRuntime _root;

        public SelectDemoScreen(IDictionary<string, DemoBase> demos, Action<string> loadDemo, Action exitGameAction)
        {
            _demos = demos;
            _loadDemo = loadDemo;
            _root = new ContainerRuntime()
            {
                Width = 0,
                Height = 0,
                WidthUnits = Gum.DataTypes.DimensionUnitType.RelativeToContainer,
                HeightUnits = Gum.DataTypes.DimensionUnitType.RelativeToContainer,
                ChildrenLayout = Gum.Managers.ChildrenLayout.AutoGridHorizontal
            };
            _root.AddToManagers();

            foreach(var demo in _demos.Values.OrderBy(i => i.Name))
            {
                Button button = new Button();
                button.Text = demo.Name;
                button.Click += (_, _) =>
                {
                    LoadDemo(demo);
                };
                _root.Children.Add(button.Visual);
            }
            

            //var grid = new UniformGrid();

            //foreach (var demo in _demos.Values.OrderBy(i => i.Name))
            //{
            //    var button = new Button()
            //    {
            //        Content = demo.Name,
            //        Margin = new Thickness(4),
            //    };
            //    button.Clicked += (sender, args) => LoadDemo(demo);
            //    grid.Items.Add(button);
            //}

            //var closeButton = new Button()
            //{
            //    Margin = 4,
            //    Content = "Close",
            //};
            //closeButton.Clicked += (sender, args) => exitGameAction();
            //grid.Items.Add(closeButton);

            //this.Content = grid;
        }

        public override void Update(GameTime gameTime)
        {
            FormsUtilities.Update(gameTime, _root);
            SystemManagers.Default.Activity(gameTime.TotalGameTime.TotalSeconds);
        }

        public override void Draw(GameTime gameTime)
        {
            SystemManagers.Default.Draw();
        }

        public override void Dispose()
        {
            foreach (var demo in _demos.Values)
                demo.Dispose();

            base.Dispose();
        }

        private void LoadDemo(DemoBase demo)
        {
            _loadDemo(demo.Name);
            //Hide();
        }
    }
}
