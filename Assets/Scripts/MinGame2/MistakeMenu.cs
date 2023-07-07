using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistakeMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject oops;

    public void onClick()
    {
        oops.SetActive(false);
    }
}
