using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public StarPlate[] starPuzzle1;
    public StarPlate[] starPuzzle2;
    public StarPlate[] potatoPuzzle1;
    public StarPlate[] potatoPuzzle2;
    public StarPlate[] planetPuzzle1;

    public ParticleSystem[] starPuzzleParticle;
    public ParticleSystem[] starPuzzleParticle2;
    public ParticleSystem[] potatoPuzzleParticle;
    public ParticleSystem[] potatoPuzzleParticle2;
    public ParticleSystem[] planetPuzzleParticle;

    public ParticleSystem[] starPuzzleClearEffect;
    public ParticleSystem[] starPuzzleClearEffect2;
    public ParticleSystem[] starPuzzleClearEffect3;
    public ParticleSystem[] starPuzzleClearEffect4;
    public ParticleSystem[] starPuzzleClearEffect5;

    void Update()
    {
    }

    public void StarPuzzleClearCheck()
    {
        foreach(StarPlate starPuzzle in starPuzzle1)
        {
            if (starPuzzle.myPoint == 0)
            {
                foreach (ParticleSystem effect in starPuzzleParticle)
                {
                    effect.Play();
                }
            }
        }
    }
    public bool PuzzleClearCheck(int puzzleNumber)
    {
        int clearPoint = 0;
        switch (puzzleNumber)
        {
            case 1:
                foreach (StarPlate starPuzzle in starPuzzle1)
                {
                    if (starPuzzle.myPoint == 0)
                    {
                        clearPoint++;
                    }
                    if (starPuzzle1.Length-1 == clearPoint)
                    {
                        foreach(ParticleSystem effect in starPuzzleClearEffect)
                        {
                            effect.Play();
                        }
                        return true;
                    }
                }

                break;
        }

        return false;
    }
}
