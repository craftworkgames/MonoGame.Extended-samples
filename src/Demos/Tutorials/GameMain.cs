using System.Collections.Generic;
using System.Linq;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using MonoGameGum.Forms;
using MonoGameGum.Forms.Controls;
using MonoGameGum.Forms.DefaultVisuals;
using MonoGameGum.GueDeriving;
using RenderingLibrary;
using Tutorials.Demos;
using Tutorials.Screens;

namespace Tutorials
{
    public class GameMain : Game
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly GraphicsDeviceManager _graphicsDeviceManager;
        private readonly FramesPerSecondCounter _fpsCounter = new FramesPerSecondCounter();
        private readonly Dictionary<string, DemoBase> _demos;
        private DemoBase _currentDemo;

        public ScreenManager ScreenManager { get; private set; }

        //private GuiSystem _guiSystem;

        public ViewportAdapter ViewportAdapter { get; private set; }

        public GameMain(PlatformConfig config)
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this)
            {
                IsFullScreen = config.IsFullScreen,
                SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight
            };

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;

            _demos = new DemoBase[]
            {
                new ShapesDemo(this),
                new ViewportAdaptersDemo(this),
                new CollisionDemo(this),
                new TiledMapsDemo(this),
                new AnimationsDemo(this),
                new SpritesDemo(this),
                new BatchingDemo(this),
                new InputListenersDemo(this),
                new ParticlesDemo(this),
                new CameraDemo(this),
                new BitmapFontsDemo(this)
            }.ToDictionary(d => d.Name);

            ScreenManager = new ScreenManager();
            Components.Add(ScreenManager);
        }

        protected override void Dispose(bool disposing)
        {
            foreach (var demo in _demos.Values)
                demo.Dispose();

            base.Dispose(disposing);
        }

        protected override void Initialize()
        {
            //  Initialize GUM UI System
            SystemManagers.Default = new SystemManagers();
            SystemManagers.Default.Initialize(_graphicsDeviceManager.GraphicsDevice, fullInstantiation: true);
            FormsUtilities.InitializeDefaults();
            FrameworkElement.DefaultFormsComponents[typeof(Button)] = typeof(DemoButton);

            base.Initialize();

            // TODO: Allow switching to full-screen mode from the UI
            //if (_isFullScreen)
            //{
            //    _graphicsDeviceManager.IsFullScreen = true;
            //    _graphicsDeviceManager.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            //    _graphicsDeviceManager.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            //    _graphicsDeviceManager.ApplyChanges();
            //}            
        }

        

        protected override void LoadContent()
        {
            ViewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 480);
            ScreenManager.LoadScreen(new MainMenuScreen(this));

            //_menu = new ContainerRuntime()
            //{
            //    Width = -4,
            //    Height = -4,
            //    X = 4,
            //    Y = 4,
            //    WidthUnits = Gum.DataTypes.DimensionUnitType.RelativeToContainer,
            //    HeightUnits = Gum.DataTypes.DimensionUnitType.RelativeToContainer,
            //    ChildrenLayout = Gum.Managers.ChildrenLayout.AutoGridHorizontal,
            //    AutoGridHorizontalCells = 4,
            //    WrapsChildren = true
            //};
            //_menu.AddToManagers();


            //foreach (var demo in _demos.Values.OrderBy(i => i.Name))
            //{
            //    DemoButton runtime = new DemoButton();
            //    Button button = runtime.FormsControl;
            //    button.Text = demo.Name;
            //    button.Click += (_, _) =>
            //    {
            //        LoadDemo(demo.Name);
            //    };
            //    _menu.Children.Add(button.Visual);



            //    //DemoButton button = new DemoButton();
            //    //button.Text = demo.Name;
            //    //button.Height = -4;
            //    //button.Width = -4;
            //    //button.WidthUnits = Gum.DataTypes.DimensionUnitType.RelativeToContainer;
            //    //button.HeightUnits= Gum.DataTypes.DimensionUnitType.RelativeToContainer;

            //    //button.Click += (_, _) =>
            //    //{
            //    //    LoadDemo(demo.Name);
            //    //};
            //    //_menu.Children.Add(button.FormsControl.Visual);
            //}


            //_guiSystem = new GuiSystem(ViewportAdapter, guiRenderer)
            //{
            //    ActiveScreen = _selectDemoScreen,
            //};
        }

        private void LoadDemo(string name)
        {
            IsMouseVisible = true;
            _currentDemo?.Unload();
            _currentDemo?.Dispose();
            _currentDemo = _demos[name];
            _currentDemo.Load();
            //_menu.Visible = false;
        }

        private KeyboardState _previousKeyboardState;
        private SelectDemoScreen _selectDemoScreen;

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape) && _previousKeyboardState.IsKeyUp(Keys.Escape))
                Back();

            _fpsCounter.Update(gameTime);
            //_guiSystem.Update(gameTime);
            _currentDemo?.OnUpdate(gameTime);

            _previousKeyboardState = keyboardState;
            base.Update(gameTime);
        }

        public void Back()
        {
            //    if (_menu.Visible)
            //        Exit();

            //    IsMouseVisible = true;
            //    _currentDemo?.Unload();
            //    _currentDemo?.Dispose();
            //    _currentDemo = null;
            //    _menu.Visible = true;
            //_guiSystem.ActiveScreen = _selectDemoScreen;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _fpsCounter.Draw(gameTime);
            Window.Title = $"{_currentDemo?.Name} {_fpsCounter.FramesPerSecond}";

            base.Draw(gameTime);

            //_currentDemo?.OnDraw(gameTime);

            //var scaleMatrix = ViewportAdapter.GetScaleMatrix();
            //var scaleFactor = scaleMatrix.M11;
            //SystemManagers.Default.Renderer.Camera.Zoom = scaleFactor;
            //SystemManagers.Default.Draw();

            //_guiSystem.Draw(gameTime);
        }
    }
}
