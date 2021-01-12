using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxScript : MonoBehaviour
{
    public GameObject food;
    public GameObject foodButton;
    public GameObject scrollView;

    public bool foodIsTouched = false; // значение меняется в скрипте GameManagerScript
    public bool foodIsDestroyed = false;
    public bool boxIsTouched = false; // значение меняется в скрипте GameManagerScript
    public bool boxIsOpened = false;
   
    public GameObject panel; // панель, которая появляется при открытие сундука

    void Start()
    {
        
    }

    void Update()
    {
        if ((gameObject.transform.position - Camera.main.transform.position).magnitude < 10)
        {
            if(boxIsTouched && !boxIsOpened)
            {
                boxIsOpened = true;

                gameObject.GetComponent<Animator>().SetBool("Opened", true);
              //  panel.SetActive(true);
              //  StartCoroutine("setPanel");
            }

            if (foodIsTouched && !foodIsDestroyed && boxIsOpened) // если открыт чемодан и коснулись еды
            {
                foodIsDestroyed = true;
 
                foodButton.SetActive(true); // кнопку делаем активной
                foodButton.GetComponent<FoodButtonsScript>().FoodCountIncrement(); // +1 к счетчику, text на кнопке = счетчик
                foodButton.GetComponentInChildren<Text>().text = foodButton.GetComponent<FoodButtonsScript>().GetFoodCount().ToString();
                     
                Destroy(food);
            }
        }
    }

    IEnumerator setPanel()
    {
        yield return new WaitForSeconds(3f);
        panel.SetActive(false);
    }
}
