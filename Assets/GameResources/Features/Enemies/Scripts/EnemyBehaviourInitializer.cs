namespace Tanks.Features.Enemies
{
    using Tanks.Features.Player;
    using UnityEngine;

    /// <summary>
    /// Инициализатор всех поведений врага
    /// </summary>
    public class EnemyBehaviourInitializer : MonoBehaviour
    {
        [SerializeField]
        protected ChasingEnemyBehaviour chasingBehaviour = default;
        [SerializeField]
        protected PatrolEnemyBehaviour patrolBehaviour = default;
        [SerializeField]
        protected AttackEnemyBehaviour attackBehaviour = default;

        [SerializeField]
        protected EnemyBehaviourController controller = default;

        protected PlayerMovementController player = default;

        protected virtual void Awake()
        {
            player = FindAnyObjectByType<PlayerMovementController>();

            patrolBehaviour.Init(controller, player.transform);
            chasingBehaviour.Init(controller, player.transform);
            attackBehaviour.Init();
        }
    }
}