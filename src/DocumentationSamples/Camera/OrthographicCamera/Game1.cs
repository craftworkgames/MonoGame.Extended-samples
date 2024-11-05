using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace camera
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private OrthographicCamera _camera;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 640);
            _camera = new OrthographicCamera(viewportAdapter);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            const float movementSpeed = 200;
            _camera.Move(GetMovementDirection() * movementSpeed * gameTime.GetElapsedSeconds());
            AdjustZoom();
            RotateCamera();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var transformMatrix = _camera.GetViewMatrix();
            _spriteBatch.Begin(transformMatrix: transformMatrix);

            // Draw some Rectangles for orientation as an example
            _spriteBatch.DrawRectangle(new RectangleF(0, 0, 50, 50), Color.Red, 25f);
            _spriteBatch.DrawRectangle(new RectangleF(150, 150, 50, 50), Color.Green, 25f);
            _spriteBatch.DrawRectangle(new RectangleF(150, 0, 50, 50), Color.White, 25f);
            _spriteBatch.DrawRectangle(new RectangleF(0, 150, 50, 50), Color.White, 25f);
            _spriteBatch.DrawRectangle(new RectangleF(300, 150, 50, 50), Color.White, 25f);
            _spriteBatch.DrawRectangle(new RectangleF(150, 300, 50, 50), Color.White, 25f);
            _spriteBatch.DrawRectangle(new RectangleF(300, 300, 50, 50), Color.Blue, 25f);
            _spriteBatch.DrawRectangle(new RectangleF(300, 0, 50, 50), Color.Black, 25f);
            _spriteBatch.DrawRectangle(new RectangleF(0, 300, 50, 50), Color.Orange, 25f);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private Vector2 GetMovementDirection()
        {
            var movementDirection = Vector2.Zero;
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Down))
            {
                movementDirection += Vector2.UnitY;
            }
            if (state.IsKeyDown(Keys.Up))
            {
                movementDirection -= Vector2.UnitY;
            }
            if (state.IsKeyDown(Keys.Left))
            {
                movementDirection -= Vector2.UnitX;
            }
            if (state.IsKeyDown(Keys.Right))
            {
                movementDirection += Vector2.UnitX;
            }
            return movementDirection;
        }

        private void AdjustZoom()
        {
            var state = Keyboard.GetState();
            float zoomPerTick = 0.01f;
            if (state.IsKeyDown(Keys.Z))
            {
                _camera.ZoomIn(zoomPerTick);
            }
            if (state.IsKeyDown(Keys.X))
            {
                _camera.ZoomOut(zoomPerTick);
            }
        }

        private void RotateCamera()
        {
            var state = Keyboard.GetState();
            float rotateRadiansPerTick = 0.01f;
            if (state.IsKeyDown(Keys.OemSemicolon))
            {
                _camera.Rotate(rotateRadiansPerTick);
            }
            if (state.IsKeyDown(Keys.OemQuotes))
            {
                _camera.Rotate(-rotateRadiansPerTick);
            }
        }
    }
}
