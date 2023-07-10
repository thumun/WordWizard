using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerController : MonoBehaviour
{
    // public Rigidbody2D theRB; 
    public Rigidbody theRB;
    public float moveSpeed;
    public static PlayerController instance;
    public string lastAreaExit;

    // Start is called before the first frame update
    void Start()
    {
        // Prevents duplicate players in a scene when entering and exiting 
        DontDestroyOnLoad(gameObject);
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Basic movement code - will need to be expanded when art/animations are added 
        //theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed; 2D
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 tempVect = new Vector3(h, 0, v);
        tempVect = tempVect.normalized * moveSpeed * Time.deltaTime;
        theRB.MovePosition(transform.position + tempVect); 
    }
}
