using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    public Rigidbody theRB;
    public FixedJoystick joystick;
    public Animator animator;

    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = new Vector3(joystick.Horizontal * moveSpeed, theRB.velocity.y, joystick.Vertical * moveSpeed);
    }
}
