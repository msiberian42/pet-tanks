namespace Tanks.Features.Player
{
    using UnityEngine;

    /// <summary>
    /// ���������� ����� ������
    /// </summary>
    public class PlayerShootingController : MonoBehaviour
    {
        [SerializeField]
        protected Transform turret = default;

        protected Vector3 direction = default;
        protected float angle = 0f;

        /// <summary>
        /// ������� �����
        /// </summary>
        /// <param name="target"></param>
        public virtual void RotateTurret(Vector3 target)
        {
            direction = target - turret.transform.position;

            direction.Normalize();

            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            turret.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }
}