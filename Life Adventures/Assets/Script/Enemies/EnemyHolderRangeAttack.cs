using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHolderRangeAttack : MonoBehaviour
{

    [SerializeField] private Transform enemy;
    void Update()
    {
        transform.localScale = enemy.localScale/2;
    }
}
