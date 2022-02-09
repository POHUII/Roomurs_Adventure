using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHeavyHit : MonoBehaviour
{
    public int valHeavyDamage;

    private void Start()
    {
        Destroy(gameObject, 8f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dummy"))
        {
            print("Dummy");
            other.GetComponent<DummyActive>().DummyInjured(valHeavyDamage);
        }

        if (other.CompareTag("Skeleton"))
        {
            other.GetComponent<SkeletonActive>().EnemyInjured(valHeavyDamage);
            InitEnemy.enemyRest--;
        }

        if (other.CompareTag("Archer"))
        {
            other.GetComponent<ArcherController>().DropHealthPotion();
            other.GetComponent<ArcherController>().EnemyInjured(valHeavyDamage);
        }
        if (other.CompareTag("BossSkeleton"))
        {
            other.GetComponent<BossSkeletonActive>().Injured(valHeavyDamage);
        }
        if (other.CompareTag("Spider"))
        {
            other.GetComponent<SkeletonActive>().EnemyInjured(valHeavyDamage);
            Invoke("Soliloquize", 1.2f);
        }
        if (other.CompareTag("Bat"))
        {
            other.GetComponent<BatActive>().Injured(valHeavyDamage);
        }
        if (other.CompareTag("Syris"))
        {
            other.GetComponent<SyrisController>().SyrisDead();
            EndingDesision.isBadEnding = true;
        }
    }
}

