using UnityEngine;
using System.Threading.Tasks;
using Unity.Cinemachine;

public class HeroSpanwer : PreviewSpawner
{
    public MoveJoystick moveJoystick;
    public CinemachineCamera cinemachineCamera;
    public MapGenerator mapGenerator;

    protected override async void Start()
    {
        spawnPoint = this.transform;
        GameObject go = await SpawnHero();

        Debug.Log($"<color=red> {go.name} </color>");

        JoystickConnection(go);
        CameraConnection(go);
        PlayerTargeting(HeroManager.Instance.currentHero);
    }

    void JoystickConnection(GameObject hero)
    {
        CharacterController controller = hero.GetComponent<CharacterController>();

        moveJoystick.owner = controller;
    }

    void CameraConnection(GameObject hero)
    {        
        cinemachineCamera.Follow = hero.transform;
    }

    void PlayerTargeting(GameObject hero)
    {
        mapGenerator.playerPos = hero.transform;
    }
}
