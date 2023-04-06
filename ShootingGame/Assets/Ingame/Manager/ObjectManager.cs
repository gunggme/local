using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [Header("Particle")]
    [SerializeField] GameObject explodePrefab;
    [Header("Item")]
    [SerializeField] GameObject fuerPrefab;
    [SerializeField] GameObject powerPrefab;
    [SerializeField] GameObject mzPrefab;
    [SerializeField] GameObject hpPrefab;
    [SerializeField] GameObject shieldPrefab;
    [SerializeField] GameObject skill1Prefab;
    [SerializeField] GameObject skill2Prefab;
    [Header("Enem")]
    [SerializeField] GameObject enemSPrefab;
    [SerializeField] GameObject enemMPrefab;
    [SerializeField] GameObject enemLPrefab;
    [SerializeField] GameObject meteor1Prefab;
    [SerializeField] GameObject meteor2Prefab;
    [SerializeField] GameObject enemYPrefab;
    [Header("EnemBullet")]
    [SerializeField] GameObject enemBullet1Prefab;
    [SerializeField] GameObject enemBullet2Prefab;
    [SerializeField] GameObject enemBullet3Prefab;
    [SerializeField] GameObject enemBullet4Prefab;
    [SerializeField] GameObject enemBullet5Prefab;
    [Header("PlayerBullet")]
    [SerializeField] GameObject playerBullet1Prefab;
    [SerializeField] GameObject playerBullet2Prefab;

    //Particle
    GameObject[] explode;
    //Item
    GameObject[] fuer;
    GameObject[] power;
    GameObject[] mz;
    GameObject[] hp;
    GameObject[] shield;
    GameObject[] skill1;
    GameObject[] skill2;
    //Enem
    GameObject[] enemS;
    GameObject[] enemM;
    GameObject[] enemL;
    GameObject[] meteor1;
    GameObject[] meteor2;
    GameObject[] enemY;
    //EnemBullet
    GameObject[] enemBullet1;
    GameObject[] enemBullet2;
    GameObject[] enemBullet3;
    GameObject[] enemBullet4;
    GameObject[] enemBullet5;
    //PlayerBullet
    GameObject[] playerBullet1;
    GameObject[] playerBullet2;

    GameObject[] targetPool;

    private void Awake()
    {
        //Particle
        explode = new GameObject[20];
        //Item
        fuer = new GameObject[10];
        power = new GameObject[10];
        mz = new GameObject[10];
        hp = new GameObject[10];
        shield = new GameObject[10];
        skill1 = new GameObject[10];
        skill2 = new GameObject[10];
        //Enem
        enemS = new GameObject[20];
        enemM = new GameObject[20];
        enemL = new GameObject[20];
        meteor1 = new GameObject[20];
        meteor2 = new GameObject[20];
        enemY = new GameObject[20];
        //EnemBullet
        enemBullet1 = new GameObject[300];
        enemBullet2 = new GameObject[300];
        enemBullet3 = new GameObject[300];
        enemBullet4 = new GameObject[300];
        enemBullet5 = new GameObject[300];
        //PlayerBullet
        playerBullet1 = new GameObject[100];
        playerBullet2 = new GameObject[100];

        Gen();
    }

    void Gen()
    {
        //Particle
        for(int i = 0; i < explode.Length; i++)
        {
            explode[i] = Instantiate(explodePrefab);
            explode[i].gameObject.SetActive(false);
        }
        //Item
        for(int i= 0; i < fuer.Length; i++)
        {
            fuer[i] = Instantiate(fuerPrefab);
            fuer[i].SetActive(false);
        }
        for (int i = 0; i < power.Length; i++)
        {
            power[i] = Instantiate(powerPrefab);
            power[i].SetActive(false);
        }
        for (int i = 0; i < hp.Length; i++)
        {
            hp[i] = Instantiate(hpPrefab);
            hp[i].SetActive(false);
        }
        for (int i = 0; i < mz.Length; i++)
        {
            mz[i] = Instantiate(mzPrefab);
            mz[i].SetActive(false);
        }
        for (int i = 0; i < shield.Length; i++)
        {
            shield[i] = Instantiate(shieldPrefab);
            shield[i].SetActive(false);
        }
        for (int i = 0; i < skill1.Length; i++)
        {
            skill1[i] = Instantiate(skill1Prefab);
            skill1[i].SetActive(false);
        }
        for (int i = 0; i < skill2.Length; i++)
        {
            skill2[i] = Instantiate(skill2Prefab);
            skill2[i].SetActive(false);
        }
        //Enem
        for (int i = 0; i < enemS.Length; i++)
        {
            enemS[i] = Instantiate(enemSPrefab);
            enemS[i].SetActive(false);
        }
        for (int i = 0; i < enemM.Length; i++)
        {
            enemM[i] = Instantiate(enemMPrefab);
            enemM[i].SetActive(false);
        }
        for (int i = 0; i < enemL.Length; i++)
        {
            enemL[i] = Instantiate(enemLPrefab);
            enemL[i].SetActive(false);
        }
        for (int i = 0; i < meteor1.Length; i++)
        {
            meteor1[i] = Instantiate(meteor1Prefab);
            meteor1[i].SetActive(false);
        }
        for (int i = 0; i < meteor2.Length; i++)
        {
            meteor2[i] = Instantiate(meteor2Prefab);
            meteor2[i].SetActive(false);
        }
        for(int i = 0; i < enemY.Length; i++)
        {
            enemY[i] = Instantiate(enemYPrefab);
            enemY[i].gameObject.SetActive(false);
        }
        //EnemBullet
        for (int i = 0; i < enemBullet1.Length; i++)
        {
            enemBullet1[i] = Instantiate(enemBullet1Prefab);
            enemBullet1[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < enemBullet2.Length; i++)
        {
            enemBullet2[i] = Instantiate(enemBullet2Prefab);
            enemBullet2[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < enemBullet3.Length; i++)
        {
            enemBullet3[i] = Instantiate(enemBullet3Prefab);
            enemBullet3[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < enemBullet4.Length; i++)
        {
            enemBullet4[i] = Instantiate(enemBullet4Prefab);
            enemBullet4[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < enemBullet3.Length; i++)
        {
            enemBullet5[i] = Instantiate(enemBullet5Prefab);
            enemBullet5[i].gameObject.SetActive(false);
        }
        //PlayerBullet
        for (int i = 0; i < playerBullet1.Length; i++)
        {
            playerBullet1[i] = Instantiate(playerBullet1Prefab);
            playerBullet1[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < playerBullet2.Length; i++)
        {
            playerBullet2[i] = Instantiate(playerBullet2Prefab);
            playerBullet2[i].gameObject.SetActive(false);
        }
    }

    public GameObject MakeObj(string names)
    {
        switch (names)
        {
            //Particle
            case "Explode":
                targetPool = explode;
                break;
            //Item
            case "Fuer":
                targetPool = fuer;
                break;
            case "Power":
                targetPool = power;
                break;
            case "MZ":
                targetPool = mz;
                break;
            case "HP":
                targetPool = hp;
                break;
            case "Shield":
                targetPool = shield;
                break;
            case "Skill1":
                targetPool = skill1;
                break;
            case "Skill2":
                targetPool = skill2;
                break;
            //Enem
            case "EnemS":
                targetPool = enemS;
                break;
            case "EnemM":
                targetPool = enemM;
                break;
            case "EnemL":
                targetPool = enemL;
                break;
            case "Meteor1":
                targetPool = meteor1;
                break;
            case "Meteor2":
                targetPool = meteor2;
                break;
            case "EnemY":
                targetPool = enemY;
                break;
            //EnemBullet
            case "EnemBullet1":
                targetPool = enemBullet1;
                break;
            case "EnemBullet2":
                targetPool = enemBullet2;
                break;
            case "EnemBullet3":
                targetPool = enemBullet3;
                break;
            case "EnemBullet4":
                targetPool = enemBullet4;
                break;
            case "EnemBullet5":
                targetPool = enemBullet5;
                break;
            //PlayerBulllet
            case "PlayerBullet1":
                targetPool = playerBullet1;
                break;
            case "PlayerBullet2":
                targetPool = playerBullet2;
                break;
        }

        for(int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].gameObject.SetActive(true);
                return targetPool[i];
            }
        }

        return null;
    }
}
