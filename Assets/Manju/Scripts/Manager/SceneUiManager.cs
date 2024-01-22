using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
//using static System.Net.Mime.MediaTypeNames;
//using UnityEngine.UIElements;


//230905 지호준 comment
//  현재 uimanager가 가지고 있는 prefab이 없음 아마 script만 받아서 그런 듯 
//  UI manager의 경우 성미가 이미 만든 틀에 따라서 변경이 필요해 보임
//  Ui manager 만들 때는 옆에 성미 두고 개발 필요 요망


public class SceneUiManager : MonoBehaviour
{
    public List<GameObject> gameOptionBtnList;
    public List<GameObject> stageBtnList;
    public Sprite[] exampleSprite;
    public Image introBg;

    // 스테이지 선택
    public int currentStageIndex = 0;
    private int minStageIndex = 0;
    private int maxStageIndex = 1;
    // 플레이 방법
    public int currentExampleIndex = 0;
    private int minExampleIndex = 0;
    private int maxExampleIndex = 2;

    public static SceneUiManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<SceneUiManager>();
            }
            return instance;
        }
    }
    private static SceneUiManager instance;

    
    // StageSelect, Example, InGame -> MainMenu 버튼 이동
    public void MainMenuMove()
    {
        SoundManager.Instance.bgmScene = SoundManager.BgmScene.Main;
        SoundManager.Instance.BgmPlay(SoundManager.Instance.bgmScene);
        SceneController.Instance.SceneLoader("MainMenu");
    }
    // Intro 줌인 -> 배경 밝기 = 어두움 -> 2초뒤 MainMenu
    public void ZoomIn()
    {
        StartCoroutine(ZoomInCo());
    }
    public IEnumerator ZoomInCo()
    {
        introBg.rectTransform.DOScale(60,3);
        introBg.rectTransform.GetComponent<Image>().DOColor(Color.black, 2);
        yield return new WaitForSeconds(2);
        MainMenuMove();
    }
    // 스토리 열기 및 닫기
    public void SynopsisUi()
    {
        StartCoroutine(ShowSynopsisCo());
    }
    public IEnumerator ShowSynopsisCo()
    {
        gameOptionBtnList[2].SetActive(true);
        yield return new WaitForSeconds(5);
        gameOptionBtnList[2].SetActive(false);
    }

    // MainMenu -> BackIntro 버튼 이동
    public void BackIntroMove()
    {        
        SceneController.Instance.SceneLoader("Intro");
    }

    // MainMenu -> StageSelect 버튼 이동
    public void StageSelectMove()
    {
        SceneController.Instance.SceneLoader("StageSelect");
    }

    // MainMenu -> Example 버튼 이동
    public void ExampleMove()
    {
        SceneController.Instance.SceneLoader("Example");
    }

    public void Stage01Move()
    {
        SoundManager.Instance.bgmScene = SoundManager.BgmScene.Battle;
        SoundManager.Instance.BgmPlay(SoundManager.Instance.bgmScene);
        SceneController.Instance.SceneLoader("Stage01");        
    }

    public void GameOptionUi(bool isShow)
    {
        if (isShow)
        {
            SoundManager.Instance.OptionUi.gameObject.SetActive(isShow);
            Time.timeScale = 0.0f;
        }
        else
        {
            SoundManager.Instance.OptionUi.gameObject.SetActive(isShow);
            Time.timeScale = 1.0f;
        }
    }

    public void GameExitUi(bool isShow)
    {
        gameOptionBtnList[1].SetActive(isShow);
    }

    // 스테이지 넘기며 확인
    public void NextStagerUi()
    {
        if (currentStageIndex < maxStageIndex)
        {
            currentStageIndex++;
            stageBtnList[currentStageIndex - 1].SetActive(false);
            stageBtnList[currentStageIndex].SetActive(true);
        }
    }
    public void PrevStagerUi()
    {
        if (currentStageIndex > minStageIndex)
        {
            currentStageIndex--;
            stageBtnList[currentStageIndex + 1].SetActive(false);
            stageBtnList[currentStageIndex].SetActive(true);
        }
    }
    // 플레이 방법 넘기며 확인
    public void NextExampleUi()
    {
        if (currentExampleIndex < maxExampleIndex)
        {
            currentExampleIndex++;
            GameObject.Find("Example01_Image").GetComponent<Image>().sprite = exampleSprite[currentExampleIndex];
        }
    }
    public void PrevExampleUi()
    {
        if (currentExampleIndex > minExampleIndex)
        {
            currentExampleIndex--;
            GameObject.Find("Example01_Image").GetComponent<Image>().sprite = exampleSprite[currentExampleIndex];
        }
    }
}