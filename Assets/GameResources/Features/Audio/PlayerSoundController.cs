namespace Tanks.Features.Audio
{
    using UnityEngine;
    using Tanks.Features.Player;

    /// <summary>
    /// Контроллер звуков игрока
    /// </summary>
    public class PlayerSoundController : MonoBehaviour
    {
        [SerializeField]
        protected PlayerShootingController shootingController = default;

        [SerializeField]
        protected AudioSource shootSound = default;
        [SerializeField]
        protected AudioSource reloadSound = default;

        protected float minPitch = 0.95f;
        protected float maxPitch = 1.05f;

        protected virtual void Awake()
        {
            shootingController.onShootEvent += PlayShootSound;
            shootingController.onReloadingFinishedEvent += PlayReloadSound;
        }

        protected virtual void OnDestroy()
        {
            shootingController.onShootEvent -= PlayShootSound;
            shootingController.onReloadingFinishedEvent -= PlayReloadSound;
        }

        protected virtual void PlayShootSound()
        {
            shootSound.pitch = Random.Range(minPitch, maxPitch);
            shootSound.Play();
        }

        protected virtual void PlayReloadSound()
        {
            reloadSound.pitch = Random.Range(minPitch, maxPitch);
            reloadSound.Play();
        }
    }
}