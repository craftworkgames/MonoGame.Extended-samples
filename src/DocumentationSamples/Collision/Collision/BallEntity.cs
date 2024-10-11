// Copyright (c) Craftwork Games. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Collisions;
using MonoGame.Extended;

namespace Collision
{
    public class BallEntity : IEntity
    {
        private readonly Game1 _game;
        public Vector2 Velocity;
        public IShapeF Bounds { get; }

        public BallEntity(Game1 game, CircleF circleF)
        {
            _game = game;
            Bounds = circleF;
            RandomizeVelocity();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawCircle((CircleF)Bounds, 8, Color.Red, 3f);
        }

        public void Update(GameTime gameTime)
        {
            Bounds.Position += Velocity * gameTime.GetElapsedSeconds() * 30;
        }

        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            RandomizeVelocity();
            Bounds.Position -= collisionInfo.PenetrationVector;
        }


        private void RandomizeVelocity()
        {
            Velocity.X = _game.Random.Next(-1, 2);
            Velocity.Y = _game.Random.Next(-1, 2);
        }
    }
}
