using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] private Transform right;
    [SerializeField] private Transform left;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Parameters")]
    [SerializeField] private float speed;
    private Vector3 initPosition;
    private bool movingLeft;
    [SerializeField] private float waitDuration;
    private float waitTimer;

    [Header("Animations")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        initPosition = enemy.localScale;
    }
    private void OnDisable()
    {
        anim.SetBool("waiting", true);
    }
    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x <= right.position.x)
                MoveDirection(1);
            else
                ChangeDirection();
        }            
        else
        {
            if (enemy.position.x >= left.position.x)
                MoveDirection(-1);
            else
                ChangeDirection();
        }           
        
    }

    private void ChangeDirection()
    {
        anim.SetBool("waiting", true);
        waitTimer += Time.deltaTime;

        if(waitTimer>waitDuration)
            movingLeft = !movingLeft;
    }

    private void MoveDirection(int direction)
    {
        waitTimer = 0;
        anim.SetBool("waiting", false);
        enemy.localScale = new Vector3(Mathf.Abs(initPosition.x) * direction, initPosition.y, initPosition.z);
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y, enemy.position.z);
    }
}
