namespace Tanks.Features.Player
{
    using UnityEngine;

    /// <summary>
    /// Скрипт для привязки камеры к игроку
    /// </summary>
    public class MainCameraFollower : MonoBehaviour
    {
        [SerializeField]
        protected float speed = 1f;

        [SerializeField]
        protected Transform player = default;

        protected Camera cam = default;

        protected virtual void Awake() => cam = Camera.main;

        protected virtual void FixedUpdate() => Move();

        protected virtual void Move()
        {
            var nextPos = Vector3.Lerp(
                cam.transform.position, player.transform.position, speed * Time.fixedDeltaTime);

            cam.transform.position = new Vector3(nextPos.x, nextPos.y, cam.transform.position.z);
        }
    }
}