using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableStringFloatPair
{
    public string key;
    public float value;
    public SerializableStringFloatPair(string key, float value)
    {
        this.key = key;
        this.value = value;
    }
}
public class CustomizableWave : WaveBase
{
    public float timeBetweenSpawn = .5f;
    public SerializableStringFloatPair[] waveData;
    public override IEnumerator SpawnSequence()
    {
        foreach (var pair in waveData){
            switch (pair.key)
            {
                case "wait":
                    yield return new WaitForSeconds(pair.value);
                    break;
                default:
                    for (int i = 0; i < pair.value; i++){
                        SpawnEnemy(pair.key);
                        yield return new WaitForSeconds(timeBetweenSpawn);
                    }
                    break;
            }
        }
    }
}
