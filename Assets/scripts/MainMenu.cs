using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    string levelName;

    [SerializeField]
    string creditsName;

    public void GotoLevel()
    {
        SceneManager.LoadScene(levelName);
    }

    public void GotoCredits()
    {
        SceneManager.LoadScene(creditsName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
