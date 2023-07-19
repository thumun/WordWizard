using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NReco.Csv;
using System.IO;

public class NPCDialogue
{
    public string CharPhrase { get; set; }
    public string McPhrase { get; set; }
    public bool NeedPredecessor { get; set; }

    public NPCDialogue(string charPhrase, string mcPhrase, bool needPredecessor)
    {
        CharPhrase = charPhrase;
        McPhrase = mcPhrase;
        NeedPredecessor = needPredecessor;
    }
}

public class NPC
{
    public string Name { get; private set; }
    public List<NPCDialogue> CharDialogue { get; set; }

    public NPC(string name)
    {
        Name = name;
        CharDialogue = new List<NPCDialogue>();
    }
}

public class NPCCollection
{
    public List<NPC> NPCS { get; set; }

}


public class SetDialogue
{
    private static NPCCollection data = null;
    private static string _file = "";

    public static NPCCollection GetDialogueData()
    {
        if (data == null)
        {
            //load data from the file
            data = readCSV(_file);
        }

        return data;
    }

    public static void SetLoadFile(string file)
    {
        _file = file;
    }

    public static NPCCollection readCSV(string file)
    {
        NPCCollection data = new NPCCollection();

        using (var streamRdr = new StreamReader(System.IO.Directory.GetCurrentDirectory() + @"/Assets/Data/" + file + ".csv"))
        {
            var csvReader = new CsvReader(streamRdr, ",");

            // skip 1st line 
            if (csvReader.Read())
            {

            }

            string currChar = "";
            int indx = 0;

            while (csvReader.Read())
            {
                if (currChar=="" || currChar != csvReader[0])
                {
                    currChar = csvReader[0];
                    NPC npcChar = new NPC(currChar);

                    NPCDialogue dialogue = new NPCDialogue(csvReader[1], csvReader[2], csvReader[3] != "N");

                    npcChar.CharDialogue.Add(dialogue);

                    data.NPCS.Add(npcChar);
                    indx++; 
                }
                else
                {
                    data.NPCS[indx].CharDialogue.Add(new NPCDialogue(csvReader[1], csvReader[2], csvReader[3] != "N"));
                }
            }
        }

        return data;
    }
}

