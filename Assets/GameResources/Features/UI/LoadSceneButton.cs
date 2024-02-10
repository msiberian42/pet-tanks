namespace Tanks.Features.UI
{
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// Кнопка загрузки сцены
    /// </summary>
    public class LoadSceneButton : BaseButton
    {
        [SerializeField]
        protected SceneAsset scene = default;

        protected override void OnButtonClicked() => SceneManager.LoadScene(scene.name);
    }
}
