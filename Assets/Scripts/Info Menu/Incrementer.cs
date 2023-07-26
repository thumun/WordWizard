using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Incrementer : MonoBehaviour
{
    public GameObject Show;
    public GameObject Count;
    private TMP_Text CountText;
    public int countNum;

    private int totalSlides;

    // Start is called before the first frame update
    void Start()
    {
        totalSlides = Show.GetComponent<SlideShow>().Slides.Count;
        countNum = 0;
        CountText = Count.GetComponent<TMP_Text>();
        UpdateText(countNum);
    }

    private void UpdateText(int cur)
    {
        if (CountText != null)
        {
            CountText.text = (cur + 1) + " / " + totalSlides;
        }
    }

    public void IterateSlides(int iter)
    {
        int realIter = (countNum + iter) % totalSlides;
        if (realIter < 0)
        {
            realIter = realIter + totalSlides;
        }
        countNum = realIter;
        UpdateText(countNum);
    }

    public void SetSlides(int count)
    {
        countNum = count;
        UpdateText(countNum);
    }

    public void ResetSlides()
    {
        countNum = 0;
        UpdateText(0);
    }
}
