using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    [SerializeField] float dmg;
    [SerializeField] AudioSource boomAudio;

    private void OnEnable()
    { 
        if(boomAudio != null)
        {
            boomAudio.Play();
        }
        Enemis[] enemis = FindObjectsOfType<Enemis>();
        foreach(Enemis enem in enemis)
        {
            enem.OnHit(dmg);
        }
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemBullet");
        foreach(GameObject bullet in bullets)
        {
            bullet.gameObject.SetActive(false);
        }

        Invoke("FalseObj", 0.4f);
    }

    void FalseObj()
    {
        gameObject.SetActive(false);
    }
}
