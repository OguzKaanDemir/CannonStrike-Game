using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject StartScene, ShootButton, WinScene, LoseScene, NewLevel;
    [SerializeField] GameObject[] Levels;
    Weapon weapon;
    TMP_Text TargetText, MaxBallText;

    public int levelIndex, currnetPoint, currnentMaxBall;
    public int[] targetPoint, maxBall;
    public bool isWin, isLose;

    private void Update()
    {
        if (weapon == null)
        {
            weapon = FindObjectOfType<Weapon>();
        }

        Target();
    }

    public void Shoot()
    {
        if (weapon != null)
        {
            weapon.Shoot();
            currnentMaxBall--;
            if (currnentMaxBall == 0 && currnetPoint < targetPoint[levelIndex])
            {
                isLose = true;
                StartCoroutine(CheckLose());
            }
            CurrentMaxBallText();
        }
    }

    public void StartGameScene()
    {
        newLevel();
        StartScene.SetActive(false);
        ShootButton.SetActive(true);
    }

    void Target()
    {
        if (GameObject.Find("Target") != null)
        {
            TargetText = GameObject.Find("Target").GetComponent<TMP_Text>();
            TargetText.text = ((currnetPoint) + " / " + targetPoint[levelIndex]).ToString();
            
        }
    }

    public IEnumerator CheckWin()
    {
        if (currnetPoint >= targetPoint[levelIndex])
        {
            isLose = false;
            yield return new WaitForSeconds(1);
            WinScene.transform.GetChild(0).GetComponent<TMP_Text>().text = $"LEVEL {levelIndex + 1} COMPLATED!";
            isWin = true;
            ShootButton.SetActive(false);
            NewLevel.SetActive(false);
            WinScene.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public IEnumerator CheckLose()
    {
        yield return new WaitForSeconds(2f);

        if (isLose)
        {
            isLose = false;
            LoseScene.transform.GetChild(0).GetComponent<TMP_Text>().text = $"LEVEL {levelIndex + 1} FAILED!";
            ShootButton.SetActive(false);
            NewLevel.SetActive(false);
            LoseScene.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ReStart()
    {
        isLose = false;
        isWin = false;
        currnetPoint = 0;
        ShootButton.SetActive(true);
        LoseScene.SetActive(false);
        DestroyLevel();
        newLevel();
        Time.timeScale = 1;
    }

    public void NextLevel()
    {
        isWin = false;
        isLose = false;
        levelIndex += 1;
        currnetPoint = 0;
        ShootButton.SetActive(true);
        WinScene.SetActive(false);
        DestroyLevel();
        newLevel();
        Time.timeScale = 1;
    }

    void newLevel()
    {
        NewLevel = Instantiate(Levels[levelIndex]);
        NewLevel.name = "NewLevel";
        NewLevel.SetActive(true);
        UpdateMaxBallText();
    }

    void DestroyLevel()
    {
        Destroy(NewLevel);
    }

    void UpdateMaxBallText()
    {
        MaxBallText = GameObject.FindGameObjectWithTag("weapon").gameObject.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        currnentMaxBall = maxBall[levelIndex];
        MaxBallText.text = maxBall[levelIndex].ToString();
    }

    void CurrentMaxBallText()
    {
        MaxBallText = GameObject.FindGameObjectWithTag("weapon").gameObject.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        MaxBallText.text = currnentMaxBall.ToString();
    }
}
