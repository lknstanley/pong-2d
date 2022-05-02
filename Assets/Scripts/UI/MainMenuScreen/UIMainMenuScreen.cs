using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.MainMenuScreen
{
    public class UIMainMenuScreen : MonoBehaviour
    {
        #region Event Handlers

        public void OnStartGameClicked()
        {
            SceneManager.LoadScene( "MainGameScene" );
        }

        public void OnExitGameClicked()
        {
            Application.Quit();
        }

        #endregion
    }
}