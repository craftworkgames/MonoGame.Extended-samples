using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.ECS;
using MonoGame.Extended.ECS.Systems;
using Sandbox.Components;

namespace Sandbox.Systems
{
    public class ExpirySystem : EntityProcessingSystem
    {
        private ComponentMapper<Expiry> _expiryMapper;

        public ExpirySystem() 
            : base(Aspect.All(typeof(Expiry)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _expiryMapper = mapperService.GetMapper<Expiry>();
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var expiry = _expiryMapper.Get(entityId);

            expiry.TimeRemaining -= gameTime.GetElapsedSeconds();

            if (expiry.TimeRemaining <= 0)
                DestroyEntity(entityId);
        }
    }
}
