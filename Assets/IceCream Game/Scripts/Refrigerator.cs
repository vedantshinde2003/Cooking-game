using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : MonoBehaviour
{
   
    [SerializeField] Scenario.cameraSettings CameraSettings_doorOpenEvent;

    [SerializeField] private bool bigDoorOpen;
    [SerializeField] private bool smallDoorOpen;




    private bool allowed_to_act = true;
    
    [SerializeField] private ParticleSystem frozen;
    [SerializeField] private ParticleSystem steam;
    [SerializeField] private Transform bigDoor;
    [SerializeField] private Transform smallDoor;



    [SerializeField] private GameEvent fridgeOpenEvents;
    [SerializeField] private GameEvent fridgeCloseEvents;



    public void intreactBig()
    {
        if(allowed_to_act)
        {

            if(bigDoorOpen)
            {
                if(smallDoorOpen)
                {
                    interactSmall();
                }
            }


            Quaternion rot;
            if(bigDoorOpen)
            {
                rot = Quaternion.Euler(0,0,0);
                fridgeOpenEvents.Raise(CameraSettings_doorOpenEvent);
            }
            else
            {
                rot = Quaternion.Euler(0,90,0);           
            }
            StartCoroutine(RotateObjectCoroutine(bigDoor,rot));
            bigDoorOpen = !bigDoorOpen;
        }
    }

    public void interactSmall()
    {
        if(allowed_to_act)
        {
            Quaternion rot;
            if(smallDoorOpen)
            {
                rot = Quaternion.Euler(0,0,0);

            }
            else
            {
                rot = Quaternion.Euler(0,90,0);  
                         
            }

            StartCoroutine(RotateObjectCoroutine(smallDoor,rot));
            smallDoorOpen = !smallDoorOpen;
        }
    }







    public void StartFreezing(bool switc)
    {
        StartCoroutine(FreezingRutine(switc));
    }

    private IEnumerator FreezingRutine(bool switchEvet)
    {
        print("started");
        interactSmall();
        allowed_to_act = false;
        yield return new WaitForSeconds(1);
        steam.Play();
        yield return new WaitForSeconds(3);
        allowed_to_act = true;
        interactSmall();

     
        if(switchEvet)
        {
            LevelMannger.instance.eventSwitch();
        }

        frozen.Play();
    }
    




    
    private IEnumerator RotateObjectCoroutine(Transform obj, Quaternion rotation)
    {
        Quaternion target = rotation;
        // Calculate the jump start and end positions
        Vector3 startPosition = obj.position;
     

        // Jump up
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime / 1;
            obj.rotation = Quaternion.Slerp(obj.rotation, target, t);
            yield return null;
        }
        // Ensure the object reaches the exact target position
        obj.rotation = rotation;       
        yield break;
    }
}
