using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joystickController : MonoBehaviour
{
    //[SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Joystick joystick; 

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


        Vector3 move = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        move.Normalize();
        move = move * Time.deltaTime * 8;

        rigidbody.position = rigidbody.position + move; 

        //rigidbody.velocity = new Vector3(joystick.Horizontal, rigidbody.velocity.y, joystick.Vertical);
        //rigidbody.velocity.Normalize();
        //rigidbody.velocity = rigidbody.velocity * 10;
    }
}
