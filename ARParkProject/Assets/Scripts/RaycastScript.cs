using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using DG.Tweening;

public class RaycastScript : MonoBehaviour 
{
    public bool isTouched = false;

    void Start()
    {
       
    }
   
    void FixedUpdate()
    {

        if (Input.touchCount > 0)
        {
            Ray touchRay;

            touchRay = Camera.main.ScreenPointToRay(Input.touches[0].position);

            if(Physics.Raycast(touchRay, out RaycastHit raycastHit))
            {
                var dinosaur = raycastHit.collider.gameObject;

                if (dinosaur.tag == "Dinosaur")
                {
                    isTouched = true;

                    if((dinosaur.transform.position - Camera.main.transform.position).magnitude < 50)
                    {
                       // this.transform.rotation = new Quaternion(0, Camera.main.transform.rotation.y + 90, 0, 0);
                        this.transform.DORotateQuaternion(new Quaternion(0, Camera.main.transform.rotation.y + 90f, 0, 0), 1.5f);
                        //this.GetComponentInChildren<Animation>().CrossFade("Death");
                    }
                }

            }

        }
    }
}
