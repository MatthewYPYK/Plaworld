
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    public SerializableDictionary<int, WaveBase> WaveDict = new SerializableDictionary<int, WaveBase>();
    void Start(){
        WaveDict.RemoveNullValues();
        //  edit it from WavesManager inside scene instead
        // WaveDict[1] = new Wave1();
        // WaveDict[2] = new Wave2();
        // WaveDict[5] = new Wave5();
        // WaveDict[10] = new Wave10();
    }
    public bool IsWaveDefined(int waveNumber) => WaveDict.ContainsKey(waveNumber);
    public void StartWave(int waveNumber)
    {
        if (IsWaveDefined(waveNumber))
            StartCoroutine(WaveDict[waveNumber].SpawnSequence());
        else
            Debug.LogError("Wave " + waveNumber + " is not defined.");
    }
}