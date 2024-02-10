namespace Tanks.Features.Player
{
    using UnityEngine;
    using Tanks.Features.UI;
    using Tanks.Features.Victory;

    /// <summary>
    /// Обработчик инпута стрельбы с ПК
    /// </summary>
    public class PCShootingInput : MonoBehaviour
    {
        [SerializeField]
        protected PlayerShootingController controller = default;

        protected Camera cam = default;
        protected Vector3 mousePos = default;
        protected GameOverController gameOverController = default;

        protected virtual void Start()
        {
            cam = Camera.main;

            gameOverController = FindAnyObjectByType<GameOverController>();
            gameOverController.onGameOverEvent += DisableShooting;

            VictoryController.onVictoryEvent += DisableShooting;
        }

        protected virtual void OnDestroy()
        {
            gameOverController.onGameOverEvent -= DisableShooting;
            VictoryController.onVictoryEvent -= DisableShooting;
        }

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

        protected virtual void DisableShooting() => this.enabled = false;
    }
}
