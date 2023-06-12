using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D theRB;
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
        theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
    }
}
