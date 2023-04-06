using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnEnable()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enem"))
        {
            collision.gameObject.SetActive(false);
            GameManager.instance.player.isSheild = false;
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("EnemBullet"))
        {
            GameManager.instance.player.isSheild = false;
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            GameManager.instance.player.isSheild = false;
            gameObject.SetActive(false);
        }
            
    }
}
