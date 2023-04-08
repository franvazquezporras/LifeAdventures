using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlSystem : MonoBehaviour
{
    PlayerController windowsControlls;
    PlayerMoveJoystick movileControlls;
    AttackControl windowsAttack;
    PlayerAttackJoystick movileAttack;

    [SerializeField] Button attackButton;
    [SerializeField] Button jumpButton;
    [SerializeField] GameObject joystickButton;

    private void Awake()
    {
        windowsControlls = GetComponent<PlayerController>();
        windowsAttack = GetComponent<AttackControl>();
        movileControlls = GetComponent<PlayerMoveJoystick>();
        movileAttack = GetComponent<PlayerAttackJoystick>();
    }
    void Start()
    {
    #if UNITY_STANDALONE_WIN
        ChangePlataform(0);
    #elif UNITY_ANDROID
        ChangePlataform(1);
    #endif
    }

    public void ChangePlataform(int i)
    {
        i = 0;
        if(i == 0)
        {
            
            windowsControlls.enabled = true;
            windowsAttack.enabled = true;
            movileControlls.enabled = false;
            movileAttack.enabled = false;

            attackButton.gameObject.SetActive(false);
            jumpButton.gameObject.SetActive(false);
            joystickButton.SetActive(false);

        }
        else if(i == 1)
        {
            
            windowsControlls.enabled = false;
            windowsAttack.enabled = false;
            movileControlls.enabled = true;
            movileAttack.enabled = true;

            attackButton.gameObject.SetActive(true);
            jumpButton.gameObject.SetActive(true);
            joystickButton.SetActive(true);
        }
        
    }



}
