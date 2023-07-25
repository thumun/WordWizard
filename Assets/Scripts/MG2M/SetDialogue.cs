using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NReco.Csv;
using System.IO;
using System.Linq;

public class Predecessor
{
    public bool NeedPredecessor { get; private set; }
    public string PredecessorName { get; private set; }

    public Predecessor(bool needPredecessor, string predecessorName)
    {
        NeedPredecessor = needPredecessor;
        PredecessorName = predecessorName;
    }
}

public class NPCDialogue
{
    public string CharPhrase { get; set; }
    public List<string> McPhrase { get; set; }
    public Predecessor PredecessorInfo { get; set; }
    //public bool NeedPredecessor { get; set; }

    public NPCDialogue(string charPhrase, List<string> mcPhrase, Predecessor predecessorInfo)
    {
        CharPhrase = charPhrase;
        McPhrase = mcPhrase;
        PredecessorInfo = predecessorInfo;
    }
}

public class NPC
{
    public string Name { get; private set; }
    public List<NPCDialogue> CharDialogue { get; set; }
    public int DialoguePosition = 0;

    public NPC(string name)
    {
        Name = name;
        CharDialogue = new List<NPCDialogue>();
    }

    public NPC()
    {
        CharDialogue = new List<NPCDialogue>();
    }

    public void ResetDialogue()
    {
        DialoguePosition = 0; 
    }
}

public class NPCCollection
{
    public List<NPC> NPCS { get; set; }

    public NPCCollection()
    {
        NPCS = new List<NPC>();
    }

    public NPC getNPC(string name)
    {
        NPC temp = new NPC();

        foreach (NPC character in NPCS)
        {
            if (character.Name == name)
            {
                temp = character;
                break;
            }
        }

        return temp; 
    }
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
            int indx = -1;

            while (csvReader.Read())
            {
                NPC npcChar;

                if (currChar=="" || currChar != csvReader[0])
                {
                    currChar = csvReader[0];
                    npcChar = new NPC(currChar);

                    NPCDialogue dialogue = new NPCDialogue(csvReader[1], csvReader[2].Split(',').ToList(), new Predecessor(csvReader[3] != "N", csvReader[3]));
                    
                    npcChar.CharDialogue.Add(dialogue);

                    data.NPCS.Add(npcChar);
                    indx++; 
                }
                else
                {
                    data.NPCS[indx].CharDialogue.Add(new NPCDialogue(csvReader[1], csvReader[2].Split(',').ToList(), new Predecessor(csvReader[3] != "N", csvReader[3])));
                }
            }
        }

        return data;
    }
}

