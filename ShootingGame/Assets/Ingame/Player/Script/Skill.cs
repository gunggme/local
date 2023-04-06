using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    [Header("Skill1")]
    public int skillIndex1;
    public float curSkillDelay1;
    [SerializeField] float maxSkillDelay1;
    [SerializeField] Slider skill1;
    [SerializeField] Text indexText;
    [SerializeField] Text dontPlay;
    [SerializeField] Text coolText;
    
    [Header("Skill2")]
    public int skillIndex2;
    public float curSkillDelay2;
    [SerializeField] float maxSkillDelay2;
    [SerializeField] Slider skill2;
    [SerializeField] Text indexText2;
    [SerializeField] Text dontPlay2;
    [SerializeField] Text coolText2;
    [SerializeField] GameObject boom;

    private void Update()
    {
        Skill1();
        Skill2( );
    }

    void Skill1()
    {
        coolText.text = Mathf.Round((curSkillDelay1 * 10)/ 10).ToString();
        indexText.text = skillIndex1.ToString();
        skill1.value = curSkillDelay1 / maxSkillDelay1;
        if(skillIndex1 < 1)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine("TextDown1");
            }
            return;
        }
        else
        {
            if(curSkillDelay1 > 0)
            {
                curSkillDelay1 -= Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartCoroutine("TextDown1");
                }
                return;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                GameManager.instance.player.sound.clip = GameManager.instance.player.hpup;
                GameManager.instance.player.sound.Play();
                GameManager.instance.player.hp += 2;
                GameManager.instance.HPSet();
                skillIndex1--;
                curSkillDelay1 = maxSkillDelay1;
            }
        }
    }

    WaitForSeconds waitdot3 = new(0.3f);
    IEnumerator TextDown1()
    {
        dontPlay.gameObject.SetActive(true);
        yield return waitdot3;
        dontPlay.gameObject.SetActive(false);
    }
    void Skill2()
    {
        coolText2.text = Mathf.Round((curSkillDelay2 * 10) / 10).ToString();
        indexText2.text = skillIndex2.ToString();
        skill2.value = curSkillDelay2 / maxSkillDelay2;
        if (skillIndex2 < 1)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine("TextDown2");
            }
            return;
        }
        else
        {
            if (curSkillDelay2 > 0)
            {
                curSkillDelay2 -= Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.R))
                {
                    StartCoroutine("TextDown2");
                }
                return;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                GameManager.instance.camShake.TimeSeT(0.4f);
                boom.SetActive(true);
                skillIndex2--;
                curSkillDelay2 = maxSkillDelay2;
            }
        }
    }
    IEnumerator TextDown2()
    {
        dontPlay2.gameObject.SetActive(true);
        yield return waitdot3;
        dontPlay2.gameObject.SetActive(false);
    }
}
