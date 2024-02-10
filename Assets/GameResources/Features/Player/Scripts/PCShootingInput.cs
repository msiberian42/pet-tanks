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
        protected Vector3 mousePos = default;

        protected virtual void Awake() => cam = Camera.main;

        protected virtual void Update()
        {
            if (Time.timeScale == 0f)
            {
                return;
            }

            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            controller.RotateTurret(mousePos);

            if (Input.GetMouseButtonDown(0) && controller.IsLoaded)
            {
                controller.Shoot(mousePos);
            }
        }
    }
}
