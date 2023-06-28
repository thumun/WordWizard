using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NReco.Csv;
using System.IO;
using System.Linq;

public class CorrectIdiom
{
    public string Idiom { get; private set; }
    public string Hint { get; private set; }

    public CorrectIdiom(string _idiom, string _hint)
    {
        Idiom = _idiom;
        Hint = _hint;
    }
}

public class WrongIdiom
{
    public string Idiom { get; private set; }

    public WrongIdiom(string _idiom)
    {
        Idiom = _idiom;
    }
}

public class Idiom
{
    public List<CorrectIdiom> Correct { get; set; }
    public List<WrongIdiom> Wrong { get; set; }

    public Idiom()
    {

    }

    public Idiom(List<CorrectIdiom> _correct, List<WrongIdiom> _wrong)
    {
        Correct = _correct;
        Wrong = _wrong; 
    }

    public List<WrongIdiom> GetWrong()
    {
        List<WrongIdiom> wrongLst = new List<WrongIdiom>();

        ShuffleMe(Wrong);
        wrongLst.Add(Wrong[0]);
        wrongLst.Add(Wrong[1]);

        return wrongLst;
    }

    // https://stackoverflow.com/questions/49570175/simple-way-to-randomly-shuffle-list 
    private static void ShuffleMe<T>(IList<T> list)
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

    private static Idiom data = null;
    private static string _file = "";

    public static Idiom GetWizardData()
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
    private static Idiom readCSV(string file)
    {
        Idiom idiom = new Idiom();

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
                if (csvReader[0] == "right")
                {
                    idiom.Correct.Add(new CorrectIdiom(csvReader[1], csvReader[2]));
                } else
                {
                    idiom.Correct.Add(new CorrectIdiom(csvReader[1], csvReader[2]));
                }
            }
        }

        return idiom;


    }
}
