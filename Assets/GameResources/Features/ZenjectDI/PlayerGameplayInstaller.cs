namespace Tanks.Features.DI
{
    using Zenject;
    using UnityEngine;
    using Tanks.Features.Player;

    /// <summary>
    /// Инсталлер зависимостей игрока
    /// </summary>
    public sealed class PlayerGameplayInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerController _playerPrefab = default;

        [SerializeField]
        private Transform _playerSpawnPosition = default;

        public override void InstallBindings()
        {
            PlayerController playerController = Container
                .InstantiatePrefabForComponent<PlayerController>(_playerPrefab, 
                _playerSpawnPosition.position, Quaternion.identity, null);

            Container.Bind<PlayerController>().FromInstance(playerController).AsSingle();
        }
    }
}
