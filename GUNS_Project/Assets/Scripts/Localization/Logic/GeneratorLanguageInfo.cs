using System.Collections;
using System.Collections.Generic;

namespace Localization
{

    public class GeneratorLanguageInfo
    {
        private static readonly LanguageInfo[] _infos = new[]
        {
            new LanguageInfo("English", LanguageType.En),
            new LanguageInfo("Russian", LanguageType.Ru),
            new LanguageInfo("Turkish", LanguageType.Tr),
            new LanguageInfo("Arabic", LanguageType.Ar),
            new LanguageInfo("Azerbaijani", LanguageType.Az),
            new LanguageInfo("Belarusian", LanguageType.Be),
            new LanguageInfo("German", LanguageType.De),
            new LanguageInfo("Spanish", LanguageType.Es),
            new LanguageInfo("Estonian", LanguageType.Et),
            new LanguageInfo("French", LanguageType.Fr),
            new LanguageInfo("Hebrew", LanguageType.He),
            new LanguageInfo("Hindi", LanguageType.Hi),
            new LanguageInfo("Armenian", LanguageType.Hy),
            new LanguageInfo("Indonesian", LanguageType.Id),
            new LanguageInfo("Italian", LanguageType.It),
            new LanguageInfo("Japanese", LanguageType.Ja),
            new LanguageInfo("Georgian", LanguageType.Ka),
            new LanguageInfo("Kazakh", LanguageType.Kk),
            new LanguageInfo("Kyrgyz", LanguageType.Ky),
            new LanguageInfo("Lithuanian", LanguageType.Lt),
            new LanguageInfo("Latvian", LanguageType.Lv),
            new LanguageInfo("Portuguese", LanguageType.Pt),
            new LanguageInfo("Romanian", LanguageType.Ro),
            new LanguageInfo("Tajik", LanguageType.Tg),
            new LanguageInfo("Turkmen", LanguageType.Tk),
            new LanguageInfo("Ukrainian", LanguageType.Uk),
            new LanguageInfo("Uzbek", LanguageType.Uz),
        };

        public static IEnumerable<LanguageInfo> Generate() =>
            _infos;
    }

    public class LanguageInfo
    {
        private readonly string _name;
        private readonly LanguageType _type;

        public LanguageInfo(string name, LanguageType type)
        {
            _name = name;
            _type = type;
        }

        public string Name => _name;

        public LanguageType Type => _type;
    }

    public enum LanguageType
    {
        En,
        Ru,
        Tr,
        Es,
        Id,
        De,
        Fr,
        It,
        Pt,
        Be,
        Uk,
        Az,
        Ja,
        Ar,
        Hy,
        Ka,
        He,
        Et,
        Kk,
        Ky,
        Lt,
        Lv,
        Ro,
        Tg,
        Tk,
        Uz,
        Hi
    }
}