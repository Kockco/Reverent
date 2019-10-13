using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    #region
    int starPuzzleClear;
    public StarPlate[] starPuzzle1;
    public StarPlate[] starPuzzle2;
    public ParticleSystem[] starPuzzleParticle;
    public ParticleSystem[] starPuzzleParticle2;
    public ParticleSystem[] starPuzzleClearEffect;
    public ParticleSystem[] starPuzzleClearEffect2;
    public ParticleSystem[] starPuzzleAllClearEffect;
    #endregion

    public StarPlate[] potatoPuzzle1;
    public StarPlate[] potatoPuzzle2;
    public StarPlate[] planetPuzzle1;
    
    public ParticleSystem[] potatoPuzzleParticle;
    public ParticleSystem[] potatoPuzzleParticle2;
    public ParticleSystem[] planetPuzzleParticle;
    
    public ParticleSystem[] starPuzzleClearEffect3;
    public ParticleSystem[] starPuzzleClearEffect4;
    public ParticleSystem[] starPuzzleClearEffect5;

    void Update()
    {
    }

    public void StarPuzzleClearCheck(int PuzzleNumber)
    {
        int clearPoint = 0;
        foreach (ParticleSystem effect in starPuzzleParticle)
        {
            effect.Play();
        }
        if (PuzzleNumber == 1)
        {
            for (int i = 0; i < starPuzzle1.Length; i++)
            {
                if (starPuzzle1[i].myPoint == 0)
                {
                    clearPoint++;
                }
            }
            if (clearPoint == starPuzzle1.Length)
            {
                starPuzzleClear++;
                foreach (ParticleSystem effect in starPuzzleClearEffect)
                {
                    effect.Play();
                }
            }
        }
        else if(PuzzleNumber == 2)
        {
            for (int i = 0; i < starPuzzle2.Length; i++)
            {
                if (starPuzzle2[i].myPoint == 0)
                {
                    clearPoint++;
                }
            }
            if (clearPoint == starPuzzle2.Length)
            {
                starPuzzleClear++;
                foreach (ParticleSystem effect in starPuzzleClearEffect2)
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
