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

        protected virtual void OnEnable()
        {
            Cursor.visible = false;
            cursorSprite.enabled = true;
        }

        protected virtual void Update() => 
            cursorSprite.transform.position = Input.mousePosition;

        protected virtual void OnDisable()
        {
            Cursor.visible = true;
            cursorSprite.enabled = false;
        }
    }
}
