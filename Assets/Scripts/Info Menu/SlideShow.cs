using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideShow : MonoBehaviour
{
    public GameObject Inc;

    public List<GameObject> Slides;

    public int countIncrementer;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject g in Slides)
        {
            g.SetActive(false);
        }
        Slides[0].SetActive(true);
    }

    public void setSlide()
    {
        countIncrementer = Inc.GetComponent<Incrementer>().countNum;
        foreach (GameObject g in Slides)
        {
            g.SetActive(false);
        }
        Slides[countIncrementer].SetActive(true);
    }
}
