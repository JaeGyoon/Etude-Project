using EtudeProject;
using UnityEngine;

public class CharacterController : MonoBehaviour
{    
    public Animator animator;

    public Vector3 moveInput;

    private CharacterMovement movement;
    private CharacterCombat combat;
    private CharacterHealth health;
    private CharacterStateMachine stateMachine;
        
    public FloatingJoystick moveJoystick;
    private void Awake()
    {
        movement = GetComponent<CharacterMovement>();
        combat = GetComponent<CharacterCombat>();
        health = GetComponent<CharacterHealth>();

        stateMachine = new CharacterStateMachine(this);
    }

    private void Start()
    {
        stateMachine.ChangeState(CharacterStateType.Idle);
    }

    private void Update()
    {
        stateMachine.StayState();
    }

    public void ChangeState(CharacterStateType type)
    {
        stateMachine.ChangeState(type);
    }

    public void Move(Vector3 direction)
    {
        movement.Move(direction);
    }

    
}
