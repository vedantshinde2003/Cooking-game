using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveable : interactables
{
    [SerializeField] private GameObject active;
    [SerializeField] private GameObject Trigger;

    [SerializeField] private GameEvent afterTriggerEvents;



    void Start()
    {
        base.setup();
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("trigger"))
        {

            if(Trigger != null)
            {

                if(col.gameObject == Trigger)
                {
                    transform.position = col.transform.position;
                    desableDrag();
                    if(active != null)
                    {
                        gameObject.SetActive(false);
                        active.SetActive(true);
                    }

                    if(switchEvent)
                    {
                        LevelMannger.instance.eventSwitch();
                    }


                    if(afterTriggerEvents != null)
                    {
                        afterTriggerEvents.Raise();
                    }
                }
            }
            else
            {
                transform.position = col.transform.position;
                desableDrag();
                if(active != null)
                {
                    gameObject.SetActive(false);
                    active.SetActive(true);
                }

                  if(switchEvent)
                    {
                        LevelMannger.instance.eventSwitch();
                    }


            
                if(afterTriggerEvents != null)
                {
                    afterTriggerEvents.Raise();
                }
            }
         
        }
        
    }
}
