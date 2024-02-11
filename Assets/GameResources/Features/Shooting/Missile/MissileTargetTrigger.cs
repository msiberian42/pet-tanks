namespace Tanks.Features.Shooting
{
    using Tanks.Features.Enemies;
    using UnityEngine;

    /// <summary>
    /// Триггер поиска цели для ракеты
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public sealed class MissileTargetTrigger : MonoBehaviour
    {
        [SerializeField]
        private PlayerMissile _missile = default;

        private EnemyHealthController _enemyHealthController = default;
        private bool _hasTarget = false;

        private void OnEnable() => _hasTarget = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_hasTarget)
            {
                return;
            }

            _enemyHealthController = collision.gameObject.GetComponent<EnemyHealthController>();

            if (_enemyHealthController != null && CheckTargetVisibility(_enemyHealthController.transform.position))
            {
                _missile.SetTarget(_enemyHealthController.transform);
                _hasTarget = true;
            }
        }

        private bool CheckTargetVisibility(Vector3 target)
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, target, _missile.WallsLayer);

            if (hit.collider != null)
            {
                return false;
            }

            return true;
        }
    }
}