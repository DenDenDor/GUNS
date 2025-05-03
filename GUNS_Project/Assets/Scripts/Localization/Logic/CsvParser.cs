using System;
using System.Collections.Generic;
using UnityEngine;

namespace Localization
{

    public abstract class CsvParser<T>
    {
        private const char LineSeparator = '\n';

        private const char CellsInLineSeparator = '~';

        private readonly IParserLine[] _parserLines =
        {
            new RewriteLineParser(CellsInLineSeparator),
        };


        protected abstract bool SkipFirstLine { get; }

        public IEnumerable<T> Parse(TextAsset csv)
        {
            if (csv == null)
                throw new ArgumentNullException(nameof(csv));

            var result = new List<T>();

            string[] lines = csv.text.Split(LineSeparator);

            for (int i = SkipFirstLine ? 1 : 0; i < lines.Length; i++)
            {
                string line = lines[i];

                string correctLine = line;

                foreach (var parserLine in _parserLines)
                {
                    correctLine = parserLine.GiveParsedLine(correctLine);
                }

                string[] cellInLines = correctLine.Split(CellsInLineSeparator);

                if (ValidateLine(cellInLines, out var validated))
                    result.Add(validated);
            }

            return result;
        }

        protected abstract bool ValidateLine(string[] cellsInLine, out T validated);
    }

    public interface IParserLine
    {
        public string GiveParsedLine(string line);
    }

    public class RewriteLineParser : IParserLine
    {
        private const char IntermediateSeparator = ',';

        private const char StartSentenceSeparator = '"';

        private readonly char _cellsInLineSeparator;

        public RewriteLineParser(char cellsInLineSeparator)
        {
            _cellsInLineSeparator = cellsInLineSeparator;
        }

        public string GiveParsedLine(string line)
        {
            string correctLine = line;

            int index = 0;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == IntermediateSeparator)
                {
                    if (index % 2 == 0)
                        correctLine = correctLine.Remove(i, 1).Insert(i, _cellsInLineSeparator.ToString());
                }

                if (line[i] == StartSentenceSeparator)
                    index++;
            }

            return correctLine;
        }
    }
}