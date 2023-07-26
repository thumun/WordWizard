using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NReco.Csv;
using System;
using System.IO;

public class Dictionary : MonoBehaviour
{
    public GameObject DictionaryObject;

    public GameObject DictContainer;
    private IDictionary<string, DictionaryEntry> dict;

    public GameObject definition;
    private TMP_Text def;

    private string curTerm { get; set; }
    private string prevTerm;

    void Start()
    {
        def = definition.GetComponent<TMP_Text>();

        dict = DictContainer.GetComponent<DictContainer>().dict;
    }

    // Update is called once per frame
    void Update()
    {
        if (dict == null)
        {
            dict = DictContainer.GetComponent<DictContainer>().dict;
        }
        else
        {
            if (prevTerm != curTerm)
            {
                if (dict.ContainsKey(curTerm.ToLower()))
                {
                    def.text = dict[curTerm.ToLower()].Definition;
                }
                else
                {
                    def.text = "This word is either a name or has no definition in the dictionary";
                }
                prevTerm = curTerm;
            }
        }
    }

    public void SetDef(string input)
    {
        if (dict == null)
        {
            dict = DictContainer.GetComponent<DictContainer>().dict;
        }
        curTerm = input;

        if (curTerm != "" && curTerm != null)
        {
            if (dict.ContainsKey(curTerm.ToLower()))
            {
                DictionaryObject.SetActive(true);
            }
        }
    }
}