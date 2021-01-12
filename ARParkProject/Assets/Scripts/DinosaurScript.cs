using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.IO;
using System;

public class DinosaurScript : MonoBehaviour
{
    public Dinosaur dinosaur;
    Animator animator;
    public List<GameObject> hearts;
    public GameObject takeMeMessage;

    public int maxHeartCount;

    string path = null;

 
    void Start()
    {
        // СЧИТЫВАНИЕ ДАННЫХ О ДИНОЗАВРЕ ИЗ <DINOSAUR_NAME>.JSON

        for (int i = 0; i < maxHeartCount; i++) {
            hearts[i].SetActive(false);
        }

#if UNITY_ANDROID && !UNITY_EDITOR

        path = Path.Combine(Application.persistentDataPath, gameObject.name + ".json");

#else

        path = Path.Combine(Application.dataPath, gameObject.name + ".json");

#endif

        if (File.Exists(path))
        {
            dinosaur = JsonUtility.FromJson<Dinosaur>(File.ReadAllText(path));

            for (int i = 0; i < dinosaur.heartCount; i++)
            {
                hearts[i].SetActive(true);

                if (i == maxHeartCount - 1) takeMeMessage.SetActive(true);
            }
        }
        else
        {
            dinosaur = new Dinosaur();
        }

        animator = GetComponentInParent<Animator>();

    } 

    void FixedUpdate()
    {
        
        if ((gameObject.transform.position - Camera.main.transform.position).magnitude < 50 && dinosaur.isTouched)
        {
            gameObject.GetComponent<Animator>().SetBool("Catched", true);
        }

        if (animator.GetBool("Fed"))
        {
            StartCoroutine("fedAnimationOff");
        }
    }

    IEnumerator fedAnimationOff()
    {
        yield return new WaitForSeconds(4f);
        animator.SetBool("Fed", false);
    }

#if UNITY_ANDROID && !UNITY_EDITOR

   private void OnApplicationPause(bool pause) {
        if (pause) {
            File.WriteAllText(path, JsonUtility.ToJson(dinosaur));
        }
    }

#endif

    private void OnApplicationQuit()
    {
        File.WriteAllText(path, JsonUtility.ToJson(dinosaur));
    }

    [Serializable] 
    public class Dinosaur
    {
        public bool isTouched; // = catched
        public int heartCount;

        public Dinosaur(bool isTouched, int heartCount)
        {
            this.isTouched = isTouched;
            this.heartCount = heartCount;
        }

        public Dinosaur()
        {
            isTouched = false;
            heartCount = 0;
        }
    }
}
