namespace Tanks.Features.UI
{
    using UnityEngine;

    /// <summary>
    /// Блокировщик кнопки загрузки уровня
    /// </summary>
    public class SelectLevelButtonBlocker : MonoBehaviour
    {
        [SerializeField]
        protected int levelNumber = 1;

        [SerializeField]
        protected GameObject blocker = default;

        protected int lastLevel = 0;

        protected const string LAST_LEVEL = "Last passed level";

        protected virtual void OnEnable()
        {
            if (!PlayerPrefs.HasKey(LAST_LEVEL))
            {
                PlayerPrefs.SetInt(LAST_LEVEL, 0);
                PlayerPrefs.Save();
            }

            lastLevel = PlayerPrefs.GetInt(LAST_LEVEL);

            blocker.SetActive(!(levelNumber <= lastLevel + 1));
        }
    }
}