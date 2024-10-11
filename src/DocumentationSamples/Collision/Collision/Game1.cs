// Copyright (c) Craftwork Games. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Collisions;
using MonoGame.Extended;

namespace Collision
{
    public class Game1 : Game
    {
        // Monogame core related
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Monogame.Extended.Collisions example related
        public readonly Random Random = new Random(Guid.NewGuid().GetHashCode());
        private readonly List<IEntity> _entities = new List<IEntity>();
        private readonly CollisionComponent _collisionComponent;
        const int MapWidth = 500;
        const int MapHeight = 500;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Monogame.Extended.Collisions example related
            _collisionComponent = new CollisionComponent(new RectangleF(0, 0, MapWidth, MapHeight));
        }

        protected override void Initialize()
        {
            base.Initialize();

            // Create some objects to use in the collision demo
            for (var i = 0; i < 50; i++)
            {
                var size = Random.Next(20, 40);
                var position = new Vector2(Random.Next(-MapWidth, MapWidth * 2), Random.Next(0, MapHeight));
                if (i % 2 == 0)
                {
                    _entities.Add(new BallEntity(this, new CircleF(position, size)));
                }
                else
                {
                    _entities.Add(new CubeEntity(this, new RectangleF(position, new SizeF(size, size))));
                }
            }

            // Add those objects to the collisionComponent so it will do the collision checking for us
            foreach (IEntity entity in _entities)
            {
                _collisionComponent.Insert(entity);
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Make sure each entity moves around the screen
            foreach (IEntity entity in _entities)
            {
                entity.Update(gameTime);
            }

            // Make sure all collisions are detected and the OnCollision event for each is called
            _collisionComponent.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw all the entities
            _spriteBatch.Begin();
            foreach (IEntity entity in _entities)
            {
                entity.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
