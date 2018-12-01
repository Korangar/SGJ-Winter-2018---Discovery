using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public string level;
    public LoadSceneMode loadMode;

    private void OnEnable()
    {
        SceneManager.LoadScene(level, loadMode);
    }

    private void OnDisable()
    {
        SceneManager.UnloadSceneAsync(level);
    }
}
