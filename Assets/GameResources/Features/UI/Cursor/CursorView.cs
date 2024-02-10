namespace Tanks.Features.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Кастомное отображение курсора
    /// </summary>
    public class CursorView : MonoBehaviour
    {
        [SerializeField]
        protected Image cursorSprite = default;

        protected PauseController pauseController = default;

        protected virtual void Awake()
        {
            pauseController = FindAnyObjectByType<PauseController>();
            pauseController.onPauseEnabled += DisableCustomCursor;
            pauseController.onPauseDisabled += EnableCustomCursor;
        }

        protected virtual void OnDestroy()
        {
            pauseController.onPauseEnabled -= DisableCustomCursor;
            pauseController.onPauseDisabled -= EnableCustomCursor;
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
