using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void ReloadScene()
    {
        // Reload the current scene
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneName);
    }
}
