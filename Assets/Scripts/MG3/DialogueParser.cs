using System.Collections;
using System.Collections.Generic;
using NReco.Csv;
using System.IO;
using System.Linq;
using UnityEngine;

public class DialogueParser
{

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
