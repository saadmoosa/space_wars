using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    //config params
    [SerializeField] int damage = 100;
    [SerializeField] GameObject particleEffect;

    public int Damage { get => damage; }

    public void Hit()
    {
        StartCoroutine(ParticleEffect());
        Destroy(gameObject);
    }

    IEnumerator ParticleEffect()
    {
        GameObject vfx = Instantiate(particleEffect, transform.position, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(1);
    }
}
