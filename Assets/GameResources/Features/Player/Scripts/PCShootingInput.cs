namespace Tanks.Features.Player
{
    using UnityEngine;
    using static UnityEngine.GraphicsBuffer;

    /// <summary>
    /// Обработчик инпута стрельбы с ПК
    /// </summary>
    public class PCShootingInput : MonoBehaviour
    {
        [SerializeField]
        protected PlayerShootingController controller = default;

        protected Camera cam = default;

        protected virtual void Awake() => cam = Camera.main;

        protected virtual void Update()
        {
            controller.RotateTurret(cam.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
