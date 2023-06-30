using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NReco.Csv;
using System.IO;
using System.Linq;

public class IdiomBase
{
    public string Idiom { get; private set; }
    public IdiomBase(string idiom)
    {
        Idiom = idiom;
    }

}
public class CorrectIdiom : IdiomBase
{
    public string Hint { get; private set; }

    public CorrectIdiom(string _idiom, string _hint) : base(_idiom)
    {
        Hint = _hint;
    }
}

public class WrongIdiom : IdiomBase
{
    public WrongIdiom(string _idiom) : base(_idiom)
    {

    }
}

public class IdiomData
{
    public List<CorrectIdiom> Correct { get; set; }
    public List<WrongIdiom> Wrong { get; set; }

    public IdiomData()
    {

    }

    public IdiomData(List<CorrectIdiom> _correct, List<WrongIdiom> _wrong)
    {
        Correct = _correct;
        Wrong = _wrong; 
    }

    public List<IdiomBase> GetWrong()
    {
        List<IdiomBase> choices = new List<IdiomBase>();

        ShuffleMe(Wrong);

        choices.Add(Wrong[0]);
        choices.Add(Wrong[1]);
        choices.Add(Correct[0]);

        ShuffleMe(choices);

        return choices;
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

    private static IdiomData data = null;
    private static string _file = "";

    public static IdiomData GetWizardData()
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
                if (csvReader[0] == "right")
                {
                    idiom.Correct.Add(new CorrectIdiom(csvReader[1], csvReader[2]));
                } else
                {
                    idiom.Wrong.Add(new WrongIdiom(csvReader[1]));
                }
            }
        }

        return idiom;
    }
}
