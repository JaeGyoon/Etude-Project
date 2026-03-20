using UnityEditor;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(StatHandler))]
[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    private CharacterController controller;
    private Rigidbody rb;

    // 추후 스탯
    public float moveSpeed = 5f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Move(Vector3 direction)
    {
        if (direction == Vector3.zero)
        {
            return;
        }

        //Debug.Log(direction);

        Vector3 moveDir = direction.normalized * moveSpeed;

        if ((moveDir != Vector3.zero) )
        {
            transform.rotation = Quaternion.LookRotation(moveDir);
        }

        rb.linearVelocity = moveDir;

    }
}
