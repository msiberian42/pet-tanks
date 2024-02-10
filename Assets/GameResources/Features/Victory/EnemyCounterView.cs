namespace Tanks.Features.Victory 
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// ¬ью счетчика врагов
    /// </summary>
    public class EnemyCounterView : MonoBehaviour
    {
        [SerializeField]
        protected Text textView = default;

        [SerializeField]
        protected EnemyCounterVictoryController counter = default;

        protected virtual void Awake() => counter.onEnemyCountChangedEvent += OnEnemyCountChanged;

        protected virtual void Start() => OnEnemyCountChanged();

        protected virtual void OnDestroy() => counter.onEnemyCountChangedEvent -= OnEnemyCountChanged;

        protected virtual void OnEnemyCountChanged() => 
            textView.text = counter.Enemies.Count.ToString();
    }
}