namespace Tanks.Features.UI
{
    using UnityEngine;

    /// <summary>
    /// Кастомное отображение курсора
    /// </summary>
    public class CursorView : MonoBehaviour
    {
        [SerializeField]
        protected Texture2D cursorTexture = default;

        protected virtual void OnEnable() => 
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);

        protected virtual void OnDisable() =>
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
