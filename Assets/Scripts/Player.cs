using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //config params
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.3f;
    [SerializeField] float health = 100f;

    [Header("Projectile")]
    [SerializeField] GameObject playerLaser;
    [SerializeField] float projectileSpeed =10f;
    [SerializeField] float projectileFiringPeriod = 5f;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true){
            GameObject laser = Instantiate(playerLaser, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }   
    }
    private void Move()
    {
        //get the movement of the horizontal axis and make it framerate independent
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime *moveSpeed;
        float newXPos = transform.position.x + deltaX;

        //get the movement of the vertical axis and make it framerate independent
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        float newYPos = transform.position.y + deltaY;

        //move the new vector

        transform.position = new Vector2(Mathf.Clamp(newXPos, xMin, xMax), Mathf.Clamp(newYPos, yMin, yMax));
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        //horizontal boundaries for player movement
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        //vertical boundaries for player movement
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
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
