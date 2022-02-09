using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyActive : MonoBehaviour
{
    private GameObject prefabImpactEffect;
    private AudioSource hitSound;
    private Animator animDummy;

    public GameObject bloodBar;
    public float currentHP, maxHP;
    public GameObject boundNextLevel;

    private void Start()
    {
        prefabImpactEffect = Resources.Load<GameObject>("Prefabs/ImpactEffect");
        hitSound = GetComponent<AudioSource>();
        animDummy = GetComponent<Animator>();
    }

    private void Update()
    {
        DummyHealth();
    }

    public void DummyInjured(int valDamage)
    {
        currentHP -= valDamage;
        GameObject impactEffect = Instantiate(prefabImpactEffect, transform.position, Quaternion.identity) as GameObject;
        hitSound.Play();
        Destroy(impactEffect, 0.12f);

        if (currentHP <= 0)
        {
            animDummy.Play("dummy_fall");
            Invoke("DummyReset", 1f);
            boundNextLevel.GetComponent<Collider2D>().isTrigger = true;
        }
    }

    private void DummyHealth()
    {
        bloodBar.transform.localScale = new Vector3(currentHP / maxHP, 0.1f, 1);
    }

    private void DummyReset()
    {
        currentHP = maxHP;
        animDummy.Play("dummy_idle");
    }
}
