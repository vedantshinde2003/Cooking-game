using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeshSelectionManger : MonoBehaviour
{
    [SerializeField] private GameObject[] scenes;
    [SerializeField] private Animator anim;
    
    

    public static DeshSelectionManger instance;

    
    void Start()
    {
        instance = this;
    }

    
    void Update()
    {
        
    }



    public void onDeshSelected(int i)
    {
        GameObject currentScene = Instantiate(scenes[i]);
        currentScene.SetActive(true);
        removePad();
    }

    public void showPad()
    {
        anim.SetBool("showPad",true);
    }

    public void removePad()
    {
        anim.SetBool("showPad",false);
    }
}
