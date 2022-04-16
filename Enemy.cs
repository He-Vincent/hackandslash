using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  
    public GameObject playerObject;
    public float speed = 1;

    //HP
    public int health = 3;

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

        
    }

    // Update is called once per frame
    void Update()
    {

        
        // when the enemy is NOTs stunned
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

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "DamageZone")
        {
            //Debug.Log("ouch");
            // reduce hp when hit
            health -= playerObject.GetComponent<PlayerController>().attackPower;

            // refresh the stun timer
            stunUntilTime = Time.time + stunDuration;
        }
      
    }
}
