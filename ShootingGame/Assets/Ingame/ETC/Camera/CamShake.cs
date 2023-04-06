using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    //움직임 강도
    [SerializeField] float moveMag;

    [SerializeField] float moveTime;
    Vector3 initPosition;
    private void Awake()
    {
        initPosition = transform.position;
    }


    public void TimeSeT(float time)
    {
        moveTime = time;
    }

    private void Update()
    {
        if (moveTime > 0)
        {
            transform.position = Random.insideUnitSphere * moveMag + initPosition;
            moveTime -= Time.deltaTime;
        }
        else
        {
            transform.position = initPosition;
        }
    }
}
