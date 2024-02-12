namespace Tanks.Features.Enviroment
{
    using UnityEngine;
    using Tanks.Features.Player;

    /// <summary>
    /// Триггер, добавляющий ракеты игроку
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class AddMissilesTrigger : MonoBehaviour
    {
        [SerializeField, Min(1)]
        protected int missileCount = 4;

        protected Collider2D coll = default;
        protected PlayerMissileLaunchController controller = default;

        protected virtual void Awake()
        {
            coll = GetComponent<Collider2D>();
            coll.isTrigger = true;
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            controller = collision.GetComponent<PlayerMissileLaunchController>();

            if (controller != null)
            {
                controller.ChangeMissileCount(missileCount);
                coll.enabled = false;
            }
        }
    }
}
