namespace Tanks.Features.Enviroment
{
    using UnityEngine;
    using Tanks.Features.Interfaces;

    /// <summary>
    /// Разрушаемая стена
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class BreakableWall : MonoBehaviour, IExplodable
    {
        protected WallCrashEffectsPool crashPool = default;
        protected WallCrashEffect crashEffect = default;

        protected ITankController tank = default;

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            tank = collision.gameObject.GetComponent<ITankController>();

            if (tank != null)
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

        public void GetExplosionDamage(float damage) => DestroyWall();
    }
}