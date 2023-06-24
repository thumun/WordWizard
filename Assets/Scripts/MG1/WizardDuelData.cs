using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NReco.Csv;
using System.IO;
using System.Linq;

public class ResponseChoice
{
    public Choice Choice { get; set; }
    public bool IsCorrect { get; set; }

    // empty constructor 
    public ResponseChoice()
    {

    }

    public ResponseChoice(Choice choice, bool correct)
    {
        Choice = choice;
        IsCorrect = correct;
    }
}


public class Choice
{
    public string Tense { get; private set; }
    public string Response { get; private set; }

    
    public Choice(string tense, string response)
    {
        Tense = tense;
        Response = response;
    }
}

public class Question
{
    public List<Choice> Choices { get; private set; }

    public Question(List<Choice> choices)
    {
        Choices = choices;
    }

    private static void ShuffleMe<T>(this IList<T> list)
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

    public List<ResponseChoice> PresentChoices(string tense)
    {

        //find correct answer
        var correct = new ResponseChoice()
        {
            Choice = Choices.Where(c => c.Tense == tense).First(),
            IsCorrect = true
        };


        //get two wrong answers

        int rnd = -1;
        List<ResponseChoice> temp = new List<ResponseChoice>();

        while (rnd != -1 && temp.Capacity != 2)
        {
            rnd = Random.Range(0, Choices.Capacity);

            if (Choices[rnd].Tense != tense)
            {
                continue;
            } else
            {
                temp.Add(new ResponseChoice(new Choice(Choices[rnd].Tense,
                        Choices[rnd].Response), false));
            }
        }

        //randomize choices
        temp.Add(correct);
        ShuffleMe<ResponseChoice>(temp); // test this 

        //which option is correct
        //give the list back

        return temp;

        //throw new System.Exception("Not implemented");
    }

}

public class Category
{
    
    public List<Question> Questions { get; private set; }

    public Category(List<Question> questions)
    {
        Questions = questions;
    }

}

public class WizardData
{
    public List<Category> Categories { get; private set; }

    public WizardData(List<Category> categories)
    {
        Categories = categories;
    }

}


public class WizardDuelData
{
    // read csv
    public static void readCSV(string file)
    {

        using (var streamRdr = new StreamReader(System.IO.Directory.GetCurrentDirectory() + @"/Assets/Data/" + file + ".csv"))
        {
            var csvReader = new CsvReader(streamRdr, ",");

            var header = new List<string>();

            // want to split first line 
            if(csvReader.Read())
            {
                header = Enumerable.Range(0, csvReader.FieldsCount).Select(index => csvReader[index]).ToList();
            }

            // add categories to line.modes 

            while (csvReader.Read())
            {
                var questions = Enumerable.Range(0, csvReader.FieldsCount).Select(index => csvReader[index]).ToList();






            }
        }



        //WizardData data = new WizardData();

     
    }
}