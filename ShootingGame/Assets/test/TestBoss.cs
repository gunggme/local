using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoss : MonoBehaviour
{
    [SerializeField] float spinSpeed;
    [SerializeField] float time;
    [SerializeField] GameObject bullet;
    [SerializeField] float spinTimer;
    [SerializeField] float spinTime = 0.2f;
 
    [SerializeField] Transform target;

    [SerializeField] Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Summon();
    }

    void Summon()
    {
        Vector3 vec = target.position - transform.position;
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        spinTimer += Time.deltaTime;
        if (spinTimer < spinTime)
        {
            Invoke(nameof(Summon), 0);
            return;
        }
        spinTimer = 0;

        GameObject dir = Instantiate(bullet);
        //GameObject dir2 = Instantiate(bullet);
        dir.transform.position = transform.position;
       // dir2.transform.position = transform.position;

        dir.transform.rotation = Quaternion.Euler(0, 0,  Random.Range(transform.rotation.z, transform.rotation.z - 180));
        // dir2.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z * 90);

        Invoke(nameof(Summon), 0f);
    }

    WaitForSeconds waitDot5 = new(0.5f);

    IEnumerator go(IList<Transform> objects)
    {
        yield return waitDot5;
        for (int i = 0; i < objects.Count; i++)
        {
            Vector3 targetPosition = target.position - transform.position;
            float angle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;

            objects[i].transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        objects.Clear();
    }
}
