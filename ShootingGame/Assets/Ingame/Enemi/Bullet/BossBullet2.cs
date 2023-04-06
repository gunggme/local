using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border2"))
        {
            transform.localScale = new Vector3(1, 1, 1);
            gameObject.SetActive(false);
        }
    }
}
