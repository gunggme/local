using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("FalseObj", 0.5f);
    }

    void FalseObj()
    {
        gameObject.SetActive(false);
    }
}
