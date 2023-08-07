using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingridentpad : MonoBehaviour
{
  

    [SerializeField] private Animator anim;

    private bool invoke = false;
  

   
    public void showPad()
    {
        invoke = false;
        anim.SetBool("showPad",true);
    }


    public void removePad()
    {
        anim.SetBool("showPad",false);
    }


    public void onTap()
    {
        if(!invoke)
        {
            if(LevelMannger.instance)
                LevelMannger.instance.eventSwitch();
            
        }

        invoke = true;
        removePad();
    }
}
