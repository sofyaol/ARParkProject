using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodScript : MonoBehaviour
{
    public GameObject myFoodButton;
    public string myFoodButtonName;

    public bool isFallen = false;
    public bool isInstantiated = false;
    bool touchIsStarted = false;

    Vector2 startPos, endPos, direction;

    Rigidbody rigidbody;
    Vector3 speed;

    Animator animator;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        foreach(GameObject button in GameObject.Find("GameManager").GetComponent<GameManagerScript>().foodButtonsList)
        {
            if(myFoodButtonName == button.name)
            {
                myFoodButton = button;
            }
        }
   
    }


    void FixedUpdate()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && !isFallen)
        {
            Ray touchRay;

            touchRay = Camera.main.ScreenPointToRay(Input.touches[0].position);

            if (Physics.Raycast(touchRay, out RaycastHit raycastHit))
            {
                var food = raycastHit.collider.gameObject;

                if (food.tag == "Food")
                {
                    startPos = Input.touches[0].position;
                    touchIsStarted = true;
                }

            }
        }

        
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended && !isFallen && touchIsStarted)
        {

            endPos = Input.touches[0].position;
            direction = endPos - startPos;


            isFallen = true;
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;

            speed = transform.forward * Mathf.Clamp(direction.magnitude * 0.01f, 1f, 8f) + transform.up * Mathf.Clamp(direction.magnitude * 0.1f, 1, 7);
          
            rigidbody.AddForce(speed, ForceMode.Impulse);
 
            Destroy(gameObject, 4f);
        }

    }

    // если объект (еды) тригерится с головой динозавра

    void OnTriggerEnter(Collider name)
    {
        if ( name.gameObject.tag == "Dinosaur" && isFallen)
        {
            myFoodButton.GetComponent<FoodButtonsScript>().FoodCountDecrement(); // -1 к счетчику, text на кнопке = счетчик

            int foodCount;

            if ((foodCount = myFoodButton.GetComponent<FoodButtonsScript>().GetFoodCount()) == 0) // если счетчик == 0
            {
                myFoodButton.SetActive(false); 
            }

            else // иначе - текст на кнопке = число счетчика 
            {
                myFoodButton.GetComponentInChildren<Text>().text = foodCount.ToString(); 
            }

            name.gameObject.GetComponentInParent<Animator>().SetBool("Fed", true);

            // устанавливаем число сердечек 

            // НУЖЕН РЕФАКТОРИНГ!!!!!!!!!!!!!!!!!!!

            if (name.gameObject.GetComponentInParent<DinosaurScript>().dinosaur.heartCount < name.gameObject.GetComponentInParent<DinosaurScript>().maxHeartCount)
            {
                name.gameObject.GetComponentInParent<DinosaurScript>().hearts[name.gameObject.GetComponentInParent<DinosaurScript>().dinosaur.heartCount].SetActive(true);
                name.gameObject.GetComponentInParent<DinosaurScript>().dinosaur.heartCount++;
                if (name.gameObject.GetComponentInParent<DinosaurScript>().dinosaur.heartCount == name.gameObject.GetComponentInParent<DinosaurScript>().maxHeartCount)
                {
                    name.gameObject.GetComponentInParent<DinosaurScript>().takeMeMessage.SetActive(true);
                }
            }

            Destroy(gameObject);
        }
    }
}
