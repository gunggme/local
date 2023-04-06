using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed;
    [SerializeField] float range;

    [SerializeField] SpriteRenderer spri;
    [SerializeField] Sprite[] sprite;
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        if(transform.position.y < -range)
        {
            transform.position = target.position + Vector3.up * range;
        }

        if(sprite != null && GameManager.instance != null)
        {
            switch (GameManager.instance.stageNum)
            {
                case 1:
                    spri.sprite = sprite[0];
                    break;
                case 2:
                    spri.sprite = sprite[1];
                    break;
            }
        }
    }
}
