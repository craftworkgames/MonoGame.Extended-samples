// Original code dervied from:
// https://github.com/thelinuxlich/starwarrior_CSharp/blob/master/StarWarrior/StarWarrior/Systems/CollisionSystem.cs

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollisionSystem.cs" company="GAMADU.COM">
//     Copyright © 2013 GAMADU.COM. All rights reserved.
//
//     Redistribution and use in source and binary forms, with or without modification, are
//     permitted provided that the following conditions are met:
//
//        1. Redistributions of source code must retain the above copyright notice, this list of
//           conditions and the following disclaimer.
//
//        2. Redistributions in binary form must reproduce the above copyright notice, this list
//           of conditions and the following disclaimer in the documentation and/or other materials
//           provided with the distribution.
//
//     THIS SOFTWARE IS PROVIDED BY GAMADU.COM 'AS IS' AND ANY EXPRESS OR IMPLIED
//     WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
//     FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL GAMADU.COM OR
//     CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
//     CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
//     SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
//     ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
//     NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
//     ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
//     The views and conclusions contained in the software and documentation are those of the
//     authors and should not be interpreted as representing official policies, either expressed
//     or implied, of GAMADU.COM.
// </copyright>
// <summary>
//   The collision system.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Collections;
using MonoGame.Extended.ECS;
using MonoGame.Extended.ECS.Systems;
using StarWarrior.Components;

namespace StarWarrior.Systems
{
    public class CollisionSystem : EntityUpdateSystem
    {
        private ComponentMapper<Transform2> _transformMapper;
        private ComponentMapper<EnemyComponent> _enemyMapper;
        private ComponentMapper<OwnerComponent> _ownerComponent;
        private ComponentMapper<PlayerComponent> _playerMapper;
        private ComponentMapper<HealthComponent> _healthMapper;

        private Bag<int> _enemies;
        private Bag<int> _players;
        private Bag<int> _bullets;

        public CollisionSystem() : base(Aspect.One(typeof(HealthComponent), typeof(OwnerComponent)))
        {

        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transformMapper = mapperService.GetMapper<Transform2>();
            _enemyMapper = mapperService.GetMapper<EnemyComponent>();
            _ownerComponent = mapperService.GetMapper<OwnerComponent>();
            _playerMapper = mapperService.GetMapper<PlayerComponent>();
            _healthMapper = mapperService.GetMapper<HealthComponent>();

            _enemies = new Bag<int>();
            _players = new Bag<int>();
            _bullets = new Bag<int>();
        }

        protected override void OnEntityAdded(int entityId)
        {
            if (_enemyMapper.Get(entityId) != null)
                _enemies.Add(entityId);
            else if (_playerMapper.Get(entityId) != null)
                _players.Add(entityId);
            else if (_ownerComponent.Get(entityId) != null)
                _bullets.Add(entityId);
        }
        protected override void OnEntityRemoved(int entityId)
        {
            if (_enemyMapper.Get(entityId) != null)
                _enemies.Remove(entityId);
            else if (_playerMapper.Get(entityId) != null)
                _players.Remove(entityId);
            else if (_ownerComponent.Get(entityId) != null)
                _bullets.Remove(entityId);
        }

        public override void Update(GameTime gameTime) 
        {
            if (_bullets.Count == 0 || _enemies.Count == 0)
                return;

            // Check bullets against enemy ships
            foreach (int bulletId in _bullets)
            {
                Transform2 bulletTransform = _transformMapper.Get(bulletId);
                OwnerComponent bulletOwner = _ownerComponent.Get(bulletId);

                PlayerComponent playerComponent = null;
                if (bulletOwner != null)
                {
                    playerComponent = _playerMapper.Get(bulletOwner.OwnerID);
                }

                // Player bullet, check against all enemies
                if (playerComponent != null)
                {
                    foreach (int enemyId in _enemies)
                    {
                        Transform2 enemyTransform = _transformMapper.Get(enemyId);
                        HealthComponent enemyHealth = _healthMapper.Get(enemyId);
                        if (CollisionExists(bulletTransform,enemyTransform))
                        {
                            DestroyEntity(bulletId);

                            // apply health reduction
                            enemyHealth.AddDamage(1);
                            if (!enemyHealth.IsAlive)
                                DestroyEntity(enemyId);
                        }
                    }
                }
                // Enemy bullet, check against players
                else
                {
                    foreach (int playerId in _players)
                    {
                        Transform2 playerTransform = _transformMapper.Get(playerId);
                        HealthComponent playerHealth = _healthMapper.Get(playerId);
                        if (CollisionExists(bulletTransform, playerTransform))
                        {
                            DestroyEntity(bulletId);

                            // apply health reduction
                            playerHealth.AddDamage(1);
                            if (!playerHealth.IsAlive)
                                DestroyEntity(playerId);
                        }
                    }
                }
            }
        }

        // ReSharper disable once SuggestBaseTypeForParameter
        private static bool CollisionExists(Transform2 bulletTransform, Transform2 shipTransform)
        {
            return Vector2.Distance(bulletTransform.WorldPosition, shipTransform.WorldPosition) < 20;
        }
    }
}
