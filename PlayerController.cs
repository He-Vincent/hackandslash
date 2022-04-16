using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //variables for controlling player's speed
    public float walkSpeed = 1;
    public float runSpeed = 1000;

    public GameObject damageZonePrefab;
    // pause timer
    public float pauseDuration = 0.5f;
    float pauseUntilTime;

    public int health = 10;
    public int maxHealth = 10;

    public Image healthImage;

    public int attackPower = 10;

    public GameObject deathScreen;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("This is Start!");

        deathScreen.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //TODO: get player movement in all 4 directions

        //get the user's input for x and y movement
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(inputX, inputY, 0);

        movement = movement * Time.deltaTime;

        //TODO: Turn the character left or right depending on inputX
        // inputX = -1 when moving left
        //inputX = 1 when moving right


     


        if (inputX < 0)
        {
            
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (inputX > 0)
        {
            
            transform.localScale = new Vector3(-1, 1, 1);
        }


        float hitZoneScaleX = transform.localScale.x;



        //TODO: make it so that when the player is holding down shift, they move faster


        // using Input.GetKey to see if player is pressing left shift
        if (Input.GetKey(KeyCode.LeftShift))
        {        
            movement = movement * runSpeed;
            //Debug.Log("Left shift is pressed!");
        }
        else
        {
            movement = movement * walkSpeed;
        }


        //apply the movement to the player object
        if (Time.time > pauseUntilTime)
        {
            transform.Translate(movement);
        }


        // spawn a damage zone when the player left clicks
        if (Input.GetMouseButtonDown(0))
        {

          

            GameObject hitZoneObj = Instantiate(damageZonePrefab, transform.position, Quaternion.identity);
            hitZoneObj.transform.localScale = new Vector3(hitZoneScaleX, 1, 1);


            GetComponent<Animator>().SetTrigger("attack");

            pauseUntilTime = Time.time + pauseDuration;

           

        }

        if (inputX != 0 || inputY != 0)
        {
            //GetComponent<Animator>().Play("Red Hood Run");
            GetComponent<Animator>().SetBool("run", true);
        }
        else 
        {

            //GetComponent<Animator>().Play("player idle");
            GetComponent<Animator>().SetBool("run", false);
        }

        float fillAmount = (float) health / maxHealth;
        healthImage.fillAmount = fillAmount;

        if (health <= 0)
        {
            deathScreen.SetActive(true);
            gameObject.SetActive(false);
        }

        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "HealthBottle")
        {
            health += 5;
            if (health > maxHealth)
            {
                health = maxHealth;
            }

            Debug.Log("Health: " + health);
            Destroy(collision.gameObject);
            
        }

        else if (collision.tag == "AttackPowerBottle")
        {
            attackPower += 5;

            Debug.Log("attackPower: " + attackPower);
            Destroy(collision.gameObject);
        }

        else if (collision.tag == "Projectile")
        {
            health -= 4;
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            health -= 2;
            //Debug.Log(health);
            //Debug.Log("ouch the player is hit by skeletons and need help");
        }
       

    }


}
