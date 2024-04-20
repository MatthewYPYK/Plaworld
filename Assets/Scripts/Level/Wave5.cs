using System.Collections;
using UnityEngine;

public class Wave5 : WaveBase
{

    // Coroutine to define and execute a wave sequence
    public override IEnumerator SpawnSequence()
    {
        SpawnEnemy("AirShip");
        yield return new WaitForSeconds(1f);
        SpawnEnemy("Jeep");
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 5; i++){
            SpawnEnemy("Soldier");
            yield return new WaitForSeconds(1f);
        }
    }
}
