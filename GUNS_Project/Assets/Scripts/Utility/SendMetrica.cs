using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using YG;
using static EventMetricaKeys;

public static class SendMetrica
{
    private static string GetMetricaKey(EventMetricaKeys metricaKeys)
    {
        return metricaKeys switch
        {
            StartGame => "Start Game",
            PickUpCoins => "Pick up start loot",
            BuildSoldierBarrack => "Build a soldier barrack",
            ToFightFirst => "Start first battle",
            DuringFighting => "In battle",
            PickUpLootAfterFirst => "Pick up loot after battle",
            BuildTank => "Build a tank",
            ToFightAgain => "Start second battle",
            _=> ""
        };
    }
    
    private static string GetMetricaKey(DictionaryMetricaKeys dictionaryMetricaKeys)
    {
        return dictionaryMetricaKeys switch
        {
            DictionaryMetricaKeys.FloodDeaths => "FloodDeaths",
            DictionaryMetricaKeys.VolcanicEruptionDeaths => "VolcanicEruptionDeaths",
            DictionaryMetricaKeys.SnowAvalancheDeaths => "SnowAvalancheDeaths",
            DictionaryMetricaKeys.AcidRainDeaths => "AcidRainDeaths",
            DictionaryMetricaKeys.FireDeaths => "FireDeaths",
            DictionaryMetricaKeys.EarthquakeDeaths => "EarthquakeDeaths",
            DictionaryMetricaKeys.DesertDeaths => "DesertDeaths",
            DictionaryMetricaKeys.MeteorRainDeaths => "MeteorRainDeaths",
            DictionaryMetricaKeys.Tornado => "TornadoDeaths",
            DictionaryMetricaKeys.ThunderstormDeaths => "ThunderstormDeaths",
            DictionaryMetricaKeys.PlayerDeaths => "PlayerDeaths",
            DictionaryMetricaKeys.Level => "Levels",
            _ => ""
        };
    }
    
    public static void Send(EventMetricaKeys metricaKeys)
    {
        Debug.Log("F I N I S H E D " + metricaKeys);
       string metrica = GetMetricaKey(metricaKeys);
       
       SDKMediator.Instance.SDKAdapter.SendMetrica(metrica);
    }   
    
    public static void Send(DictionaryMetricaKeys dictionaryMetricaKeys, int value)
    {
        Debug.Log(" L O L " + dictionaryMetricaKeys + " V = A = L = U = E: " + value);
        var key = GetMetricaKey(dictionaryMetricaKeys);
        
        IDictionary<string, string> eventParams = new Dictionary<string, string>()
        {
            {key, value.ToString()}
        };
        
        SDKMediator.Instance.SDKAdapter.SendMetrica(key, eventParams);
    }
}

public enum DictionaryMetricaKeys
{
    FloodDeaths,
    VolcanicEruptionDeaths,
    SnowAvalancheDeaths,
    AcidRainDeaths,
    FireDeaths,
    EarthquakeDeaths,
    DesertDeaths,
    MeteorRainDeaths,
    Tornado,
    ThunderstormDeaths,
    
    PlayerDeaths,
    Level
}

public enum EventMetricaKeys
{
    StartGame,
    PickUpCoins,
    BuildSoldierBarrack,
    ToFightFirst,
    DuringFighting,
    PickUpLootAfterFirst,
    BuildTank,
    ToFightAgain,
}