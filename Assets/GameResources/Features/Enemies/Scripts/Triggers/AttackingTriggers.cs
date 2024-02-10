namespace Tanks.Features.Enemies
{
    using Tanks.Features.Player;
    using UnityEngine;

    /// <summary>
    /// Триггер перехода из преследования в атаку
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class AttackingTriggers : MonoBehaviour
    {
        [SerializeField]
        private EnemyBehaviourController _controller = default;
        [SerializeField]
        private EnemyBehaviourInitializer _initializer = default;

        private PlayerMovementController _player = default;
        private Collider2D _coll = default;

        private void Awake()
        {
            _coll = GetComponent<Collider2D>();
            _coll.isTrigger = true;

            _player = FindAnyObjectByType<PlayerMovementController>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_player != null && collision.gameObject == _player.gameObject && _controller.PlayerIsVisible)
            {
                _initializer.SetAttackBehaviour();
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (_player != null && collision.gameObject == _player.gameObject
                && _controller.CurrentBehaviour == _initializer.ChasingBehaviourInstance
                && _controller.PlayerIsVisible)
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