namespace Tanks.Features.UI
{
    using UnityEngine;
    using Tanks.Features.Enemies;

    /// <summary>
    /// Контроллер канваса врага
    /// </summary>
    public class EnemyCanvasController : MonoBehaviour
    {
        [SerializeField]
        protected EnemyHealthController controller = default;

        [SerializeField]
        protected EnemyHealthView canvasPrefab = default;

        protected EnemyHealthView healthCanvas = default;

        protected virtual void Awake()
        {
            healthCanvas = Instantiate(canvasPrefab, transform.parent);
            healthCanvas.SetHealthController(controller);
        }

        protected virtual void OnDestroy() => Destroy(healthCanvas);

        protected virtual void OnEnable() => healthCanvas?.gameObject.SetActive(true);

        protected virtual void OnDisable() => healthCanvas?.gameObject.SetActive(false);

        protected virtual void Update() => 
            healthCanvas.transform.position = controller.transform.position;
    }
}