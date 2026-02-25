using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : Manager<SceneLoadManager>
{
    private string currentScene;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private async void Start()
    {
        await LoadScene("2.LobbyScene"); 
    }

    public async Task LoadScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(currentScene))
        {
            await SceneManager.UnloadSceneAsync(currentScene);
        }

        await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        currentScene = sceneName;
    }
}
