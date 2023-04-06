using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemis : MonoBehaviour
{
    [SerializeField] string enemName;

    [Header("Stat")]
    [SerializeField] float hp;
    [SerializeField] float score;
    public float speed;

    [SerializeField] float curDelay;
    [SerializeField] float maxDelay;

    [Header("ETC")]
    [SerializeField] SpriteRenderer spri;
    [SerializeField] Color onHitColor = new(1, 1, 1, 0.5f);
    [SerializeField] Color returnColor = new(1, 1, 1, 1);
    [SerializeField] Transform playerT;
    [SerializeField] string[] itemName;
    [SerializeField] AudioSource hitSound;

    private void Awake()
    {
        itemName = new string[] { "Fuer", "Fuer", "Fuer", "Power", "Fuer", "HP", "HP", "MZ", "Shield", "Skill1", "Skill2", "Shield", "Fuer", "Fuer", "Fuer" };
    }

    private void Start()
    {
        playerT = GameObject.Find("Player").transform;
    }

    private void OnEnable()
    {
        switch (enemName)
        {
            case "S":
                if(GameManager.instance != null)
                {
                    switch (GameManager.instance.stageNum)
                    {
                        case 1:
                            hp = 10;
                            score = 300;
                            speed = 3;
                            maxDelay = 1.7f;
                            break;
                        case 2:
                            hp = 15;
                            score = 500;
                            speed = 4;
                            maxDelay = 1.3f;
                            break;
                    }
                }
                break;
            case "M":
                if (GameManager.instance != null)
                {
                    switch (GameManager.instance.stageNum)
                    {
                        case 1:
                            hp = 14;
                            score = 500;
                            speed = 4;
                            maxDelay = 1.4f;
                            break;
                        case 2:
                            hp = 20;
                            score = 800;
                            speed = 2;
                            maxDelay = 1f;
                            break;
                    }
                }
                break;
            case "L":
                if (GameManager.instance != null)
                {
                    switch (GameManager.instance.stageNum)
                    {
                        case 1:
                            hp = 16;
                            score = 800;
                            speed = 1;
                            maxDelay = 1.9f;
                            break;
                        case 2:
                            hp = 25;
                            score = 1000;
                            speed = 1;
                            maxDelay = 1.3f;
                            break;
                    }
                }
                break;
            case "Y":
                if (GameManager.instance != null)
                {
                    switch (GameManager.instance.stageNum)
                    {
                        case 1:
                            hp = 10;
                            score = 800;
                            speed = 2;
                            maxDelay = 1.3f;
                            break;
                        case 2:
                            hp = 20;
                            score = 1000;
                            speed = 3;
                            maxDelay = 1f;
                            break;
                    }
                }
                break;
            case "Meteor1":
                if (GameManager.instance != null)
                {
                    switch (GameManager.instance.stageNum)
                    {
                        case 1:
                            hp = 5;
                            score = 300;
                            speed = 8;
                            break;
                        case 2:
                            hp = 10;
                            score = 500;
                            speed = 10;
                            break;
                    }
                }
                break;
            case "Meteor2":
                if(GameManager.instance != null)
                {
                    switch (GameManager.instance.stageNum)
                    {
                        case 1:
                            hp = 10;
                            score = 500;
                            speed = 10;
                            break;
                        case 2:
                            hp = 15;
                            score = 800;
                            speed = 15;
                            break;
                    }
                }
                break;
        }
    }

    private void Update()
    {
        Shot();   
    }
    void Shot()
    {
        if(curDelay < maxDelay)
        {
            curDelay += Time.deltaTime;
            return;
        }

        if(enemName == "S")
        {
            Vector3 vec = playerT.position - transform.position;
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet1");
            dir.transform.position = transform.position;
            dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            dir.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 7, ForceMode2D.Impulse);
        }
        else if (enemName == "M")
        {
            Vector3 vec = playerT.position - transform.position;
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet2");
            dir.transform.position = transform.position;
            dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            dir.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 10, ForceMode2D.Impulse);
        }
        else if (enemName == "L")
        {
            Vector3 vec = playerT.position - transform.position;
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet3");
            GameObject dir2 = GameManager.instance.objMana.MakeObj("EnemBullet3");
            dir.transform.position = transform.position + Vector3.right * 0.25f;
            dir2.transform.position = transform.position + Vector3.left * 0.25f;
            dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            dir2.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            dir.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 5, ForceMode2D.Impulse);
            dir2.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 5, ForceMode2D.Impulse);
        }
        else if(enemName == "Y")
        {
            CircleAndGo();
        }

        curDelay = 0;
    }

    void CircleAndGo()
    {
        List<Transform> bullets = new List<Transform>();

        for (int i = 0; i < 180; i += 90)
        {
            GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet4");

            dir.transform.position = transform.position;

            bullets.Add(dir.transform);

            dir.transform.rotation = Quaternion.Euler(0, 0, i);
        }

        StartCoroutine(go(bullets));
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
        hitSound.Play();
        OnHitEffect();

        if(hp < 1)
        {
            GameObject explode = GameManager.instance.objMana.MakeObj("Explode");
            explode.transform.position = transform.position;
            int ranItem = Random.Range(0, itemName.Length);
            GameObject item = GameManager.instance.objMana.MakeObj(itemName[ranItem]);
            item.transform.position = transform.position;
            transform.rotation = Quaternion.identity;
            GameManager.instance.pScore += score;
            gameObject.SetActive(false);
        }
    }

    void OnHitEffect()
    {
        spri.color = onHitColor;
        Invoke("ReturnColor", 0.3f);
    }

    void ReturnColor()
    {
        spri.color = returnColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Bullet bu = collision.GetComponent<Bullet>();
            OnHit(bu.dmg);
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Player"))
            OnHit(999);
        if (collision.gameObject.CompareTag("Border"))
        {
            transform.rotation = Quaternion.identity;
            gameObject.SetActive(false);
        }
    }
}
