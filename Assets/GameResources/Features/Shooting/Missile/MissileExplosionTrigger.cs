namespace Tanks.Features.Shooting
{
    using Tanks.Features.Enemies;
    using UnityEngine;

    /// <summary>
    /// Триггер взрыва ракеты
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public sealed class MissileExplosionTrigger : MonoBehaviour
    {
        [SerializeField]
        private PlayerMissile _missile = default;

        private EnemyHealthController _enemyHealthController = default; 

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.layer == _missile.WallsLayer)
            {
                _missile.Explode();
                return;
            }

            _enemyHealthController = collision.gameObject.GetComponent<EnemyHealthController>();

            if (_enemyHealthController != null)
            {
                _missile.Explode();
            }
        }
    }
}
