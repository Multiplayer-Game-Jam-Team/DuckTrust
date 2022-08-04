using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : Singleton<SceneLoader>
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

