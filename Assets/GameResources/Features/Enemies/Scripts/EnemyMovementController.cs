namespace Tanks.Features.Enemies
{
    using System.Collections;
    using Tanks.Features.Player;
    using UnityEngine;
    using UnityEngine.AI;

    /// <summary>
    /// Контроллер передвижения врага
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMovementController : MonoBehaviour
    {
        [SerializeField, Header("Максимальная дистанция, на которую враг приближается к игроку")]
        protected float followDistance = 5f;

        [SerializeField, Header("Частота обновления информации о позиции о")]
        protected float playerPosUpdateRate = 1f;

        protected NavMeshAgent agent = default;
        protected PlayerMovementController player = default;
        protected Coroutine checkPlayerPositionRoutine = default;

        protected virtual void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;

            player = FindAnyObjectByType<PlayerMovementController>();
        }

        protected virtual void OnEnable()
        {
            checkPlayerPositionRoutine = StartCoroutine(CheckPlayerPositionRoutine());
        }

        protected virtual void OnDisable()
        {
            if (checkPlayerPositionRoutine != null)
            {
                StopCoroutine(checkPlayerPositionRoutine);
                checkPlayerPositionRoutine = null;
            }
        }

        protected virtual IEnumerator CheckPlayerPositionRoutine()
        {
            while (isActiveAndEnabled)
            {
                if (Vector3.Distance(transform.position, player.transform.position) >= followDistance)
                {
                    agent.SetDestination(player.transform.position);
                }
                else
                {
                    agent.SetDestination(transform.position);
                }
                yield return new WaitForSeconds(playerPosUpdateRate);
            }
        }
    }
}