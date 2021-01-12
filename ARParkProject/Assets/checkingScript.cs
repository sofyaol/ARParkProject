using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class checkingScript : MonoBehaviour
{
 
    Vector2 lastPosition = new Vector2(0, 0);
    Vector2 currPosition = new Vector2(0, 0);


    Vector2 delta;
    Vector2 currRow = new Vector2(0, 1);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lastPosition.x = currPosition.x;
        lastPosition.y = currPosition.y;

        currPosition.x = transform.position.x;
        currPosition.y = transform.position.z;


        if (!currPosition.Equals(lastPosition))
        {

            delta = currPosition - lastPosition;

            if (Math.Abs(Vector2.SignedAngle(delta, currRow)) > 15)
            {
                Debug.Log("x: " + delta.x + " y: " + delta.y);

                Debug.Log(Vector2.Angle(delta, currRow).ToString());

                transform.Rotate(0, Vector2.SignedAngle(delta, currRow), 0, Space.Self);


                currRow = delta;
                // currRow.y = delta.y;
            }
        }
    }
}
