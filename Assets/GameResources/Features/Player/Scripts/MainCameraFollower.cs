namespace Tanks.Features.Player
{
    using UnityEngine;

    /// <summary>
    /// Скрипт для привязки камеры к игроку
    /// </summary>
    public class MainCameraFollower : MonoBehaviour
    {
        [SerializeField]
        protected float smoothing = 1f;

        [SerializeField]
        protected float offsetZ = -10f;

        [SerializeField]
        protected Transform player = default;

        protected Camera cam = default;
        protected Vector3 offset = Vector3.zero;

        protected virtual void Awake()
        {
            cam = Camera.main;
            offset.z = offsetZ;
        }

        protected virtual void FixedUpdate() => Move();

        protected virtual void Move()
        {
            cam.transform.position = Vector3.Lerp(
                cam.transform.position, player.transform.position + offset, smoothing * Time.fixedDeltaTime);
        }
    }
}