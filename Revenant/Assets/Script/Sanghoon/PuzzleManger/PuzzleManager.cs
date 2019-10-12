using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public StarPlate[] starPuzzle1;
    public StarPlate[] starPuzzle2;

    public ParticleSystem[] particle;

    void Update()
    {
    }

    public void StarPuzzleClearCheck()
    {
        foreach(StarPlate starPuzzle in starPuzzle1)
        {
            if (starPuzzle.isLock == true &&
                starPuzzle.myPoint == 0)
            {

            }
        }
    }
}
