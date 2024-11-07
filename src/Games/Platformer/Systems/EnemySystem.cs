using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ECS;
using MonoGame.Extended.ECS.Systems;
using MonoGame.Extended.Graphics;
using Platformer.Collisions;
using Platformer.Components;

namespace Platformer.Systems
{
    public class EnemySystem : EntityProcessingSystem
    {
        public EnemySystem()
            : base(Aspect.All(typeof(Body), typeof(Enemy)))
        {
        }

        private ComponentMapper<Body> _bodyMapper;
        private ComponentMapper<Enemy> _enemyMapper;
        private ComponentMapper<AnimatedSprite> _animatedSpriteMapper;

        public override void Initialize(IComponentMapperService mapperService)
        {
            _enemyMapper = mapperService.GetMapper<Enemy>();
            _bodyMapper = mapperService.GetMapper<Body>();
            _animatedSpriteMapper = mapperService.GetMapper<AnimatedSprite>();
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var elapsedSeconds = gameTime.GetElapsedSeconds();
            var body = _bodyMapper.Get(entityId);
            var enemy = _enemyMapper.Get(entityId);
            var sprite = _animatedSpriteMapper.Get(entityId);

            enemy.TimeLeft -= elapsedSeconds;

            if (enemy.TimeLeft <= 0)
            {
                enemy.Speed = -enemy.Speed;
                enemy.TimeLeft = 1.0f;

                if (sprite.Effect == SpriteEffects.None)
                    sprite.Effect = SpriteEffects.FlipHorizontally;
                else
                    sprite.Effect = SpriteEffects.None;

                sprite.SetAnimation("walk");
            }

            body.Position = body.Position.Translate(enemy.Speed * elapsedSeconds, 0);
        }
    }
}
