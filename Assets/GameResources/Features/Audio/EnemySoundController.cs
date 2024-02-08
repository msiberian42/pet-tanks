namespace Tanks.Features.Audio
{
    using Tanks.Features.Enemies;
    using UnityEngine;

    /// <summary>
    /// Контроллер звуков врага
    /// </summary>
    public class EnemySoundController : MonoBehaviour
    {
        [SerializeField]
        protected EnemyBehaviourInitializer enemyBehaviourInitializer = default;

        [SerializeField]
        protected AudioSource shootSound = default;

        protected AttackEnemyBehaviour attackEnemyBehaviour = default;

        protected float minPitch = 0.95f;
        protected float maxPitch = 1.05f;

        protected virtual void Start()
        {
            attackEnemyBehaviour = enemyBehaviourInitializer.AttackBehaviourInstance;
            attackEnemyBehaviour.onShootEvent += PlayShootSound;
        }

        protected virtual void OnDestroy()
        {
            attackEnemyBehaviour.onShootEvent -= PlayShootSound;
        }

        protected virtual void PlayShootSound()
        {
            shootSound.pitch = Random.Range(minPitch, maxPitch);
            shootSound.Play();
        }
    }
}
