using System.Collections;
using System.Collections.Generic;
using NReco.Csv;
using System.IO;
using System.Linq;
using UnityEngine;

public class DialogueInfo
{
    public List<string> MiniMenu { get; set; }
    public List<string> Running { get; set; }
    public List<string> Good { get; set; }
    public List<string> Bad { get; set; }

    public int menu = 0;
    public int funfact = 0;
    public int goodResponse = 0;
    public int badResponse = 0; 

    public DialogueInfo()
    {
        MiniMenu = new List<string>();
        Running = new List<string>();
        Good = new List<string>();
        Bad = new List<string>();
    }

    public string getMenu()
    {
        menu++;
        if (MiniMenu.Count > 0)
        {
            if (menu >= MiniMenu.Count)
            {
                menu = 0;
            }
        }
        return MiniMenu[menu];
    }

    public string getFunFact()
    {
        funfact++;
        if (Running.Count > 0)
        {
            if (funfact >= Running.Count)
            {
                funfact = 0;
            }
        }
        return Running[funfact];
    }

    public string getGoodFeedback()
    {
        goodResponse++;
        if (Good.Count > 0)
        {
            if(goodResponse >= Good.Count)
            {
                goodResponse = 0;
            }
        }
        return Good[goodResponse];
    }

    public string getBadFeedback()
    {
        badResponse++;
        if (Bad.Count > 0)
        {
            if(badResponse >= Bad.Count)
            {
                badResponse = 0;
            }
        }
        return Bad[badResponse];
    }

    // https://stackoverflow.com/questions/49570175/simple-way-to-randomly-shuffle-list 
    public static void ShuffleMe<T>(IList<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;

        for (int i = list.Count - 1; i > 1; i--)
        {
            int rnd = random.Next(i + 1);

            T value = list[rnd];
            list[rnd] = list[i];
            list[i] = value;
        }
    }
}

public class DialogueParser
{
    private static DialogueInfo data = null;
    private static string _file = "";

    public static DialogueInfo GetDialogueData()
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

    // read csv
    public static DialogueInfo readCSV(string file)
    {
        DialogueInfo data = new DialogueInfo();

        var header = new List<string>();

        using (var streamRdr = new StreamReader(System.IO.Directory.GetCurrentDirectory() + @"/Assets/Data/" + file + ".csv"))
        {
            var csvReader = new CsvReader(streamRdr, ",");

            // skip 1st line 
            if (csvReader.Read())
            {

            }

            while (csvReader.Read())
            {
                if(csvReader[0] == "mini menu")
                {
                    data.MiniMenu.Add(csvReader[1]);
                } else if (csvReader[0] == "running dialogue")
                {
                    data.Running.Add(csvReader[1]);
                } else if (csvReader[0] == "bad feedback")
                {
                    data.Bad.Add(csvReader[1]);
                } else if (csvReader[0] == "good feedback")
                {
                    data.Good.Add(csvReader[1]);
                }
            }
        }

        // shuffling right answers up front
        IdiomData.ShuffleMe(data.MiniMenu);
        IdiomData.ShuffleMe(data.Running);
        IdiomData.ShuffleMe(data.Good);
        IdiomData.ShuffleMe(data.Bad);

        return data;
    }
}