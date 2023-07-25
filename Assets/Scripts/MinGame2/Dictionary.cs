using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NReco.Csv;
using System;
using System.IO;

public struct DictionaryEntry
{
    public string PartOfSpeech { get; set; }
    public string Definition { get; set; }
}

public class Dictionary : MonoBehaviour
{
    public GameObject definition;
    private TMP_Text def;

    public string curTerm { get; set; }
    private string prevTerm;

    private IDictionary<string, DictionaryEntry> dict;

    // Start is called before the first frame update
    void Start()
    {
        def = definition.GetComponent<TMP_Text>();

        dict = new Dictionary<string, DictionaryEntry>();

        string tempTerm;
        DictionaryEntry tempEntry;
        using (var streamRdr = new StreamReader(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Assets", "Data", "dictionary.csv")))
        {
            var csvReader = new CsvReader(streamRdr, ",");

            // skip 1st line 
            if (csvReader.Read())
            {

            }

            while (csvReader.Read())
            {
                Debug.Log(csvReader);
                tempTerm = csvReader[0];
                if (!dict.ContainsKey(tempTerm.ToLower()))
                {
                    tempEntry = new DictionaryEntry
                    {
                        PartOfSpeech = csvReader[1],
                        Definition = csvReader[2]
                    };

                    dict.Add(tempTerm.ToLower(), tempEntry);
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
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