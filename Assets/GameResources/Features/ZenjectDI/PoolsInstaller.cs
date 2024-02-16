namespace Tanks.Features.DI
{
    using UnityEngine;
    using Zenject;
    using Tanks.Features.Shooting;
    using Tanks.Features.Explosion;
    using Tanks.Features.Enviroment;

    /// <summary>
    /// Инсталлер пулов
    /// </summary>
    public sealed class PoolsInstaller : MonoInstaller
    {
        [SerializeField]
        private Transform _poolsParent = default;

        [SerializeField]
        private EnemyProjectilePool _enemyProjectilePool = default;
        [SerializeField]
        private PlayerProjectilePool _playerProjectilePool = default;
        [SerializeField]
        private ExplosionsPool _explosionsPool = default;
        [SerializeField]
        private PlayerMissilePool _playerMissilePool = default;
        [SerializeField]
        private WallCrashEffectsPool _wallCrashEffectsPool = default;
        [SerializeField]
        private ProjectileHitEffectPool _projectileHitEffectPool = default;

        public override void InstallBindings()
        {
            EnemyProjectilePool enemyProjectilePool = Container
                .InstantiatePrefabForComponent<EnemyProjectilePool>(_enemyProjectilePool, _poolsParent);
            Container.Bind<EnemyProjectilePool>().FromInstance(enemyProjectilePool).AsSingle();

            PlayerProjectilePool playerProjectilePool = Container
                .InstantiatePrefabForComponent<PlayerProjectilePool>(_playerProjectilePool, _poolsParent);
            Container.Bind<PlayerProjectilePool>().FromInstance(playerProjectilePool).AsSingle();

            ExplosionsPool explosionsPool = Container
                .InstantiatePrefabForComponent<ExplosionsPool>(_explosionsPool, _poolsParent);
            Container.Bind<ExplosionsPool>().FromInstance(explosionsPool).AsSingle();

            PlayerMissilePool playerMissilePool = Container
                .InstantiatePrefabForComponent<PlayerMissilePool>(_playerMissilePool, _poolsParent);
            Container.Bind<PlayerMissilePool>().FromInstance(playerMissilePool).AsSingle();

            WallCrashEffectsPool wallCrashEffectsPool = Container
                .InstantiatePrefabForComponent<WallCrashEffectsPool>(_wallCrashEffectsPool, _poolsParent);
            Container.Bind<WallCrashEffectsPool>().FromInstance(wallCrashEffectsPool).AsSingle();

            ProjectileHitEffectPool projectileHitEffectPool = Container
                .InstantiatePrefabForComponent<ProjectileHitEffectPool>(_projectileHitEffectPool, _poolsParent);
            Container.Bind<ProjectileHitEffectPool>().FromInstance(projectileHitEffectPool).AsSingle();
        }
    }
}