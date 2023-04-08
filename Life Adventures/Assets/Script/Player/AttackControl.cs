using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControl : MonoBehaviour
{   
    private int player;
    private float coolDown = 0.3f;
    private float coolDownTimer = Mathf.Infinity;
    private PlayerController playerC;
    private Animator anim;
    [SerializeField] private Transform spawnFire;
    [SerializeField] private GameObject[] fireAttacks;
    [SerializeField] private GameObject[] dageAttacks;
    [SerializeField] private GameObject[] hachaAttacks;

    [Header ("Audios")]
    [SerializeField] private AudioClip magicSoundAttack;
    [SerializeField] private AudioClip dageSoundAttack;
    


    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerC = GetComponent<PlayerController>();
        player = PlayerPrefs.GetInt("tipoPersonaje");
    }
    private void Update()
    {
        if (coolDownTimer > coolDown && playerC.canAttack() && Input.GetMouseButton(0))
            BasicAttack();
        coolDownTimer += Time.deltaTime;
    }
    public void BasicAttack()
    {
        anim.SetTrigger("Basic");
        coolDownTimer = 0;

        if(player == 0)
        {
            SoundsManager.instance.PlaySound(magicSoundAttack);
            InvokeMagic();
        }else if(player == 1)
        {

            SoundsManager.instance.PlaySound(dageSoundAttack);
            InvokeHache();
        }
        else if(player == 2)
        {
            SoundsManager.instance.PlaySound(dageSoundAttack);
            InvokeDage();
        }       
    }

    private int findRangeAttack()
    {
        //pool attack
        if(player == 0)
        {
            for (int i = 0; i < fireAttacks.Length; i++)
            {
                if (!fireAttacks[i].activeInHierarchy)
                    return i;
            }
        }else if(player == 1)
        {
            for (int i = 0; i < hachaAttacks.Length; i++)
            {
                if (!hachaAttacks[i].activeInHierarchy)
                    return i;
            }
        }
        else if( player == 2)
        {
            for (int i = 0; i < dageAttacks.Length; i++)
            {
                if (!dageAttacks[i].activeInHierarchy)
                    return i;
            }
        }
        
        return 0;
    }


    private void InvokeMagic()
    {
        
        fireAttacks[findRangeAttack()].transform.position = spawnFire.position;
        fireAttacks[findRangeAttack()].GetComponent<MagicAttack>().setDirection(Mathf.Sign(transform.localScale.x));
    }

    private void InvokeDage()
    {

        dageAttacks[findRangeAttack()].transform.position = spawnFire.position;
        dageAttacks[findRangeAttack()].GetComponent<WeaponAttack>().setDirection(Mathf.Sign(transform.localScale.x));
    }
    private void InvokeHache()
    {

        hachaAttacks[findRangeAttack()].transform.position = spawnFire.position;
        hachaAttacks[findRangeAttack()].GetComponent<WeaponAttack>().setDirection(Mathf.Sign(transform.localScale.x));
    }
}
