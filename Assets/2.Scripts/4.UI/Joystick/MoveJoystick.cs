using EtudeProject;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MoveJoystick : MonoBehaviour
{
    public CharacterController owner;
    public FloatingJoystick joystick;

    private void Awake()
    {
        joystick = GetComponent<FloatingJoystick>();
    }
       

    // Update is called once per frame
    void Update()
    {            
        if (owner == null)
        {
            return;
        }

        owner.moveInput = joystick.Direction;

        Vector3 dir = new Vector3(joystick.Horizontal, 0 , joystick.Vertical).normalized;

        owner.Move(dir);

        if (joystick.Direction != Vector2.zero)
        {
            owner.ChangeState(CharacterStateType.Move);
        }
        else
        {
            owner.ChangeState(CharacterStateType.Idle);
        }

        

    }

    public void JoystickConnection(CharacterController contoller)
    {
        owner = contoller;
    }
}
