using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonActive : Enemy
{
    protected Rigidbody2D rigidbodySkeleton;
    protected Collider2D collSkeleton;
    protected bool skeletonInjured;

    private GameObject healthPotion;

    public GameObject leftMovePos, rightMovePos;
    public static bool skeletonAttacking;

    protected override void Start()
    {
        base.Start();
        rigidbodySkeleton = GetComponent<Rigidbody2D>();
        collSkeleton = GetComponent<Collider2D>();
        animatorEnemy = GetComponent<Animator>();

        startTimeBtwAttack = 0.6f;

        healthPotion = Resources.Load<GameObject>("Prefabs/health_potion");
        propPrefab = healthPotion;

        Player = GameObject.Find("$Player");
    }

    private void Update()
    {

        if (!injured)
        {
            EnemyMovement();
            Attacking();
        }

        GoWhere();
        RunAnimation();
    }

    protected virtual void GoWhere()
    {
        if (Player != null)
            leftPosition = rightPosition = Player.transform.position.x;
    }

}
