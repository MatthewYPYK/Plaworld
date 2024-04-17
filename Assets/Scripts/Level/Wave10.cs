using System.Collections;
using UnityEngine;

public class Wave10 : WaveBase
{

    // Coroutine to define and execute a wave sequence
    public override IEnumerator SpawnSequence()
    {
        SpawnEnemy("Tank");
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 5; i++){
            SpawnEnemy("Soldier");
            yield return new WaitForSeconds(.1f);
        }
    }
}
