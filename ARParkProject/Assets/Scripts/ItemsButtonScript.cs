using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsButtonScript : MonoBehaviour
{
    // скрипт для кнопки в левом верхнем углу, чтобы появился скролвью

    public GameObject scrollView;
    bool isAppeared = false;

    public void OnClick()
    {
        if (!isAppeared)
        {
            isAppeared = !isAppeared;
            scrollView.GetComponent<Animator>().SetBool("IsAppeared", true);
        }

        else
        {
            isAppeared = !isAppeared;
            scrollView.GetComponent<Animator>().SetBool("IsAppeared", false);
        }
    }
    
}
