using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FillBlanks : MonoBehaviour
{
    public TMP_Text Sample1;
    public TMP_Text Sample2;

    public double number;

    // Update is called once per frame
    void Update()
    {
        Sample1.text = $"Monospace: {number:N0}".Monospace();
        Sample2.text = $"Monospace: {number.ToString("N0").Monospace()}";

        number += 250 * Time.deltaTime;
    }
}
