using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] int health = 200;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float shipPadding = 0.5f;
    [SerializeField] float shipYMax = 0.6f;
    
    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 20f;
    [SerializeField] float firePauseRate = 0.05f;
    float xMin;
    float xMax;
    float yMin;
    float yMax;



    // Start is called before the first frame update
    void Start()
    {
        SetupMoveBoundaries();
    }

   

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();

    }

    private void Fire()
    {
        //if( Input.GetButtonDown("Fire1"))
        //{
        //    GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        //    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,laserSpeed);
        //}

        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine("FireContinuously");
                
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine("FireContinuously");

        }
    }

    IEnumerator FireContinuously ()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, laserSpeed);

            yield return new WaitForSeconds(firePauseRate);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

       
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
       
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void SetupMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + shipPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - shipPadding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + shipPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, shipYMax, 0)).y;
    }


    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp( transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp( transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }
}
