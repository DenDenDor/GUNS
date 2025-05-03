using System;

[Serializable]
public class SaveData
{
    [AutoGenerateSaveMethod] public float MusicValue = 0.5f;
    [AutoGenerateSaveMethod] public float SoundValue = 0.5f;
    [AutoGenerateSaveMethod] public int Coins = 0;
    [AutoGenerateSaveMethod] public int Levels = 0;
    [AutoGenerateSaveMethod] public bool IsMusicTurnOn = true;
    [AutoGenerateSaveMethod] public bool IsSoundTurnOn = true;
    [AutoGenerateSaveMethod] public int EducationStep;
    [AutoGenerateSaveMethod] public bool IsEducationFinished;
    [AutoGenerateSaveMethod] public int GoldCount;
    [AutoGenerateSaveMethod] public int SilverCount;
    [AutoGenerateSaveMethod] public bool IsLanguageSelected;
    [AutoGenerateSaveMethod] public string Language;
}