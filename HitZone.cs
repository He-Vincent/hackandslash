using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZone : MonoBehaviour
{
    public float lastTime = 0.1f;
    float deathTime;

    // Start is called before the first frame update
    void Awake()
    {
        deathTime = Time.time + lastTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= deathTime)
        {
            // die
            //Debug.Log("hitZone dies");
            Destroy(gameObject);

        }
       
    }
}
