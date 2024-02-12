namespace Tanks.Features.UI
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// Кнопка загрузки сцены
    /// </summary>
    public class LoadSceneButton : BaseButton
    {
        [SerializeField]
        protected string sceneName = default;

        protected override void OnButtonClicked() => SceneManager.LoadScene(sceneName);
    }
}
