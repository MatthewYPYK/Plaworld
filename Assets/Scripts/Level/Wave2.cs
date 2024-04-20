using System.Collections;
using UnityEngine;

public class Wave2 : WaveBase
{

    // Coroutine to define and execute a wave sequence
    public override IEnumerator SpawnSequence()
    {
        for (int i = 0; i < 2; i++){
            SpawnEnemy("Soldier");
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(2f);
        SpawnEnemy("Jeep");
    }
}
