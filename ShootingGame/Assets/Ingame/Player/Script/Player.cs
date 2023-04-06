using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField] float speed;
    [SerializeField] public int power;
    public int hp;
    public float fuer;

    [Header("PlayerTransform0")]
    [SerializeField] Transform[] firePosition;
    [SerializeField] float curDelay;
    [SerializeField] float maxDelay;

    [Header("ETC")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer spri;
    [SerializeField] Sprite[] sprite;
    [SerializeField] Color onHitColor = new Color(1, 1, 1, 0.5f);
    [SerializeField] Color returnColor = new Color(1, 1, 1, 1);
    [SerializeField] bool isHit;
    [SerializeField] bool isMZ;
    public bool isDown;
    public bool isSheild;
    [SerializeField] GameObject shield;
    [SerializeField] public Skill skill;
    [SerializeField] Coroutine coru;
    [SerializeField] AudioClip hit;
    [SerializeField] AudioClip fire;
    [SerializeField]public AudioClip hpup;
    [SerializeField]public AudioSource sound;
    [SerializeField]public AudioSource pickup;
    [SerializeField] public bool isDash;

    private void Start()
    {
        isDash = false;
        InvokeRepeating("FuerDown", 1, 1);
    }

    void Update()
    {
        if(hp > 5)
        {
            hp = 5;
            GameManager.instance.HPSet();
        }
        if(fuer > 100)
        {
            fuer = 100;
        }
        Move();
        Shot();
        DashTime();
        
    }

    public float dashTime;
    void DashTime()
    {
        if(dashTime > 0)
        {
            dashTime -= Time.deltaTime;
            return;
        }
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("Dash");
            dashTime = 5;
        }
    }

    void Move()
    {
        if (!isDash)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            switch (h)
            {
                case -1:
                    spri.sprite = sprite[0];
                    break;
                case 0:
                    spri.sprite = sprite[1];
                    break;
                case 1:
                    spri.sprite = sprite[2];
                    break;
            }

            transform.position += new Vector3(h, v, 0) * speed * Time.deltaTime;
        }

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if(pos.x < 0) pos.x = 0;
        if (pos.x > 1) pos.x = 1;
        if (pos.y < 0) pos.y = 0;
        if (pos.y > 1) pos.y = 1;

        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    WaitForSeconds waitdot3 = new(0.3f);
    WaitForSeconds waitdot2 = new(0.2f);
    IEnumerator Dash()
    {
        isDash = true;
        isHit = true;
        gameObject.layer = 7;
        rigid.velocity = Vector3.up * 15;
        yield return waitdot2;
        rigid.velocity = Vector3.zero;
        isDash = false;
        gameObject.layer = 6;
        isHit = false;
    }

    void Shot()
    {
        if(curDelay < maxDelay)
        {
            curDelay += Time.deltaTime;
            return;
        }
        if (Input.GetKey(KeyCode.Z))
        {
            sound.clip = fire;
            sound.Play();
            switch (power)
            {
                case 1:
                    GameObject dirC = GameManager.instance.objMana.MakeObj("PlayerBullet1");
                    dirC.transform.position = firePosition[0].position;

                    dirC.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 13, ForceMode2D.Impulse);
                    break;
                case 2:
                    GameObject dirC2 = GameManager.instance.objMana.MakeObj("PlayerBullet2");
                    dirC2.transform.position = firePosition[0].position;

                    dirC2.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 13, ForceMode2D.Impulse);
                    break;
                case 3:
                    GameObject dirL2 = GameManager.instance.objMana.MakeObj("PlayerBullet1");
                    GameObject dirR2 = GameManager.instance.objMana.MakeObj("PlayerBullet1");
                    dirL2.transform.position = firePosition[2].position;
                    dirR2.transform.position = firePosition[1].position;

                    dirL2.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 13, ForceMode2D.Impulse);
                    dirR2.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 13, ForceMode2D.Impulse);
                    break;
                case 4:
                    GameObject dirL4 = GameManager.instance.objMana.MakeObj("PlayerBullet1");
                    GameObject dirC4 = GameManager.instance.objMana.MakeObj("PlayerBullet2");
                    GameObject dirR4 = GameManager.instance.objMana.MakeObj("PlayerBullet1");
                    dirL4.transform.position = firePosition[2].position;
                    dirC4.transform.position = firePosition[0].position;
                    dirR4.transform.position = firePosition[1].position;

                    dirL4.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 13, ForceMode2D.Impulse);
                    dirC4.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 13, ForceMode2D.Impulse);
                    dirR4.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 13, ForceMode2D.Impulse);
                    break;
            }

            curDelay = 0;
        }
    }

    void OnHit()
    {
        hp--;
        sound.clip = hit;
        sound.Play();
        GameManager.instance.camShake.TimeSeT(0.3f);
        StartCoroutine(OnHitEffect());
    }

    void FuerDown()
    {
        if (isDown)
        {
            fuer -= 2;
        }
    }

    WaitForSeconds wait1 = new(1);
    WaitForSeconds waitdot1 = new(0.1f);
    public IEnumerator OnHitEffect()
    {
        GameManager.instance.HPSet();
        spri.color = onHitColor;
        isHit = true;
        gameObject.layer = 7;
        yield return wait1;
        gameObject.layer = 6;
        isHit = false;
        spri.color = returnColor;
    }

    IEnumerator MZ()
    {
        spri.color = onHitColor;
        isHit = true;
        gameObject.layer = 7;
        isMZ = true;
        yield return wait1;
        spri.color = returnColor;
        yield return waitdot1;
        spri.color = onHitColor;
        yield return waitdot1;
        spri.color = returnColor;
        yield return waitdot1;
        spri.color = onHitColor;
        yield return waitdot1;
        spri.color = returnColor;
        yield return waitdot1;
        spri.color = returnColor;
        isHit = false;
        gameObject.layer = 6;
        isMZ = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isHit)
        {
            if (collision.gameObject.CompareTag("EnemBullet"))
            {
                OnHit();
                collision.gameObject.SetActive(false);
            }
            if (collision.gameObject.CompareTag("Enem"))
            {
                OnHit();
            }
        }

        if (collision.gameObject.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            pickup.Play();
            switch (item.itemName)
            {
                case "Fuer":
                    if(fuer >= 100)
                    {
                        GameManager.instance.pScore += 300;
                    }
                    else
                    {
                        fuer += 15;
                    }
                    break;
                case "Power":
                    if(power == 4)
                    {
                        GameManager.instance.pScore += 300;
                    }
                    else
                    {
                        power++;
                    }
                    break;
                case "HP":
                    if(hp == 5)
                    {
                        GameManager.instance.pScore += 300;
                    }
                    else
                    {
                        hp++;
                        GameManager.instance.HPSet();
                    }
                    break;
                case "MZ":
                    if (isMZ)
                    {
                        StopCoroutine(coru);
                        coru = StartCoroutine("MZ");
                    }
                    else
                    {
                        coru = StartCoroutine("MZ");
                    }
                    break;
                case "Shield":
                    if (isSheild)
                    {
                        GameManager.instance.pScore += 300;
                    }
                    else
                    {
                        shield.gameObject.SetActive(true);
                        isSheild = true;
                    }
                    break;
                case "Skill1":
                    if(skill.skillIndex1 == 3)
                    {
                        GameManager.instance.pScore += 300;
                    }
                    else
                    {
                        skill.skillIndex1++;
                    }
                    break;
                case "Skill2":
                    if (skill.skillIndex2 == 2)
                    {
                        GameManager.instance.pScore += 300;
                    }
                    else
                    {
                        skill.skillIndex2++;
                    }
                    break;
            }
            collision.gameObject.SetActive(false);
        }
    }
}
