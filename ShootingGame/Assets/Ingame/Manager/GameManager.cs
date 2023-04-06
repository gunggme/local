using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ObjectManager objMana;
    [SerializeField] GameObject playerP;
    public SpawnManager spawnMana;
    public CamShake camShake;

    public Player player;
    [Header("HP")]
    [SerializeField] Image[] hps;
    [SerializeField] Color noColor = new Color(1, 1, 1, 0);
    [SerializeField] Color color = new Color(1, 1, 1, 1);

    [Header("Fuer")]
    [SerializeField] Slider fuerBar;

    [Header("Score || Time")]
    public float pScore;
    [SerializeField] public float timer;
    [SerializeField] float rScore;
    [SerializeField] Text scoreText;
    [SerializeField] Text timeText;
    [SerializeField] bool isTime;

    [Header("Stage")]
    public int stageNum;
    [SerializeField] Animator stageAni;
    [SerializeField] Text stageText;

    [Header("OverScrine")]
    [SerializeField] Animator scrine;
    [SerializeField] Text stageStatText;
    [SerializeField] Text scoreText2;
    [SerializeField] Text bonusText;
    [SerializeField] Text timeText2;
    [SerializeField] float bonus;

    [Header("Stage1")]
    [SerializeField] Stage1Boss boss1;
    [SerializeField] GameObject boss1Obj;
    [SerializeField] Slider boss1HP;
    [Header("Stage2")]
    [SerializeField] Stage2Boss boss2;
    [SerializeField] GameObject boss2Obj;
    [SerializeField] Slider boss2HP;


    [Header("Boss")]
    [SerializeField] float bossTime;
    [SerializeField] Slider bossTimer;
    [SerializeField] bool isBoss;

    [Header("ETC")]
    [SerializeField] AudioClip clearSound;
    [SerializeField] AudioClip overSound;
    [SerializeField] AudioSource audios;
    [SerializeField] AudioSource scoreSound;
    [SerializeField] GameObject pop;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PlayerPrefs.SetFloat("stage1Score", 0);
        PlayerPrefs.SetFloat("stage1Time", 0);
        PlayerPrefs.SetFloat("curScore", 0);
        PlayerPrefs.SetFloat("curTime", 0);
        StartCoroutine("ScoreSet");
        StartCoroutine("StageStart");
    }

    private void Update()
    {
        bossTimer.value = bossTime / 100;
        if(boss1HP.value <= 0 && boss1HP.gameObject.activeSelf)
        {
            boss1Obj.SetActive(false);
            boss1HP.gameObject.SetActive(false);
            StartCoroutine("Stage1Over");
        }
        if (boss2HP.value < 0 && boss2HP.gameObject.activeSelf)
        {
            boss2Obj.SetActive(false);
            boss2HP.gameObject.SetActive(false);
            StartCoroutine("Stage2Over");
        }
        if(fuerBar.value <= 0)
        {
            StartCoroutine("GameOver");
        }
        BossTimer();
        Timer();
        Boss1HPSet();
        Boss2HPSet();
        
        scoreText.text = "Score : " + rScore.ToString("N0");
        timeText.text = "Time : " + Mathf.Round((timer * 10) / 10).ToString("N0");
        fuerBar.value = player.fuer / 100;
    }
    WaitForSeconds wait1 = new(1);

    IEnumerator StageStart()
    {
        stageNum++;
        stageText.text = "Stage " + stageNum + "Start!";
        stageAni.SetTrigger("On");
        yield return wait1;
        player.isDown = true;
        isBoss = false;
        bossTime = 100;
        spawnMana.isSpawn = true;
        isTime = true;
    }

    public IEnumerator Stage1Over()
    {
        pop.gameObject.SetActive(true);
        audios.clip = clearSound;
        audios.Play();
        isBoss = true;
        player.isDown = false;
        isTime = false;
        spawnMana.isSpawn = false;
        bonus = player.hp * 100 * player.fuer;
        stageStatText.text = "Stage 1Clear!";
        scoreText2.text = "Score : " + rScore.ToString("N0");
        bonusText.text = "Bonus + " + bonus.ToString("N0");
        timeText2.text = "Time : " + Mathf.Round((timer * 10) / 10).ToString("N0");
        stageText.text = "Stage 1Over!";
        PlayerPrefs.SetFloat("stage1Score", rScore);
        PlayerPrefs.SetFloat("stage1Time", timer);
        stageAni.SetTrigger("On");
        yield return wait1;
        scrine.SetTrigger("Open");
        pScore += bonus;
    }

    IEnumerator Stage2Over()
    {
        pop.gameObject.SetActive(true);
        audios.clip = clearSound;
        audios.Play();
        isBoss = true;
        player.isDown = false;
        isTime = false;
        bossTime = 100;
        spawnMana.isSpawn = false;
        bonus = player.hp * 100 * player.fuer;
        stageStatText.text = "Stage 2Clear!";
        scoreText2.text = "Score : " + rScore.ToString("N0");
        bonusText.text = "Bonus + " + bonus.ToString("N0");
        timeText2.text = "Time : " + Mathf.Round((timer * 10) / 10).ToString("N0");
        stageText.text = "Stage 2Clear!";
        PlayerPrefs.SetFloat("curScore", rScore);
        PlayerPrefs.SetFloat("curTime", timer);
        stageAni.SetTrigger("On");
        yield return wait1;
        stageText.text = "Game Over";
        stageAni.SetTrigger("On");
        yield return wait1;
        scrine.SetTrigger("Open");
        pScore += bonus;
    }

    IEnumerator GameOver()
    {
        audios.clip = overSound;
        audios.Play();
        playerP.gameObject.SetActive(false);
        isBoss = true;
        player.isDown = false;
        isTime = false;
        spawnMana.isSpawn = false;
        bonus = 0;
        stageNum = -1;
        stageStatText.text = "Game Over..";
        scoreText2.text = "Score : " + rScore.ToString("N0");
        bonusText.text = "Bonus + " + bonus.ToString("N0");
        timeText2.text = "Time + " + Mathf.Round((timer * 10) / 10).ToString("N0");
        stageText.text = "Game Over..";
        PlayerPrefs.SetFloat("curScore", rScore);
        PlayerPrefs.SetFloat("curTime", timer);
        stageAni.SetTrigger("On");
        yield return wait1;
        scrine.SetTrigger("Open");
    }

    bool isClick = false;
    public void OKBtn()
    {
       
        if (!isClick)
        {
            isClick = true;
            scrine.SetTrigger("Close");
            switch (stageNum)
            {
                case -1:
                    StartCoroutine("SceneMove");
                    break;
                case 1:
                    StartCoroutine("Maybe1");
                    break;
                case 2:
                    StartCoroutine("SceneMove");
                    break;
            }
        }
    }

    IEnumerator Maybe1()
    {
        yield return wait1;
        isClick = false;
        StartCoroutine("StageStart");
    }

    IEnumerator SceneMove()
    {
        yield return wait1;
        isClick = false;
        SceneManager.LoadScene(2);
    }

    void Timer()
    {
        if (isTime)
        {
            timer += Time.deltaTime;
        }
    }

    public void HPSet()
    {
        switch (player.hp)
        {
            case 0:
                StartCoroutine("GameOver");
                hps[0].color = noColor;
                hps[1].color = noColor;
                hps[2].color = noColor;
                hps[3].color = noColor;
                hps[4].color = noColor;
                break;
            case 1:
                hps[0].color = color;
                hps[1].color = noColor;
                hps[2].color = noColor;
                hps[3].color = noColor;
                hps[4].color = noColor;
                break;
            case 2:
                hps[0].color = color;
                hps[1].color = color;
                hps[2].color = noColor;
                hps[3].color = noColor;
                hps[4].color = noColor;
                break;
            case 3:
                hps[0].color = color;
                hps[1].color = color;
                hps[2].color = color;
                hps[3].color = noColor;
                hps[4].color = noColor;
                break;
            case 4:
                hps[0].color = color;
                hps[1].color = color;
                hps[2].color = color;
                hps[3].color = color;
                hps[4].color = noColor;
                break;
            case 5:
                hps[0].color = color;
                hps[1].color = color;
                hps[2].color = color;
                hps[3].color = color;
                hps[4].color = color;
                break;
        }
    }

    void Boss1HPSet()
    {
        boss1HP.value = boss1.hp / 1500;
    }

    void Boss2HPSet()
    {
        boss2HP.value = boss2.hp / 2500;
    }

    IEnumerator ScoreSet()
    {
        while (true)
        {
            if(pScore > 0)
            {
                scoreSound.Play();
                pScore -= 100;
                rScore += 100;
            }
            else if(pScore <= 0)
            {
                scoreSound.Stop();
            }
            yield return null;
        }
    }

    void BossTimer()
    {
        if (isTime)
        {
            if(bossTime > 0)
            {
                bossTime -= Time.deltaTime;
                return;
            }
            if (!isBoss)
            {
                switch (stageNum)
                {
                    case 1:
                        boss1Obj.gameObject.SetActive(true);
                        boss1HP.gameObject.SetActive(true);
                        spawnMana.isSpawn = false;
                        isBoss = true;
                        break;
                    case 2:
                        boss2Obj.SetActive(true);
                        boss2HP.gameObject.SetActive(true);
                        spawnMana.isSpawn = false;
                        isBoss = true;
                        break;
                }
            }
        }
    }
}
