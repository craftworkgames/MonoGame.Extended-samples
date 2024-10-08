using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;

namespace SpaceGame.Entities
{
    public class MeteorFactory
    {
        private readonly EntityManager _entityManager;
        private readonly Dictionary<int, Texture2DRegion[]> _meteorRegions;
        private readonly Random _random = new Random();

        public MeteorFactory(EntityManager entityManager, ContentManager contentManager)
        {
            _entityManager = entityManager;

            _meteorRegions = new Dictionary<int, Texture2DRegion[]>
            {
                {4, new[]
                {
                    new Texture2DRegion(contentManager.Load<Texture2D>("meteorBrown_big1")),
                    new Texture2DRegion(contentManager.Load<Texture2D>("meteorBrown_big2")),
                    new Texture2DRegion(contentManager.Load<Texture2D>("meteorBrown_big3")),
                    new Texture2DRegion(contentManager.Load<Texture2D>("meteorBrown_big4"))
                }},
                {3, new[]
                {
                    new Texture2DRegion(contentManager.Load<Texture2D>("meteorBrown_med1")),
                    new Texture2DRegion(contentManager.Load<Texture2D>("meteorBrown_med3"))
                }},
                {2, new[]
                {
                    new Texture2DRegion(contentManager.Load<Texture2D>("meteorBrown_small1")),
                    new Texture2DRegion(contentManager.Load<Texture2D>("meteorBrown_small2"))
                }},
                {1, new[]
                {
                    new Texture2DRegion(contentManager.Load<Texture2D>("meteorBrown_tiny1")),
                    new Texture2DRegion(contentManager.Load<Texture2D>("meteorBrown_tiny2"))
                }}
            };
        }

        private Texture2DRegion GetMeteorRegion(int size)
        {
            var regions = _meteorRegions[size];
            var index = _random.Next(0, regions.Length);
            return regions[index];
        }

        public void SplitMeteor(Meteor meteor)
        {
            if (meteor.Size <= 1)
            {
                throw new InvalidOperationException("Meteor size less than 2 can't be split");
            }

            for (var i = 0; i < 2; i++)
            {
                var size = meteor.Size - 1;
                var rotationSpeed = _random.Next(-10, 10) * 0.1f;
                var spawnPosition = meteor.Position;
                var velocity = i == 0 ? meteor.Velocity.PerpendicularClockwise() + meteor.Velocity : meteor.Velocity.PerpendicularCounterClockwise() + meteor.Velocity;
                var textureRegion = GetMeteorRegion(size);
                var newMeteor = new Meteor(textureRegion, spawnPosition, velocity, rotationSpeed, size);

                _entityManager.AddEntity(newMeteor);
            }
        }

        public void SpawnNewMeteor(Vector2 playerPosition)
        {
            var rotationSpeed = _random.Next(-10, 10) * 0.1f;
            var spawnCircle = new CircleF(playerPosition, 630);
            var spawnAngle = MathHelper.ToRadians(_random.Next(0, 360));
            var spawnPosition = spawnCircle.BoundaryPointAt(spawnAngle);

            var velocity = (playerPosition - spawnPosition);
            velocity.Rotate(MathHelper.ToRadians(_random.Next(-15, 15)));
            velocity *= _random.Next(3, 10) * 0.01f;

            var textureRegion = GetMeteorRegion(4);
            var meteor = new Meteor(textureRegion, spawnPosition, velocity, rotationSpeed, 3);

            _entityManager.AddEntity(meteor);
        }
    }
}
