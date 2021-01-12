using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class FoodButtonsScript : MonoBehaviour
{
    public string path = null;

    public GameObject hamburger; // вынести это в гейм менеджер, а здесь в старте проверять, к какой кнопке прикреплен скрипт 
    public GameObject donut;     // и в соответствии с этим устанавливать компонент еды, или у каждой кнопки свой скрипт со
    public GameObject cola;
    public GameObject cake;      // public GameObject spawn;     // своим объектом еды
    public GameObject gameManager;

    Ray ray;

    private int foodCount = 0;
    public FoodButton foodButton;

    public void SetFoodCount(int count) { foodCount = count; }
    public int GetFoodCount() { return foodCount; }
    public void FoodCountIncrement() { foodCount++; }
    public void FoodCountDecrement() { foodCount--; }

    void Start()
    {

#if UNITY_ANDROID && !UNITY_EDITOR

        path = Path.Combine(Application.persistentDataPath, gameObject.name + ".json");

#else

        path = Path.Combine(Application.dataPath, gameObject.name + ".json");

#endif

        if (File.Exists(path))
        {
            foodButton = JsonUtility.FromJson<FoodButton>(File.ReadAllText(path));
            foodCount = foodButton.foodCount;
        }

        if (foodCount == 0) { gameObject.SetActive(false); }
        else
        {
            GetComponentInChildren<Text>().text = foodCount.ToString();
        }
    }

    // ПРИ НАЖАТИИ НА КНОПКУ БУРГЕР

    public void HamburgerButtonClick()
    {
        if (gameManager.GetComponent<GameManagerScript>().choosenFood == null)
        {
            InstantiateFood(hamburger);
        }


        else
        {
            Destroy(gameManager.GetComponent<GameManagerScript>().choosenFood);
        }
        
    }

    // ПРИ НАЖАТИИ НА КНОПКУ ПОНЧИК

    public void DonutButtonClick()
    {
        if (gameManager.GetComponent<GameManagerScript>().choosenFood == null)
        {
            InstantiateFood(donut);
        }

        else
        {
            Destroy(gameManager.GetComponent<GameManagerScript>().choosenFood);
        }
    }

    // ПРИ НАЖАТИИ НА КНОПКУ КОЛА

    public void ColaButtonClick()
    {
        if (gameManager.GetComponent<GameManagerScript>().choosenFood == null)
        {
            InstantiateFood(cola);
        }

        else
        {
            Destroy(gameManager.GetComponent<GameManagerScript>().choosenFood);
        }
    }

    // ПРИ НАЖАТИИ НА КНОПКУ ТОРТ

    public void CakeButtonClick()
    {
        if (gameManager.GetComponent<GameManagerScript>().choosenFood == null)
        {
            InstantiateFood(cake);
        }

        else
        {
            Destroy(gameManager.GetComponent<GameManagerScript>().choosenFood);
        }
    }

    
    private void InstantiateFood(GameObject food)
    {
        ray = Camera.main.ScreenPointToRay(gameManager.GetComponent<GameManagerScript>().spawn.transform.position); // луч из камеры в spawn
        Vector3 foodPosition = ray.GetPoint(0.5f); // получаем позицию луча

        gameManager.GetComponent<GameManagerScript>().choosenFood = Instantiate(food, foodPosition, Quaternion.identity); // устанавливаем объект еды на spawn 
        gameManager.GetComponent<GameManagerScript>().choosenFood.GetComponent<FoodScript>().isInstantiated = true;
    }

#if UNITY_ANDROID && !UNITY_EDITOR

   private void OnApplicationPause(bool pause) {
        if (pause) {
            foodButton.foodCount = foodCount;
            File.WriteAllText(path, JsonUtility.ToJson(foodButton));
        }
    }

#endif

    private void OnApplicationQuit()
    {
        foodButton.foodCount = foodCount;
        File.WriteAllText(path, JsonUtility.ToJson(foodButton));
    }

    [Serializable]
    public class FoodButton
    {
        public int foodCount;

        public FoodButton(int foodCount = 0) { this.foodCount = foodCount; }
    }
}
