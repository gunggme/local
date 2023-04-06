using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPositions;

    [SerializeField] string[] enemNames1;
    [SerializeField] string[] enemNames2;
    [SerializeField] string[] enemName;

    [SerializeField] float curDelay;
    [SerializeField] float nextDelay;

    [SerializeField] public bool isSpawn;

    [SerializeField] int ranSpawn;
    [SerializeField] int ranEnem;

    private void Awake()
    {
        enemNames1 = new string[] { "Meteor1", "EnemY", "Meteor2", "Meteor1", "Meteor2", "Meteor1", "EnemS", "EnemS", "EnemM", "EnemL" };
        enemNames2 = new string[] { "EnemY", "EnemM", "EnemL", "Meteor1", "Meteor2", "Meteor1", "EnemS", "EnemS", "EnemY", "EnemL" };
    }

    private void Update()
    {
        SpawnWait();
    }

    void SpawnWait()
    {
        if (isSpawn)
        {
            if(curDelay < nextDelay)
            {
                curDelay += Time.deltaTime;
                return;
            }

            Spawn();
            curDelay = 0;
            nextDelay = Random.Range(0.8f, 1.3f);
        }
    }

    void Spawn()
    {
        switch (GameManager.instance.stageNum)
        {
            case 1:
                enemName = enemNames1;
                ranSpawn = Random.Range(0, spawnPositions.Length - 4);
                ranEnem = Random.Range(0, enemNames1.Length);
                break;
            case 2:
                enemName = enemNames2;
                ranSpawn = Random.Range(0, spawnPositions.Length);
                ranEnem = Random.Range(0, enemNames2.Length);
                break;
        }

        GameObject dir = GameManager.instance.objMana.MakeObj(enemName[ranEnem]);
        dir.transform.position = spawnPositions[ranSpawn].position;

        Enemis enemLogic = dir.GetComponent<Enemis>();
        if(ranSpawn == 5 || ranSpawn == 6)
        {
            dir.transform.Rotate(Vector3.back * 90);
            dir.GetComponent<Rigidbody2D>().velocity = new Vector2(enemLogic.speed * -1, -1);
        }
        else if(ranSpawn == 7 || ranSpawn == 8)
        {
            dir.transform.Rotate(Vector3.forward * 90);
            dir.GetComponent<Rigidbody2D>().velocity = new Vector2(enemLogic.speed, -1);
        }
        else
        {
            dir.GetComponent<Rigidbody2D>().velocity = new Vector2(0, enemLogic.speed * -1); 
        }
    }
}
