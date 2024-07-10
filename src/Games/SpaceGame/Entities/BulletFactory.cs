using Microsoft.Xna.Framework;
using MonoGame.Extended.Graphics;

namespace SpaceGame.Entities
{
    public interface IBulletFactory
    {
        void Create(Vector2 position, Vector2 direction, float rotation, float speed);
    }

    public class BulletFactory : IBulletFactory
    {
        private readonly Texture2DRegion _bulletRegion;
        private readonly IEntityManager _entityManager;

        public BulletFactory(IEntityManager entityManager, Texture2DRegion bulletRegion)
        {
            _entityManager = entityManager;
            _bulletRegion = bulletRegion;
        }

        public void Create(Vector2 position, Vector2 direction, float rotation, float speed)
        {
            var velocity = direction * speed;
            var laser = new Laser(_bulletRegion, velocity)
            {
                Position = position,
                Rotation = rotation
            };
            _entityManager.AddEntity(laser);
        }
    }
}
