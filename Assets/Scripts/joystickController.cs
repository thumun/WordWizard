using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joystickController : MonoBehaviour
{

    protected Joystick joystick; 

    // Start is called before the first frame update
    void Start()
    {
        joystick = FindAnyObjectByType<Joystick>();
    }

    // Update is called once per frame
    void Update()
    {

        var rigidbody = GetComponent<Rigidbody>();
        // normalize velocity --> joystick horizontal && joystick vertical
        //Vector3 test = new Vector3(joystick.Horizontal, rigidbody.velocity.y, joystick.Vertical);
        
        rigidbody.velocity = new Vector3(joystick.Horizontal * 10, rigidbody.velocity.y, joystick.Vertical * 10);
        
    }
}
