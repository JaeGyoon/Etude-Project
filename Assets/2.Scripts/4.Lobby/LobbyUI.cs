using UnityEngine;

public class LobbyUI : MonoBehaviour
{    
    public async void OnClickGameStartBtn()
    {
        await SceneLoadManager.Instance.LoadScene("3.GameScene");
    }
}
