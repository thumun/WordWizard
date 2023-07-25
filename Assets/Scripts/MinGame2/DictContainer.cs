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

public class DictContainer : MonoBehaviour
{
    public IDictionary<string, DictionaryEntry> dict { get; set; }

    void Start()
    {
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
                Debug.Log(csvReader[0]);
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
        
    }
}
