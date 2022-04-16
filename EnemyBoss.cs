using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public GameObject playerObject;

    public float shootInterval;
    float shootTime;

    public GameObject projectilePrefab;

    public int health = 100;
    public float speed = 1;


    float stunDuration = 0.5f;
    float stunUntilTime;
    public Color damageColour;
    Color defaultColour;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColour = spriteRenderer.color;
        shootTime = Time.time + shootInterval;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time >= stunUntilTime)
        {
            // move the enemy towards the player object
            //playerObject.transform.position;
            Vector3 newPosition = Vector3.MoveTowards(transform.position, playerObject.transform.position, 1 * Time.deltaTime * speed);
            transform.position = newPosition;
            spriteRenderer.color = defaultColour;
        }
        // when the enemy IS  stunned
        else
        {
            spriteRenderer.color = damageColour;
        }


        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (Time.time > shootTime)
        {
            //Debug.Log("pew pew");

            GameObject p = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Vector3 pVelocity = playerObject.transform.position - transform.position;
            p.GetComponent<Projectile>().velocity = pVelocity.normalized * 5;

            if (health < 50)
            {
                shootTime += shootInterval / 2;
            }
            else
            {
                shootTime += shootInterval;
            }
        }
    }



    //    //Shoot a projectile at the player
    //    if (Time.time > shootTime)
    //    {
    //        Debug.Log("pew pew");

    //        GameObject p = Instantiate(projectilePrefab);
    //        p.GetComponent<Projectile>().velocity = 
    //            playerObject.transform.position - transform.position;

    //        shootTime += shootInterval;
    //    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DamageZone")
        {
            //Debug.Log("ouch");
            // reduce hp when hit
            health -= playerObject.GetComponent<PlayerController>().attackPower;

            // refresh the stun timer
            stunUntilTime = Time.time + stunDuration;
        }

    }


}

