using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public StarPlate[] starPuzzle1;
    public GameObject starPuzzle1Pos;
    public StarPlate[] starPuzzle2;
    public GameObject starPuzzle2Pos;

    public ParticleSystem[] starPuzzleParticle;

    void Update()
    {
    }

    public void StarPuzzleClearCheck()
    {
        foreach(StarPlate starPuzzle in starPuzzle1)
        {
            if (starPuzzle.myPoint == 0)
            {
                starPuzzleParticle[0].transform.position = starPuzzle1Pos.transform.position;
                foreach (ParticleSystem effect in starPuzzleParticle)
                {
                    effect.Play();
                }
            }
        }
    }
}
