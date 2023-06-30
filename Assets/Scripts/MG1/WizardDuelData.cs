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
    public string Ask { get; set; }

    public Question(List<Choice> choices, string ask)
    {
        Choices = choices;
        Ask = ask;
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
        List<int> rndNum = new List<int>();
        
        while (temp.Count < 2)
        {
            rnd = Random.Range(0, Choices.Capacity);

            if (Choices[rnd].Tense == tense || rndNum.Contains(rnd))
            {
                rndNum.Add(rnd);
                continue;
            } else
            {
                temp.Add(new ResponseChoice(new Choice(Choices[rnd].Tense,
                        Choices[rnd].Response), false));
                rndNum.Add(rnd);
            }
        }

        /*
        for (int i = 0; i < 2; i++)
        {
            rnd = Random.Range(0, Choices.Capacity);

            if (Choices[rnd].Tense == tense)
            {
                rndNum.Add(rnd);
                continue;
            } else if (rndNum.Contains(rnd))
            {
                rndNum.Add(rnd);
                continue;
            }
            else
            {
                temp.Add(new ResponseChoice(new Choice(Choices[rnd].Tense,
                        Choices[rnd].Response), false));
            }

            rndNum.Add(rnd);
        }
        */

        //randomize choices
        temp.Add(correct);
        ShuffleMe(temp); // test this 

        return temp;

        //throw new System.Exception("Not implemented");
    }
}

public class Category
{
    public string Name { get; private set; }
    public List<Question> Questions { get; private set; }

    public Category(string name)
    {
        Name = name;
        Questions = new List<Question>();
    }
    
    public void addQuestion(Question question)
    {
        Questions.Add(question);
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

public static class WizardDuelData
{

    private static WizardData data = null;
    private static string _file = "";

    public static WizardData GetWizardData()
    {
        if(data == null)
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
    private static WizardData readCSV(string file)
    {
        Dictionary<string, Category> categories = new Dictionary<string, Category>();

        var header = new List<string>();

        using (var streamRdr = new StreamReader(System.IO.Directory.GetCurrentDirectory() + @"/Assets/Data/" + file + ".csv"))
        {
            var csvReader = new CsvReader(streamRdr, ",");

            
            // want to split first line 
            if(csvReader.Read())
            {
                // ignoring category label 
                header = Enumerable.Range(0, csvReader.FieldsCount).Skip(1).Select(index => csvReader[index]).ToList();
            }

            // add categories to line.modes 

            while (csvReader.Read())
            {

                Category category;

                if (!categories.TryGetValue(csvReader[0], out category))
                {
                    category = new Category(csvReader[0]);
                    categories.Add(csvReader[0], category);
                }

                string question = csvReader[1];
                var responses = Enumerable.Range(0, csvReader.FieldsCount).Skip(2).Select(index => new Choice(header[index-1], csvReader[index])).ToList();

                Question a = new Question(responses, question);

                category.addQuestion(a);

            }
        }

        WizardData data = new WizardData(categories.Select(kv => kv.Value).ToList());
        return data;

     
    }
}