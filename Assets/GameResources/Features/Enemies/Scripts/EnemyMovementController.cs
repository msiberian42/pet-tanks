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

        [SerializeField, Header("Частота обновления информации о позиции игрока")]
        protected float playerPosUpdateRate = 1f;

        [SerializeField]
        protected LayerMask wallsLayer = default;

        protected NavMeshAgent agent = default;
        protected PlayerMovementController player = default;
        protected Coroutine checkPlayerPositionRoutine = default;
        protected Vector3 lastPlayerPos = default;

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
                if (PlayerIsVisible())
                {
                    lastPlayerPos = player.transform.position;

                    if (Vector3.Distance(transform.position, player.transform.position) >= followDistance)
                    {

                        agent.SetDestination(lastPlayerPos);

                    }
                    else if (Vector3.Distance(transform.position, player.transform.position) < followDistance)
                    {
                        agent.SetDestination(transform.position);
                    }
                }
                else if (lastPlayerPos != default && Vector3.Distance(transform.position, lastPlayerPos) >= 1f)
                {
                    agent.SetDestination(lastPlayerPos);
                }
                else if (lastPlayerPos != default)
                {
                    agent.SetDestination(player.transform.position);
                    lastPlayerPos = default;
                }

                yield return new WaitForSeconds(playerPosUpdateRate);
            }
        }

        protected virtual bool PlayerIsVisible()
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, player.transform.position, wallsLayer);

            if (hit.collider != null)
            {
                return false;
            }

            return true;
        }

        protected virtual void Rotate(Vector3 target)
        {

        }
    }
}