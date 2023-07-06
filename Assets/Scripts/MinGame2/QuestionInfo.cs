using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace MinigameTwo
{
    [Serializable]

    public struct QuestionFormats
    {
        // Link keyword
        public string Keyword;

        // Index of the first character of the word
        // Does not count link tags
        public int index;

        // Index counting tags. May not be used
        public int indexWithTags;

        // Whether or not there is a mistake
        public bool mistake;

        // The word (space deliminated string) in question
        public string word;

        // Correct string that replaces original
        public string correctAnswer;
    }
}