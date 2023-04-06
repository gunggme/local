using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverManager : MonoBehaviour
{
    [SerializeField] InputField inputName;

    [SerializeField] Animator main;
    [SerializeField] Animator rank;

    [Header("Rank")]
    [SerializeField] float[] rankScore;
    [SerializeField] float[] rankTime;
    [SerializeField] string[] rankName;
    [SerializeField] Text[] rankText;
    [SerializeField] RectTransform rankRect;

    [Header("Stage1")]
    [SerializeField] float stage1Score;
    [SerializeField] float stage1Time;
    [SerializeField] Text endText1;

    [Header("StageAll")]
    [SerializeField] string playerName;
    [SerializeField] float playerScore;
    [SerializeField] float playerTime;
    [SerializeField] Text endText2;

    [SerializeField] bool isUp;
    [SerializeField] float upSpeed;
    [SerializeField] AudioSource clickSound;

    private void Start()
    {
        endText1.text = "Stage1\nScore : " + stage1Score.ToString("N0") + "\nTime : " + stage1Time.ToString("N0");
        endText2.text = "All Stage\nScore : " + playerScore.ToString("N0") + "\nTime : " + playerTime.ToString("N0");
    }

    

    private void Awake()
    {
        //Rank
        rankScore = new float[10];
        rankTime = new float[10];
        rankName = new string[10];
        for(int i = 0; i < 10; i++)
        {
            rankScore[i] = PlayerPrefs.GetFloat(i + "rankScore");
            rankTime[i] = PlayerPrefs.GetFloat(i + "rankTime");
            rankName[i] = PlayerPrefs.GetString(i + "rankName");

            rankText[i].text = "Rank " + (i + 1) + " " + rankName[i] + "\nScore : " + rankScore[i].ToString("N0") + "\nTime : " + Mathf.Round((rankTime[i] * 10) / 10);
        }
        //Stage1
        stage1Score = PlayerPrefs.GetFloat("stage1Score");
        stage1Time = PlayerPrefs.GetFloat("stage1Time");

        //AllStage
        playerScore = PlayerPrefs.GetFloat("curScore");
        playerTime = PlayerPrefs.GetFloat("curTime");
    }

    private void Update()
    {
        if (inputName.text.Length > 1 && inputName.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            NameSet();
            clickSound.Play();
        }

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

    void NameSet()
    {
        playerName = inputName.text;
        inputName.gameObject.SetActive(false);
        RankSet(playerScore, playerTime, playerName);
    }

    void RankSet(float score, float time, string name)
    {
        float tmpScore = 0;
        float tmpTime = 0;
        string tmpName = "";

        for(int i = 0; i < 10; i++)
        {
            while(score > rankScore[i])
            {
                tmpScore = rankScore[i];
                tmpTime = rankTime[i];
                tmpName = rankName[i];

                rankScore[i] = score;
                rankTime[i] = time;
                rankName[i] = name;

                score = tmpScore;
                time = tmpTime;
                name = tmpName;

            }
        }

        for(int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetFloat(i + "rankScore", rankScore[i]);
            PlayerPrefs.SetFloat(i + "rankTime", rankTime[i]);
            PlayerPrefs.SetString(i + "rankName", rankName[i]);
            rankText[i].text = "Rank " + (i + 1) + " " + rankName[i] + "\nScore : " + rankScore[i].ToString("N0") + "\nTime : " + rankTime[i];
        }
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
        Invoke("Main", 1);
        isUp = false;
    }

    public void BtnGoHome()
    {
        clickSound.Play();
        main.SetTrigger("Close");
        Invoke("Home", 1);
    }

    public void BtnReStart()
    {
        clickSound.Play();
        main.SetTrigger("Close");
        Invoke("ReStart", 1);
    }

    void Home()
    {
        SceneManager.LoadScene(0);
    
    }

    void ReStart()
    {
        SceneManager.LoadScene(1);
    }

    void Rank()
    {
        isUp = true;
        rankRect.anchoredPosition = new Vector2(0, -858);
        rank.SetTrigger("Open");
    }

    void Main()
    {
        main.SetTrigger("Open");
    }
}
