using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Localization
{

    public class LocalizedTextCsvParser : CsvParser<LocalizedText>
    {
        private bool _isFound;
        private int _startIndex = int.MaxValue;

        protected override bool SkipFirstLine => true;

        protected override bool ValidateLine(string[] cellsInLine, out LocalizedText validated)
        {
            if (_isFound == false)
                GenerateStartIndex(cellsInLine.ToList());

            string keyPhrase = cellsInLine[0];

            if (keyPhrase == "")
            {
                validated = null;
                return false;
            }

            List<ILocalizedData> list = new List<ILocalizedData>();

            int length = Enum.GetNames(typeof(LanguageType)).Length;


            for (int i = _startIndex; i < cellsInLine.Length; i++)
            {
                if (length >= i)
                {
                    if (int.TryParse(cellsInLine[1], out int number) && number == 2)
                    {
                        var line = cellsInLine[i];

                        List<string> words = line.Split(' ').ToList();

                        int difference = int.MaxValue;

                        int spaceIndex = 0;

                        string correctWord = "";

                        foreach (var element in words)
                        {
                            int index = words.IndexOf(element);

                            List<string> firstArrayWords = words.Take(index).ToList();

                            List<string> secondArrayWords = words.Except(firstArrayWords).ToList();

                            int firstLength = 0;

                            int secondLength = 0;

                            firstArrayWords.ForEach(word => firstLength += word.Length);
                            secondArrayWords.ForEach(word => secondLength += word.Length);

                            int newDifference = (int) GetDifference(firstLength, secondLength);

                            if (difference > newDifference)
                            {
                                correctWord = firstArrayWords.LastOrDefault() + " ";

                                if (line.Contains(correctWord))
                                    spaceIndex = line.IndexOf(correctWord) + correctWord.Length - 1;

                                difference = newDifference;
                            }
                        }

                        cellsInLine[i] = cellsInLine[i].Remove(spaceIndex, 1).Insert(spaceIndex, "<br>");
                    }

                    list.Add(new TextData((LanguageType) i - _startIndex, cellsInLine[i]));
                }
            }

            validated = new LocalizedText(keyPhrase.Trim(), list);

            return true;
        }

        private decimal GetDifference(decimal nr1, decimal nr2)
        {
            return Math.Abs(nr1 - nr2);
        }

        private void GenerateStartIndex(List<string> cellsInLine)
        {
            foreach (var type in Enum.GetNames(typeof(LanguageType)))
            {
                foreach (var line in cellsInLine)
                {
                    if (line == type.ToLower())
                    {
                        int index = cellsInLine.IndexOf(line);

                        if (index < _startIndex)
                            _startIndex = index;

                        _isFound = true;
                        break;
                    }
                }
            }
        }
    }

    public class LocalizedText : ILocalizedObject<ILocalizedData>
    {
        public LocalizedText(string key, IReadOnlyList<ILocalizedData> objectByLanguage)
        {
            Key = key;
            ObjectByLanguage = objectByLanguage;
        }

        public string Key { get; private set; }

        public IReadOnlyList<ILocalizedData> ObjectByLanguage { get; private set; }
    }

    public interface ILocalizedObject<out T> where T : ILocalizedData
    {
        public string Key { get; }

        public IReadOnlyList<T> ObjectByLanguage { get; }
    }

}