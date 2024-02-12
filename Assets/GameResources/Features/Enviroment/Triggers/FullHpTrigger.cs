namespace Tanks.Features.Enviroment
{
    using UnityEngine;
    using Tanks.Features.Player;

    /// <summary>
    /// Триггер, восстанавливающий здоровье игроку
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class FullHpTrigger : MonoBehaviour
    {
        protected Collider2D coll = default;
        protected PlayerHealthController playerHealthController = default;

        protected virtual void Awake()
        {
            coll = GetComponent<Collider2D>();
            coll.isTrigger = true;
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            playerHealthController = collision.GetComponent<PlayerHealthController>();

            if (playerHealthController != null)
            {
                playerHealthController.SetMaxHP();
                coll.enabled = false;
            }
        }
    }
}