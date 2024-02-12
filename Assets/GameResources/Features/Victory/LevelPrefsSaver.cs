namespace Tanks.Features.Victory
{
    using UnityEngine;

    /// <summary>
    /// Сохранение количества пройденных уровней в префсы
    /// </summary>
    public class LevelPrefsSaver : MonoBehaviour
    {
        [SerializeField, Min(0)]
        protected int levelNumber = 0;

        protected const string LAST_LEVEL = "Last passed level";

        protected virtual void Awake() => VictoryController.onVictoryEvent += SaveLevel;

        protected virtual void OnDestroy() => VictoryController.onVictoryEvent -= SaveLevel;

        protected virtual void SaveLevel()
        {
            if (PlayerPrefs.GetInt(LAST_LEVEL) < levelNumber)
            {
                PlayerPrefs.SetInt(LAST_LEVEL, levelNumber);
                PlayerPrefs.Save();
            }
        }
    }
}