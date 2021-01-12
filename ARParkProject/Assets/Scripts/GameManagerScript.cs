using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    public List<GameObject> foodButtonsList; // список неактивных кнопок еды
    RaycastHit[] raycastHits;
    int hitsCount;


    public GameObject choosenFood = null;
    Ray ray;
    public GameObject spawn;


    void Update()
    {
        if (Input.touchCount > 0)
        {
            Ray touchRay;

            touchRay = Camera.main.ScreenPointToRay(Input.touches[0].position);
            raycastHits = Physics.RaycastAll(touchRay);

            hitsCount = raycastHits.Length;

            if (hitsCount > 0)
            { 
                foreach (RaycastHit hit in raycastHits)
                {
                    if (hit.collider.gameObject.tag == "BoxFood")
                    {
                        if (hit.collider.gameObject.GetComponentInParent<BoxScript>().boxIsOpened)
                        {
                            if (hit.distance < 0.7
)
                            {
                                hit.collider.gameObject.GetComponentInParent<BoxScript>().foodIsTouched = true;
                            }
                        }
                    }
                }

                var raycastObject = raycastHits[0].collider.gameObject; // проверяем только первый объект с которым столкнулись на сцене 

                if (raycastObject.name == "AR Floor")
                    raycastObject = raycastHits[1].collider.gameObject;

                if (raycastObject.tag == "Dinosaur")
                {
                    raycastObject.GetComponentInParent<DinosaurScript>().dinosaur.isTouched = true;
                }
                
                else if(raycastObject.tag == "Box")
                {
                    raycastObject.GetComponentInParent<BoxScript>().boxIsTouched = true;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (choosenFood != null)
        {
            // если объект (еда) еще не на земле, но уже создан 
            if (!choosenFood.GetComponent<FoodScript>().isFallen && choosenFood.GetComponent<FoodScript>().isInstantiated)
            {
                // объект перемещается за камерой
                ray = Camera.main.ScreenPointToRay(spawn.transform.position);
                Vector3 choosenFoodPosition = ray.GetPoint(0.5f);

                choosenFood.transform.position = choosenFoodPosition;
                choosenFood.transform.rotation = Camera.main.transform.rotation;
            }

            // иначе если выбранный объект уже на земле, но еще не NULL
            else if (choosenFood.GetComponent<FoodScript>().isFallen)
            {
                // делаем объект NULL
                choosenFood = null;
            }
        }
    }

    void OnGUI()
    {
        string collidersNames = null;

        if(raycastHits != null)
        foreach (RaycastHit hit in raycastHits) {
            collidersNames += " " + hit.collider.name;
        }

        GUI.Label(new Rect(40, 300, 1000, 1000), collidersNames);
    }
}
