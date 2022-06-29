using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMove : EnemyMove
{

    protected override void SetStatus()
    {
        healthpoint = Mathf.Round(waveManager.nowWave * waveManager.nowWave / 3 + 10);
        attackpoint = Mathf.Round(2 + waveManager.nowWave * 0.4f);
        speed = 4;
        airdelayTime = 0.5f;
    }
}
