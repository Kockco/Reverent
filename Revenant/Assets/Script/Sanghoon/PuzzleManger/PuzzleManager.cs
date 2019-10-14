using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    //1번퍼즐
    #region
    public bool starPuzzle1Clear;
    public bool starPuzzle2Clear;
    public StarPlate[] starPuzzle1;
    public StarPlate[] starPuzzle2;
    public ParticleSystem[] starPuzzleParticle;
    public ParticleSystem[] starPuzzleClearEffect;
    public ParticleSystem[] starPuzzleClearEffect2;
    public ParticleSystem[] starPuzzleAllClearEffect;
    #endregion

    //2번퍼즐
    #region
    bool potatoPuzzle1Clear;
    bool potatoPuzzle2Clear;
    public PotatoPlate[] potatoPuzzle1;
    public Potato[] potato1;
    public PotatoPlate[] potatoPuzzle2;
    public Potato[] potato2;

    public ParticleSystem[] potatoPuzzleParticle;
    public ParticleSystem[] potatoPuzzleParticle2;
    public ParticleSystem[] potatoPuzzleClearEffect;
    public ParticleSystem[] potatoPuzzleClearEffect2;
    public ParticleSystem[] potatoPuzzleAllClearEffect;

    #endregion

    //3번퍼즐
    #region
    public PlanetLine[] planetPuzzle;
    public ParticleSystem[] planetPuzzleParticle;
    public ParticleSystem[] planetPuzzleLineParticle;
    public ParticleSystem[] planetPuzzleAllClearEffect;
    #endregion

    CameraScript cam;

    void Start()
    {
        foreach (ParticleSystem effect in planetPuzzleParticle)
        {
            effect.Stop();
        }

        cam = Camera.main.transform.gameObject.GetComponent<CameraScript>();
    }

    //스타 퍼즐 클리어 체크 (1탄퍼즐) 매개변수는 1-1 인지 1-2인지 체크
    public void StarPuzzleClearCheck(int PuzzleNumber)
    {
        int clearPoint = 0;
        //1개맞으면 반짝하는거
        foreach (ParticleSystem effect in starPuzzleParticle)
        {
            effect.Play();
        }
        //1-1퍼즐
        if (PuzzleNumber == 0)
        {
            //퍼즐 두개다 맞았는지 체크
            for (int i = 0; i < starPuzzle1.Length; i++)
            {
                if (starPuzzle1[i].myPoint == 0)
                {
                    clearPoint++;
                }
            }
            //두개다 맞으면 클리어 이펙트 플레이
            if (clearPoint == starPuzzle1.Length)
            {
                starPuzzle1Clear = true;
                foreach (ParticleSystem effect in starPuzzleClearEffect)
                {
                    effect.Play();
                    cam.PuzzleClearView(effect, true);
                }
            }
        }
        //1-2퍼즐
        else if (PuzzleNumber == 1)
        {
            for (int i = 0; i < starPuzzle2.Length; i++)
            {
                //모든 퍼즐조각이 포인트가 0인가?(0이점답임)
                if (starPuzzle2[i].myPoint == 0)
                {
                    clearPoint++;
                }
            }
            //퍼즐2 이펙트 플레이
            if (clearPoint == starPuzzle2.Length)
            {
                starPuzzle2Clear = true;
                foreach (ParticleSystem effect in starPuzzleClearEffect2)
                {
                    effect.Play();
                    cam.PuzzleClearView(effect, true);
                }
            }
        }
    }

    //올클리어 이벤트 ( 1번퍼즐의 올클리어면 매개변수 1을 넣어서 출력 )
    public bool PuzzleClearCheck(int puzzleNumber)
    {
        switch (puzzleNumber)
        {
            case 1:
                if (starPuzzle1Clear && starPuzzle2Clear)
                {
                    starPuzzleAllClearEffect[0].Play();
                    GameObject.Find("CineManager").GetComponent<CineMachineScript>().PlayPuzzleCine(1);
                    Invoke("StarPuzzleallClearEffect_2", 1);
                    return true;
                }
                break;
            case 2:
                if (potatoPuzzle1Clear && potatoPuzzle2Clear)
                {
                    potatoPuzzleAllClearEffect[0].Play();
                    GameObject.Find("CineManager").GetComponent<CineMachineScript>().PlayPuzzleCine(2);
                    Invoke("PotatoPuzzleallClearEffect_2", 1);
                    return true;
                }
                break;
        }

        return false;
    }

    void StarPuzzleallClearEffect_2()
    {
        starPuzzleAllClearEffect[1].Play();
    }
    void PotatoPuzzleallClearEffect_2()
    {
        potatoPuzzleAllClearEffect[1].Play();
    }

    //스타 퍼즐 클리어 체크 (1탄퍼즐) 매개변수는 1-1 인지 1-2인지 체크
    public void PotatoPuzzleClearCheck(int PuzzleNumber)
    {
        int clearPoint = 0;
        //1-1퍼즐
        if (PuzzleNumber == 0)
        {
            //퍼즐 모두다 맞았는지 체크
            for (int i = 0; i < potatoPuzzle1.Length; i++)
            {
                if (potatoPuzzle1[i].myPoint == 0)
                {
                    clearPoint++;
                }
            }
            for (int i = 0; i < potato1.Length; i++)
            {
                if (potato1[i].resultNum == -1) { clearPoint++; }
                else if (potato1[i].resultNum == potato1[i].resultNum)
                {
                    clearPoint++;
                }
            }
            //두개다 맞으면 클리어 이펙트 플레이
            if (clearPoint == potatoPuzzle1.Length + potato1.Length)
            {
                potatoPuzzle1Clear = true;
                foreach (ParticleSystem effect in potatoPuzzleClearEffect)
                {
                    effect.Play();
                    cam.PuzzleClearView(effect, true);
                }
            }
        }
        //1-2퍼즐
        else if (PuzzleNumber == 1)
        {
            //퍼즐 모두다 맞았는지 체크
            for (int i = 0; i < potatoPuzzle2.Length; i++)
            {
                if (potatoPuzzle2[i].myPoint == 0)
                {
                    clearPoint++;
                }
            }
            for (int i = 0; i < potato2.Length; i++)
            {
                if (potato2[i].resultNum == -1) { clearPoint++; }
                else if (potato2[i].resultNum == potato2[i].resultNum)
                {
                    clearPoint++;
                }
            }
            //두개다 맞으면 클리어 이펙트 플레이
            if (clearPoint == potatoPuzzle2.Length + potato2.Length)
            {
                potatoPuzzle2Clear = true;
                foreach (ParticleSystem effect in potatoPuzzleClearEffect2)
                {
                    effect.Play();
                    cam.PuzzleClearView(effect, true);
                }
            }
        }
    }

    //플레닛 퍼즐 클리어 체크
    public void PlanetPuzzleClearCheck(int puzzleNumber)
    {
        if (puzzleNumber == 4)
            return;
        int clearPoint = 0;

        //1개맞으면 반짝하는거
        for (int i = puzzleNumber; i < puzzleNumber + 1; i++)
        {
            planetPuzzleParticle[i].Play();
        }

        //퍼즐 다 맞았는지 체크
        for (int i = 0; i < planetPuzzle.Length; i++)
        {
            if (planetPuzzle[i].myPoint == 0)
            {
                clearPoint++;
            }
        }
        //전부다 맞으면 클리어 이펙트 플레이
        if (clearPoint == planetPuzzle.Length)
        {
            foreach (ParticleSystem effect in starPuzzleClearEffect)
            {
                effect.Play();

                for (int i = puzzleNumber; i < puzzleNumber + 1; i++)
                    planetPuzzleLineParticle[i].Play();

                cam.PuzzleClearView(planetPuzzleLineParticle[0], true);
            }
        }
    }

    //플레닛에 이펙트를 전부 꺼주기
    public void PlanetReset()
    {
        foreach (ParticleSystem effect in planetPuzzleParticle)
        {
            effect.Stop();
        }
    }
}