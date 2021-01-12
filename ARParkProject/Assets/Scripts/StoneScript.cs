using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour
{
    Rigidbody stone;
    Vector3 vectorRight;
    Vector3 vectorLeft;

    Vector3 dir;
    float speed = 1000f;

    void Start()
    {
        stone = GetComponent<Rigidbody>();
        vectorRight = transform.right * 0.3f;
      //  vectorLeft = transform.right * -10f;

    }
    // Update is called once per frame
    void Update()
    {

        //  stone.AddForce(vectorRight, ForceMode.Impulse);

        
        dir = Vector3.zero;

        dir.x = Input.acceleration.x;

        if(dir.sqrMagnitude > 1)
        {
            dir.Normalize();
        }

        dir *= Time.deltaTime;

        stone.AddForce(dir * speed, ForceMode.Impulse);

        /*
        transform.Translate(dir * speed);
        */

    }


}
