using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
//using static System.Net.Mime.MediaTypeNames;
//using UnityEngine.UIElements;


//230905 ��ȣ�� comment
//  ���� uimanager�� ������ �ִ� prefab�� ���� �Ƹ� script�� �޾Ƽ� �׷� �� 
//  UI manager�� ��� ���̰� �̹� ���� Ʋ�� ���� ������ �ʿ��� ����
//  Ui manager ���� ���� ���� ���� �ΰ� ���� �ʿ� ���


public class SceneUiManager : MonoBehaviour
{
    public List<GameObject> gameOptionBtnList;
    public List<GameObject> stageBtnList;
    public Sprite[] exampleSprite;
    public Image introBg;

    // �������� ����
    public int currentStageIndex = 0;
    private int minStageIndex = 0;
    private int maxStageIndex = 1;
    // �÷��� ���
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

    
    // StageSelect, Example, InGame -> MainMenu ��ư �̵�
    public void MainMenuMove()
    {
        SoundManager.Instance.bgmScene = SoundManager.BgmScene.Main;
        SoundManager.Instance.BgmPlay(SoundManager.Instance.bgmScene);
        SceneController.Instance.SceneLoader("MainMenu");
    }
    // Intro ���� -> ��� ��� = ��ο� -> 2�ʵ� MainMenu
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
    // ���丮 ���� �� �ݱ�
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

    // MainMenu -> BackIntro ��ư �̵�
    public void BackIntroMove()
    {        
        SceneController.Instance.SceneLoader("Intro");
    }

    // MainMenu -> StageSelect ��ư �̵�
    public void StageSelectMove()
    {
        SceneController.Instance.SceneLoader("StageSelect");
    }

    // MainMenu -> Example ��ư �̵�
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

    // �������� �ѱ�� Ȯ��
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
    // �÷��� ��� �ѱ�� Ȯ��
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