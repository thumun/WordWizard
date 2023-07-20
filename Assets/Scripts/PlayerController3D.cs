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
        if (joystick.Horizontal != 0.0f && joystick.Vertical != 0.0f)
        {

            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

            Vector3 NormInput = Vector3.Normalize(new Vector3(joystick.Horizontal, 0.0f, joystick.Vertical));
            var skewedInput = matrix.MultiplyPoint3x4(NormInput);



            //theRB.velocity = new Vector3(joystick.Horizontal * moveSpeed, theRB.velocity.y, joystick.Vertical * moveSpeed);
            theRB.velocity = new Vector3(skewedInput.x * moveSpeed, theRB.velocity.y, skewedInput.z * moveSpeed);
        }
        else
        {
            theRB.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }
}
