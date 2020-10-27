using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //config param
    [SerializeField] float health = 100;
    [SerializeField] float shotCounter; //serialized for debug purposes
    [SerializeField] float minTimeBetweenShots = 2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject enemyLaser;
    [SerializeField] float projectileSpeed = 10f;

    private void Start()
    {
        //instantiate shot countdown
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        //slowly decrease the shotdown counter and then shoot
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        //create a laser and shoot it
        GameObject laser = Instantiate(enemyLaser, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D otherThing)
    {
        DamageDealer damageDealer = otherThing.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }
        ProcessHit(damageDealer);

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.Damage;
        damageDealer.Hit();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
