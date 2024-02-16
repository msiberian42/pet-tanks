namespace Tanks.Features.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using Tanks.Features.Player;
    using Tanks.Features.Victory;
    using Zenject;

    /// <summary>
    /// Кастомное отображение курсора
    /// </summary>
    public class CursorView : MonoBehaviour
    {
        [SerializeField]
        protected Image cursorSprite = default;

        protected PauseController pauseController = default;
        protected PlayerHealthController playerHealthController = default;

        [Inject]
        protected virtual void Construct(PlayerController player)
        {
            playerHealthController = player.HealthController;
            playerHealthController.onPlayerDeathEvent += DisableCustomCursor;
        }

        protected virtual void Start()
        {
            pauseController = FindAnyObjectByType<PauseController>();
            pauseController.onPauseEnabled += DisableCustomCursor;
            pauseController.onPauseDisabled += EnableCustomCursor;
            
            VictoryController.onVictoryEvent += DisableCustomCursor;
        }

        protected virtual void OnDestroy()
        {
            pauseController.onPauseEnabled -= DisableCustomCursor;
            pauseController.onPauseDisabled -= EnableCustomCursor;
            playerHealthController.onPlayerDeathEvent -= DisableCustomCursor;
            VictoryController.onVictoryEvent -= DisableCustomCursor;
        }

        protected virtual void OnEnable() => EnableCustomCursor();

        protected virtual void Update() => 
            cursorSprite.transform.position = Input.mousePosition;

        protected virtual void OnDisable() => DisableCustomCursor();

        protected virtual void EnableCustomCursor()
        {
            Cursor.visible = false;
            cursorSprite.enabled = true;
        }

        protected virtual void DisableCustomCursor()
        {
            Cursor.visible = true;
            cursorSprite.enabled = false;
        }
    }
}
