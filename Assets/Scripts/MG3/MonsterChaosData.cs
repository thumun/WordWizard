using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NReco.Csv;
using System.IO;
using System.Linq;

public class IdiomBase
{
    public string Idiom { get; private set; }
    public string Definition { get; private set; }
    public string Example { get; private set; }
    public bool IsCorrect { get; set; }

    public IdiomBase(string idiom, string definition, string example)
    {
        Idiom = idiom;
        Definition = definition;
        Example = example;
        IsCorrect = false;
    }

    public IdiomBase Clone()
    {
        return new IdiomBase(this.Idiom, this.Definition, this.Example);
    }
}

public class IdiomData
{
    public List<IdiomBase> Idioms { get; set; }

    public IdiomData()
    {

    }

    public IdiomData(List<IdiomBase> _idioms)
    {
        Idioms = _idioms; 
    }

    public List<IdiomBase> GetChoices(int roundNum)
    {
        List<IdiomBase> choices = new List<IdiomBase>();

        choices.Add(Idioms[roundNum].Clone());
        choices[0].IsCorrect = true;

        int rnd = -1;
        List<int> rndNum = new List<int>();

        string tempIdiom = Idioms[roundNum].Idiom;
        rndNum.Add(roundNum);

        while (choices.Count < 3)
        {
            rnd = Random.Range(0, Idioms.Count);
            

            if (rndNum.Contains(rnd))
            {
                rndNum.Add(rnd);
                continue;
            }
            else
            {
                Debug.Log(rnd);
                choices.Add(Idioms[rnd].Clone());
                rndNum.Add(rnd);
            }
        }

        ShuffleMe(choices);

        return choices;
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

public class MonsterChaosData
{

    private static IdiomData data = null;
    private static string _file = "";

    public static IdiomData GetIdiomData()
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
    public static IdiomData readCSV(string file)
    {
        IdiomData idiom = new IdiomData();

        List<IdiomBase> data = new List<IdiomBase>();

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
                data.Add(new IdiomBase(csvReader[0], csvReader[1], csvReader[2]));

            }
        }

        idiom.Idioms = data;

        // shuffling right answers up front
        IdiomData.ShuffleMe(idiom.Idioms);

        return idiom;
    }
}
