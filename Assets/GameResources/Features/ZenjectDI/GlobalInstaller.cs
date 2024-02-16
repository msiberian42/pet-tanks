namespace Tanks.Features.DI
{
    using UnityEngine;
    using Zenject;
    using Tanks.Features.Audio;

    /// <summary>
    /// Инсталлер глобальных зависимостей
    /// </summary>
    public sealed class GlobalInstaller : MonoInstaller
    {
        [SerializeField]
        private AudioVolumeController _audioVolumeController = default;

        public override void InstallBindings()
        {
            AudioVolumeController audioVolumeController = Container
                .InstantiatePrefabForComponent<AudioVolumeController>(_audioVolumeController);
            Container.Bind<AudioVolumeController>().FromInstance(audioVolumeController).AsSingle();
        }
    }
}
