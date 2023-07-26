using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InfoMenu : MonoBehaviour
{
    public GameObject Info;

    [SerializeField]
    public List<GameObject> deactivatedTabs;

    public bool showing;

    // Start is called before the first frame update
    void Start()
    {
        Info.SetActive(showing);
    }

    public void InfoClick()
    {
        showing = !showing;
        Info.SetActive(showing);
        foreach (GameObject g in deactivatedTabs)
        {
            g.SetActive(!showing);
        }
    }
}
