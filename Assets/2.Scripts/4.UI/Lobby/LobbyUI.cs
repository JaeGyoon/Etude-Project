using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] PreviewSpawner spawner;

    public async void OnClickGameStartBtn()
    {
        if (spawner.currentHero != null)
        {
            Destroy(spawner.currentHero);
            spawner.currentHero = null;
        }

        await SceneLoadManager.Instance.LoadScene("3.GameScene");
    }
}
