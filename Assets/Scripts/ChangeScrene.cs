using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScrene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void moveToScene(int SceneId)
    {
        SceneManager.LoadScene(SceneId);
    }

    public void quitMM()
    {
        Application.Quit();
    }
}
