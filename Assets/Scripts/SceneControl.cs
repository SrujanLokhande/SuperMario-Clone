using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        GameManager.Instance.Panel.SetActive(false);
       
    }

    public void Quit()
    {
        Application.Quit();
    }
}
