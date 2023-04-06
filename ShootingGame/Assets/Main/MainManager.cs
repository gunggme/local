using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    [Header("Rank")]
    [SerializeField] float[] rankScore;
    [SerializeField] float[] rankTime;
    [SerializeField] string[] rankName;
    [SerializeField] Text[] rankText;
    [SerializeField] RectTransform rankRect;

    [SerializeField] bool isUp;
    [SerializeField] float upSpeed;

    [SerializeField] Animator main;
    [SerializeField] Animator rank;
    [SerializeField] Animator htp;

    [SerializeField] AudioSource clickSound;

    private void Awake()
    {
        //Rank
        rankScore = new float[10];
        rankTime = new float[10];
        rankName = new string[10];
        for (int i = 0; i < 10; i++)
        {
            rankScore[i] = PlayerPrefs.GetFloat(i + "rankScore");
            rankTime[i] = PlayerPrefs.GetFloat(i + "rankTime");
            rankName[i] = PlayerPrefs.GetString(i + "rankName");

            rankText[i].text = "Rank " + (i + 1) + " " + rankName[i] + "\nScore : " + rankScore[i].ToString("N0") + "\nTime : " + Mathf.Round((rankTime[i] * 10) / 10);
        }
    }

    private void Update()
    {
        if (isUp)
        {
            rankRect.anchoredPosition += Vector2.up * upSpeed * Time.deltaTime;
            if (Input.GetMouseButton(0))
            {
                upSpeed = 120;
            }
            else
            {
                upSpeed = 60;
            }
            if (rankRect.anchoredPosition.y > 2555)
            {
                rankRect.anchoredPosition = new Vector2(0, -858);
            }
        }
    }

    public void BtnStartGame()
    {
        clickSound.Play();
        main.SetTrigger("Close");
        Invoke("GameStart", 1.2f);
    }

    void GameStart()
    {
        SceneManager.LoadScene(1);
    }

    void Main()
    {
        main.SetTrigger("Open");
    }

    public void BtnOpenRank()
    {
        clickSound.Play();
        main.SetTrigger("Close");
        Invoke("Rank", 1);
    }

    public void BtnCloseRank()
    {
        clickSound.Play();
        rank.SetTrigger("Close");
        isUp = false;
        Invoke("Main", 1);
    }

    public void GameQuit()
    {
        clickSound.Play();
        main.SetTrigger("Close");
        Invoke("EXIT", 1);
    }

    void Rank()
    {
        rank.SetTrigger("Open");
        isUp = true;
        rankRect.anchoredPosition = new Vector2(0, -858);
    }

    public void BtnOpenHtp()
    {
        clickSound.Play();
        main.SetTrigger("Close");
        Invoke("Htp", 1);
    }

    public void BtnCloseHtp()
    {
        clickSound.Play();
        htp.SetTrigger("Close");
        Invoke("Main", 1);
    }

    void Htp()
    {
        htp.SetTrigger("Open");
    }

    void EXIT()
    {
        Application.Quit();
    }
}
