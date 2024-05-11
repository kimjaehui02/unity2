using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public List<Animator> listAnimatorOfPlayer;
    public List<Animator> listAnimatorOfShadow;

    public Color GetColor;


    //public List<Sprite> listSpriteOfPlayerIdle;

    public List<GameObject> ListGameObjectsOfEffects;


    public List<AudioClip> soundClips;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayerIdleAnimation()
    {
        for (int i = 0; i < 5; i++)
        {

            if (listAnimatorOfPlayer[i].GetCurrentAnimatorStateInfo(0).IsName("MoveSide"))
            {
                listAnimatorOfPlayer[i].SetTrigger("IdleSide");

            }
            if (listAnimatorOfPlayer[i].GetCurrentAnimatorStateInfo(0).IsName("MoveUp"))
            {
                listAnimatorOfPlayer[i].SetTrigger("IdleUp");

            }
            if (listAnimatorOfPlayer[i].GetCurrentAnimatorStateInfo(0).IsName("MoveDown"))
            {
                listAnimatorOfPlayer[i].SetTrigger("IdleDown");

            }

        }
        for(int shadow = 0;shadow<5;shadow++)
        {
            if (listAnimatorOfShadow[shadow].GetCurrentAnimatorStateInfo(0).IsName("MoveSide"))
            {
                listAnimatorOfShadow[shadow].SetTrigger("IdleSide");

            }
            if (listAnimatorOfShadow[shadow].GetCurrentAnimatorStateInfo(0).IsName("MoveUp"))
            {
                listAnimatorOfShadow[shadow].SetTrigger("IdleUp");

            }
            if (listAnimatorOfShadow[shadow].GetCurrentAnimatorStateInfo(0).IsName("MoveDown"))
            {
                listAnimatorOfShadow[shadow].SetTrigger("IdleDown");

            }
        }
    }

    public void PlayerMoveAnimation(float h =0, float v =0)
    {
        float roundedValue = Mathf.Round(h) + (Mathf.Round(v)*3) + 5;
        switch (roundedValue)
        {
            case 6:
                for (int i = 0; i < 5; i++)
                {
                    listAnimatorOfPlayer[i].GetComponent<SpriteRenderer>().flipX = false;
                    if (!listAnimatorOfPlayer[i].GetCurrentAnimatorStateInfo(0).IsName("MoveSide"))
                    {
                        listAnimatorOfPlayer[i].SetTrigger("MoveSide");
                        
                    }

                    
                }
                for(int shadow = 0;shadow<5;shadow++)
                {
                    listAnimatorOfShadow[shadow].GetComponent<SpriteRenderer>().flipX = false;
                    if (!listAnimatorOfShadow[shadow].GetCurrentAnimatorStateInfo(0).IsName("MoveSide"))
                    {
                        listAnimatorOfShadow[shadow].SetTrigger("MoveSide");

                    }
                }
                break;
            case 4:
                for (int i = 0; i < 5; i++)
                {
                    listAnimatorOfPlayer[i].GetComponent<SpriteRenderer>().flipX = true;
                    if (!listAnimatorOfPlayer[i].GetCurrentAnimatorStateInfo(0).IsName("MoveSide"))
                    {
                        listAnimatorOfPlayer[i].SetTrigger("MoveSide");
                        
                    }
                }
                for (int shadow = 0; shadow < 5; shadow++)
                {
                    listAnimatorOfShadow[shadow].GetComponent<SpriteRenderer>().flipX = true;
                    if (!listAnimatorOfShadow[shadow].GetCurrentAnimatorStateInfo(0).IsName("MoveSide"))
                    {
                        listAnimatorOfShadow[shadow].SetTrigger("MoveSide");

                    }
                }
                break;
            case 8:
                for (int i = 0; i < 5; i++)
                {
                    if (!listAnimatorOfPlayer[i].GetCurrentAnimatorStateInfo(0).IsName("MoveUp"))
                    {
                        listAnimatorOfPlayer[i].SetTrigger("MoveUp");
                        listAnimatorOfPlayer[i].GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
                for (int shadow = 0; shadow < 5; shadow++)
                {
                    if (!listAnimatorOfShadow[shadow].GetCurrentAnimatorStateInfo(0).IsName("MoveUp"))
                    {
                        listAnimatorOfShadow[shadow].SetTrigger("MoveUp");
                        listAnimatorOfShadow[shadow].GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
                break;
            case 2:
                for (int i = 0; i < 5; i++)
                {
                    if (!listAnimatorOfPlayer[i].GetCurrentAnimatorStateInfo(0).IsName("MoveDown"))
                    {
                        listAnimatorOfPlayer[i].SetTrigger("MoveDown");
                        listAnimatorOfPlayer[i].GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
                for (int shadow = 0; shadow < 5; shadow++)
                {
                    if (!listAnimatorOfShadow[shadow].GetCurrentAnimatorStateInfo(0).IsName("MoveDown"))
                    {
                        listAnimatorOfShadow[shadow].SetTrigger("MoveDown");
                        listAnimatorOfShadow[shadow].GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
                break;
        }
    }

    public void PlayerDashAnimation()
    {
        for (int i = 0; i < 5; i++)
        {

            if (listAnimatorOfPlayer[i].GetCurrentAnimatorStateInfo(0).IsName("MoveSide"))
            {
                listAnimatorOfPlayer[i].SetTrigger("IdleSide");

            }
            if (listAnimatorOfPlayer[i].GetCurrentAnimatorStateInfo(0).IsName("MoveUp"))
            {
                listAnimatorOfPlayer[i].SetTrigger("IdleUp");

            }
            if (listAnimatorOfPlayer[i].GetCurrentAnimatorStateInfo(0).IsName("MoveDown"))
            {
                listAnimatorOfPlayer[i].SetTrigger("IdleDown");

            }

        }
        for (int shadow = 0; shadow < 5; shadow++)
        {

            if (listAnimatorOfShadow[shadow].GetCurrentAnimatorStateInfo(0).IsName("MoveSide"))
            {
                listAnimatorOfShadow[shadow].SetTrigger("IdleSide");

            }
            if (listAnimatorOfShadow[shadow].GetCurrentAnimatorStateInfo(0).IsName("MoveUp"))
            {
                listAnimatorOfShadow[shadow].SetTrigger("IdleUp");

            }
            if (listAnimatorOfShadow[shadow].GetCurrentAnimatorStateInfo(0).IsName("MoveDown"))
            {
                listAnimatorOfShadow[shadow].SetTrigger("IdleDown");

            }

        }
        PlaySoundByIndex(2);
    }

    public void PlayerAttackAnimation()
    {
        for (int i = 0; i < 5; i++)
        {

            if (listAnimatorOfPlayer[i].GetCurrentAnimatorStateInfo(0).IsName("MoveSide") || listAnimatorOfPlayer[i].GetCurrentAnimatorStateInfo(0).IsName("IdleSide"))
            {
                listAnimatorOfPlayer[i].SetTrigger("AttackSide");
                listAnimatorOfShadow[i].SetTrigger("AttackSide");

            }

            if (listAnimatorOfPlayer[i].GetCurrentAnimatorStateInfo(0).IsName("MoveUp") || listAnimatorOfPlayer[i].GetCurrentAnimatorStateInfo(0).IsName("IdleUp"))
            {
                listAnimatorOfPlayer[i].SetTrigger("AttackUp");
                listAnimatorOfShadow[i].SetTrigger("AttackUp");

            }

            if (listAnimatorOfPlayer[i].GetCurrentAnimatorStateInfo(0).IsName("MoveDown") || listAnimatorOfPlayer[i].GetCurrentAnimatorStateInfo(0).IsName("IdleDown"))
            {
                listAnimatorOfPlayer[i].SetTrigger("AttackDown");
                listAnimatorOfShadow[i].SetTrigger("AttackDown");

            }

        }
    }

    public void PlayerParryAnimation()
    {

    }

    
    // 인덱스를 받아 해당 사운드를 재생하는 함수
    public void PlaySoundByIndex(int soundIndex)
    {
        if (soundIndex >= 0 && soundIndex < soundClips.Count)
        {
            AudioClip selectedSound = soundClips[soundIndex];

            if (selectedSound != null)
            {
                audioSource.clip = selectedSound;
                audioSource.PlayOneShot(selectedSound);
            }
        }
    }


    public IEnumerator Hited()
    {

        listAnimatorOfPlayer[0].GetComponent<SpriteRenderer>().color = Color.red;

        PlaySoundByIndex(0);
        Instantiate(ListGameObjectsOfEffects[0], gameObject.transform.position, gameObject.transform.rotation);

        // 대기 시간 설정 (예: 2초)
        float waitTime = 2.0f;
        yield return new WaitForSeconds(waitTime);

        listAnimatorOfPlayer[0].GetComponent<SpriteRenderer>().color = Color.white;
    }
    
    public IEnumerator Hited(GameObject game)
    {
        if(game.GetComponent<SpriteRenderer>() == null)
        {
            yield break;
        }


        game.GetComponent<SpriteRenderer>().color = Color.red;

        PlaySoundByIndex(0);
        Instantiate(ListGameObjectsOfEffects[0], game.transform.position, game.transform.rotation);

        // 대기 시간 설정 (예: 2초)
        float waitTime = 2.0f;
        yield return new WaitForSeconds(waitTime);

        game.GetComponent<SpriteRenderer>().color = GetColor;
    }

    public IEnumerator Parried()
    {
        listAnimatorOfPlayer[0].GetComponent<SpriteRenderer>().color = Color.yellow;

        PlaySoundByIndex(1);
        Instantiate(ListGameObjectsOfEffects[1], gameObject.transform.position, gameObject.transform.rotation);

        // 대기 시간 설정 (예: 2초)
        float waitTime = 2.0f;
        yield return new WaitForSeconds(waitTime);

        listAnimatorOfPlayer[0].GetComponent<SpriteRenderer>().color = Color.white;
    }
}
