namespace Tanks.Features.Enemies
{
    using Tanks.Features.Player;
    using UnityEngine;
    using Zenject;

    /// <summary>
    /// Триггер перехода из патрулирования в преследование
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public sealed class ChasingTrigger : MonoBehaviour
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
                _initializer.SetChasingBehaviour();
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (_player != null && collision.gameObject == _player.gameObject 
                && _controller.CurrentBehaviour == _initializer.PatrolBehaviourInstance 
                && _controller.TargetIsVisible)
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
