using UnityEngine;
using System.Threading.Tasks;
public class HeroSpanwer : PreviewSpawner
{
    public MoveJoystick moveJoystick;

    protected async void Start()
    {
        spawnPoint = this.transform;
        GameObject go = await SpawnHero();

        Debug.Log($"<color=red> {go.name} </color>");
        JoystickConnection(go);
        
    }

    void JoystickConnection(GameObject hero)
    {
        CharacterController controller = hero.GetComponent<CharacterController>();

        moveJoystick.owner = controller;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
