namespace Tanks.Features.UI
{
    using UnityEngine.SceneManagement;

    /// <summary>
    /// Кнопка перезапуска уровня
    /// </summary>
    public class RestartButton : BaseButton
    {
        protected override void OnButtonClicked() => 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}