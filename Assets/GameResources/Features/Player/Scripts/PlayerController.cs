namespace Tanks.Features.Player
{
    using UnityEngine;

    /// <summary>
    /// Контроллер игрока
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [field: SerializeField]
        public PlayerMovementController MovementController { get; private set; } = default;

        [field: SerializeField]
        public PlayerHealthController HealthController { get; private set; } = default;

        [field: SerializeField]
        public PlayerShootingController ShootingController { get; private set; } = default;

        [field: SerializeField]
        public PlayerMissileLaunchController MissileController { get; private set; } = default;
    }
}