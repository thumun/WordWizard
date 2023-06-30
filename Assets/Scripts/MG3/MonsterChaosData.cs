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

}
/*
public class CorrectIdiom : IdiomBase
{
    public string Hint { get; private set; }

    public CorrectIdiom(string _idiom, string _hint, bool _isCorrect) : base(_idiom, _isCorrect)
    {
        Hint = _hint;
    }
}

public class WrongIdiom : IdiomBase
{
    public WrongIdiom(string _idiom, bool _isCorrect) : base(_idiom, _isCorrect)
    {

    }
}
*/

public class IdiomData
{
    public List<IdiomBase> Idioms { get; set; }
    //public List<CorrectIdiom> Correct { get; set; }
    //public List<WrongIdiom> Wrong { get; set; }

    public IdiomData()
    {

    }

    public IdiomData(List<IdiomBase> _idioms)
    {
        Idioms = _idioms; 
    }

    /*
    public IdiomData(List<CorrectIdiom> _correct, List<WrongIdiom> _wrong)
    {
        Correct = _correct;
        Wrong = _wrong; 
    }
    */

    /*
    public List<IdiomBase> GetChoices(int roundNum)
    {
        List<IdiomBase> choices = new List<IdiomBase>();

        ShuffleMe(Wrong);

        choices.Add(Wrong[0]);
        choices.Add(Wrong[1]);
        choices.Add(Correct[roundNum]);

        ShuffleMe(choices);

        return choices;
    }
    */

    public List<IdiomBase> GetChoices(int roundNum)
    {
        List<IdiomBase> choices = new List<IdiomBase>();

        choices.Add(Idioms[roundNum]);
        choices[0].IsCorrect = true;

        int rnd = -1;
        List<int> rndNum = new List<int>();

        string tempIdiom = Idioms[roundNum].Idiom;
        rndNum.Add(roundNum);

        while (choices.Count < 3)
        {
            rnd = Random.Range(0, Idioms.Capacity);

            if (choices[rnd].Idiom == tempIdiom || rndNum.Contains(rnd))
            {
                rndNum.Add(rnd);
                continue;
            }
            else
            {
                choices.Add(Idioms[rnd]);
                rndNum.Add(rnd);
            }
        }

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
    private static IdiomData readCSV(string file)
    {
        IdiomData idiom = new IdiomData();

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

                idiom.Idioms.Add(new IdiomBase(csvReader[0], csvReader[1], csvReader[2]));

                /*
                if (csvReader[0] == "right")
                {
                    idiom.Correct.Add(new CorrectIdiom(csvReader[1], csvReader[2], true));
                } else
                {
                    idiom.Wrong.Add(new WrongIdiom(csvReader[1], false));
                }
                */
            }
        }

        // shuffling right answers up front
        IdiomData.ShuffleMe(idiom.Idioms);

        return idiom;
    }
}
