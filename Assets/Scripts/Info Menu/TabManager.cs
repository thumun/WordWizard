using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    public List<GameObject> Tabs;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Tabs.Count; i++)
        {
            Tabs[i].transform.SetSiblingIndex(Tabs.Count - 1 - i);
        }
    }

    public void TabSwitch(GameObject Tab)
    {
        for (int i = 0; i < Tabs.Count; i++)
        {
            Tabs[i].transform.SetSiblingIndex(Tabs.Count - 1 - i);
        }
        Tab.transform.SetSiblingIndex(Tabs.Count - 1);
    }

    public void ResetTabs()
    {
        for (int i = 0; i < Tabs.Count; i++)
        {
            Tabs[i].transform.SetSiblingIndex(Tabs.Count - 1 - i);
        }
    }
}
