using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Boss : MonoBehaviour
{
    bool isMove = true;

    [Header("Stat")]
    [SerializeField] public float hp;
    [SerializeField] Transform[] firePosition;

    [Header("Skill")]
    [SerializeField] int skillIndex;
    [SerializeField] int curSkillStack;
    [SerializeField] int maxSkillStack;

    [Header("Spin")]
    [SerializeField] float spinSpeed;
    [SerializeField] float spinTimer;
    [SerializeField] float spinTime;

    [Header("ETC")]
    [SerializeField] SpriteRenderer[] sprits;
    [SerializeField] Transform playerT;
    [SerializeField] Color onHitColor = new Color(1, 1, 1, 0.5f);
    [SerializeField] Color returnColor = new Color(1, 1, 1, 1);
    [SerializeField] AudioSource hitSound;

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            transform.position += Vector3.down * 3 * Time.deltaTime;
            GameManager.instance.camShake.TimeSeT(0.1f);
            if (transform.position.y < 2)
            {
                isMove = false;
                Invoke("Think", 1);
            }
        }
    }

    void Think()
    {
        skillIndex = Random.Range(0, 16);
        curSkillStack = 0;
        maxSkillStack = Random.Range(4, 7);

        switch (skillIndex)
        {
            case 0:
                Invoke("SpawnOn", 1);
                break;
            case 1:
                Invoke("PlayerShot", 1);
                break;
            case 2:
                Invoke("CircleShot", 1);
                break;
            case 3:
                Invoke("ArkShot", 1);
                break;
            case 4:
                Invoke("SpawnOn", 1);
                break;
            case 5:
                Invoke("AroundShot", 1);
                break;
            case 6:
                Invoke("SpinShot", 1);
                break;
            case 7:
                Invoke("CircleAndGo", 1);
                break;
            case 8:
                Invoke("SpawnOn", 1);
                break;
            case 9:
                Invoke("SpawnOn", 1);
                break;
            case 10:
                Invoke("SpawnOn", 1);
                break;
            case 11:
                Invoke("AroundShot", 1);
                break;
            case 12:
                Invoke("AroundShot", 1);
                break;
            case 13:
                Invoke("CircleAndGo", 1);
                break;
            case 14:
                Invoke("SpinShot", 1);
                break;
            case 15:
                Invoke("SpinShot", 1);
                break;
            case 16:

                Think();
                break;
        }
    }
    int a = 1;
    void PlayerShot()
    {
        Vector3 vec = playerT.position - transform.position;
        GameObject dir1 = GameManager.instance.objMana.MakeObj("EnemBullet2");
        GameObject dir2 = GameManager.instance.objMana.MakeObj("EnemBullet2");
        GameObject dir3 = GameManager.instance.objMana.MakeObj("EnemBullet2");
        GameObject dir4 = GameManager.instance.objMana.MakeObj("EnemBullet2");
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        dir1.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        dir2.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        dir3.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        dir4.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        dir1.transform.position = firePosition[0].position + Vector3.right * 0.5f;
        dir2.transform.position = firePosition[0].position + Vector3.right * 0.25f;
        dir3.transform.position = firePosition[0].position + Vector3.left * 0.25f;
        dir4.transform.position = firePosition[0].position + Vector3.left * 0.5f;

        dir1.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 10, ForceMode2D.Impulse);
        dir2.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 10, ForceMode2D.Impulse);
        dir3.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 10, ForceMode2D.Impulse);
        dir4.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 10, ForceMode2D.Impulse);

        curSkillStack++;
        if (curSkillStack < maxSkillStack)
        {
            Invoke("PlayerShot", 0.3f);
        }
        else
        {
            Invoke("Think", 1);
        }
    }

    void CircleShot()
    {
        int roundA = 30;
        int roundB = 25;
        int round = curSkillStack % 2 == 0 ? roundA : roundB;
        for (int i = 0; i < round; i++)
        {
            Vector3 vec = new Vector2(Mathf.Sin(Mathf.PI * 2 * i / round), Mathf.Cos(Mathf.PI * 2 * i / round));
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;

            GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet3");
            dir.transform.position = firePosition[0].position;
            dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            dir.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 7, ForceMode2D.Impulse);
        }

        curSkillStack++;
        if (curSkillStack < maxSkillStack)
        {
            Invoke("CircleShot", 0.5f);
        }
        else
        {
            Invoke("Think", 1);
        }
    }

    void AroundShot()
    {
        for (int i = 180; i < 360; i += 13)
        {
            GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet4");

            dir.transform.position = transform.position;

            dir.transform.rotation = Quaternion.Euler(0, 0, i);
        }

        curSkillStack++;
        if (curSkillStack < maxSkillStack)
        {
            Invoke("AroundShot", 0.2f);
        }
        else
        {
            Invoke("Think", 1);
        }
    }


    void ArkShot()
    {
        Vector3 vec = playerT.position - transform.position;
        GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet1");
        a = a == 1 ? 2 : 1; 
        dir.transform.position = firePosition[a].position;
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        curSkillStack++;
        if (curSkillStack < maxSkillStack)
        {
            Invoke("ArkShot", 0.2f);
        }
        else
        {
            Invoke("Think", 1);
        }
    }

    void SpawnOn()
    {
        CancelInvoke("SpawnOff");
        GameManager.instance.spawnMana.isSpawn = true;
        Invoke("SpawnOff", 15f);
        Think();
    }   

    void SpawnOff()
    {
        GameManager.instance.spawnMana.isSpawn = false;
    }

    void SpinShot()
    {
        firePosition[0].transform.Rotate(Vector3.forward * (spinSpeed * 100 * Time.deltaTime), Space.Self);

        spinTimer += Time.deltaTime;
        if (spinTimer < spinTime)
        {
            Invoke("SpinShot", 0f);
            return;
        }
            spinTimer = 0;

        GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet4");
        dir.transform.position = firePosition[0].position;

        dir.transform.rotation = firePosition[0].rotation;

        curSkillStack++;
        if(curSkillStack < 72)
        {
            Invoke("SpinShot", 0f);
        }
        else
        {
            Think();
        }
    }

    void SpinRandom()
    {
        transform.transform.Rotate(Vector3.forward * (spinSpeed * 100 * Time.deltaTime));

        spinTimer += Time.deltaTime;
        if (spinTimer < spinTime)
        {
            Invoke("SpinRandom", 0);
            return;
        }
        spinTimer = 0;

        GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet4");
        GameObject dir2 = GameManager.instance.objMana.MakeObj("EnemBullet4");
        dir.transform.position = transform.position;
        dir2.transform.position = transform.position;

        dir.transform.rotation = transform.rotation;
        dir2.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z * 90);
        curSkillStack++;
        if (curSkillStack < 72)
        {
            Invoke("SpinRandom", 0);
        }
        else
        {
            Think();
        }
    }

    void CircleAndGo()
    {
        List<Transform> bullets = new List<Transform>();

        for (int i = 0; i < 360; i += 13)
        {
            GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet4");

            dir.transform.position = transform.position;

            bullets.Add(dir.transform);

            dir.transform.rotation = Quaternion.Euler(0, 0, i);
        }

        StartCoroutine(go(bullets));

        curSkillStack++;
        if(curSkillStack < maxSkillStack)
        {
            Invoke("CircleAndGo", 1);
        }
        else
        {
            Invoke("Think", 1);
        }
    }

    WaitForSeconds waitDot5 = new(0.5f);

    IEnumerator go(IList<Transform> objects)
    {
        yield return waitDot5;
        for (int i = 0; i < objects.Count; i++)
        {
            Vector3 targetPosition = playerT.position - objects[i].position;
            float angle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;

            objects[i].transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        objects.Clear();
    }

    public void OnHit(float dmg)
    {
        hp -= dmg;

        OnHitEffect();
        hitSound.Play();

        if (hp < 1)
        {
            GameManager.instance.camShake.TimeSeT(1f);
            CancelInvoke();
            GameManager.instance.pScore += 100000;
            gameObject.SetActive(false);
        }
    }

    void OnHitEffect()
    {
        foreach (SpriteRenderer spri in sprits)
        {
            spri.color = onHitColor;
        }
        Invoke("ReturnColor", 0.3f);
    }

    void ReturnColor()
    {
        foreach (SpriteRenderer spri in sprits)
        {
            spri.color = returnColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Bullet bu = collision.GetComponent<Bullet>();
            OnHit(bu.dmg);
            collision.gameObject.SetActive(false);
        }
    }
}
