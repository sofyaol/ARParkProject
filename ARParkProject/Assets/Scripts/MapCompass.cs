using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MapCompass : MonoBehaviour
{
    // x - latitude    y - longitude

    Vector2 lastPosition = new Vector2(0, 0);
    Vector2 currPosition = new Vector2(0, 0);

    // animation

    public Animator animator;
    Vector2 animPosition = new Vector2(0, 0);
    bool isStaying = true;

    Vector2 delta;
    Vector2 currRow = new Vector2(0, 1);

    float angle = 0;


    // Start is called before the first frame update
    void Start()
    {
        Input.location.Start(5, 0.1f);
        InvokeRepeating("SwitchAnimation", 1f, 1.5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // получаем новую позицию 

        lastPosition.x = currPosition.x;
        lastPosition.y = currPosition.y;

        currPosition.x = transform.position.x;
        currPosition.y = transform.position.z;

        if (!currPosition.Equals(lastPosition))
        {
            
            // находим разность векторов  

            delta = currPosition - lastPosition;
            angle = Vector2.SignedAngle(delta, currRow);

            if (Math.Abs(angle) > 5)
            {

                Debug.Log("currRow:" + currRow.x + " " + currRow.y + "delta:" + delta.x + " " + delta.y);
                Debug.Log("Angle: " + Vector2.SignedAngle(delta, currRow).ToString() + " currPosX: " + currPosition.x + " currPosY: " + currPosition.y);

                gameObject.transform.Rotate(0, angle, 0, Space.Self);

                currRow.x = delta.x;
                currRow.y = delta.y;
            }
        }


    }

    void SwitchAnimation()
    {
        if (animPosition.x == transform.position.x && animPosition.y == transform.position.z)
        {
            if (!isStaying)
            {
                animator.SetBool("stay", true);
                isStaying = true;
            }
        }

        else
        {
            if (isStaying)
            {
                animator.SetBool("stay", false);
                isStaying = false;
            }
        }

        animPosition.x = transform.position.x;
        animPosition.y = transform.position.z;
    }
}
