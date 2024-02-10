namespace Tanks.Features.Enemies
{
    using Tanks.Features.Player;
    using UnityEngine;

    /// <summary>
    /// Триггер перехода из патрулирования в преследование
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public sealed class ChasingTrigger : MonoBehaviour
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
                _initializer.SetChasingBehaviour();
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (_player != null && collision.gameObject == _player.gameObject 
                && _controller.CurrentBehaviour == _initializer.PatrolBehaviourInstance 
                && _controller.PlayerIsVisible)
            {
                _initializer.SetChasingBehaviour();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (_player != null && collision.gameObject == _player.gameObject)
            {
                _initializer.SetPatrolBehaviour();
            }
        }
    }
}
