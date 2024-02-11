namespace Tanks.Features.Enviroment
{
    using UnityEngine;
    using Tanks.Features.Player;
    using Tanks.Features.Enemies;

    /// <summary>
    /// Разрушаемая стена
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class BreakableWall : MonoBehaviour
    {
        protected WallCrashEffectsPool crashPool = default;
        protected WallCrashEffect crashEffect = default;

        protected PlayerMovementController player = default;
        protected EnemyBehaviourController enemy = default;

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            player = collision.gameObject.GetComponent<PlayerMovementController>();

            if (player != null)
            {
                DestroyWall();
                return;
            }

            enemy = collision.gameObject.GetComponent<EnemyBehaviourController>();

            if (enemy != null)
            {
                DestroyWall();
            }
        }

        protected virtual void DestroyWall()
        {
            if (crashPool == null)
            {
                crashPool = FindAnyObjectByType<WallCrashEffectsPool>();
            }

            crashEffect = crashPool.GetObject();
            crashEffect.transform.position = transform.position;

            gameObject.SetActive(false);
        }
    }
}