namespace Tanks.Features.Enemies
{
    using Tanks.Features.Player;
    using UnityEngine;
    using Zenject;

    /// <summary>
    /// Триггер перехода из преследования в атаку
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public sealed class AttackingTriggers : MonoBehaviour
    {
        [SerializeField]
        private EnemyTankStateController _controller = default;
        [SerializeField]
        private EnemyBehaviourInitializer _initializer = default;

        private PlayerMovementController _player = default;
        private Collider2D _coll = default;

        [Inject]
        private void Construct(PlayerController player) => _player = player.MovementController;

        private void Awake()
        {
            _coll = GetComponent<Collider2D>();
            _coll.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_player != null && collision.gameObject == _player.gameObject && _controller.TargetIsVisible)
            {
                _initializer.SetAttackBehaviour();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (_player != null && collision.gameObject == _player.gameObject)
            {
                _initializer.SetChasingBehaviour();
            }
        }
    }
}