using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: add a slow stop to make the stop less instantaneous and smoother?

public class PlayerTracker : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform trackedObject;
    public float distance = 10;
    //public float moveSpeed = 20;
    public float updateSpeed = 10;
    //[Range(0, 10)]
    public float currentDistance = 5;
    //private string moveAxis = "Mouse ScrollWheel";
    private GameObject ahead;
    private MeshRenderer  _renderer;
    public float hideDistance = 1.5f;

    void Start()
    {
       /// ahead = new GameObject("ahead");
       // _renderer = trackedObject.gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        /// ahead.transform.position = trackedObject.position + trackedObject.forward * (distance * 0.25f);
        // currentDistance += Input.GetAxisRaw(moveAxis) * moveSpeed * Time.deltaTime;
        /// currentDistance = Mathf.Clamp(currentDistance, 0, distance);
        /// transform.position = Vector3.MoveTowards(transform.position, trackedObject.position, updateSpeed * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, trackedObject.position + Vector3.up * currentDistance - trackedObject.forward * (currentDistance + distance * 0.5f), updateSpeed * Time.deltaTime);
        /// transform.LookAt(ahead.transform);
        //_renderer.enabled = (currentDistance > hideDistance);
        transform.position = trackedObject.transform.position + new Vector3(-1, 8, -1);
    }
}
