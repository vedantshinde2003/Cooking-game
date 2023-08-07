using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sruve : MonoBehaviour
{
    [SerializeField] private Transform current_IceCream;
    [SerializeField] private Transform iceCreamPass;
    private Transform currentParm;
    private bool invoke = false;
    private Charector currentCharector;

    public states State;
    public enum states
    {
        waiting,surving, eating 
    }






    public void setIceCream(Transform ice)
    {
        current_IceCream = ice;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        switch(State)
        {
            case states.waiting:

              break;
            case states.surving:
                if(Vector3.Distance(currentParm.position, current_IceCream.position) < 0.1f)
                {
                    State = states.eating;
                    currentCharector.startEating();
                    current_IceCream.parent = currentParm;
                } 
                else
                {
                    if(!invoke)
                    {
                        current_IceCream.position = Vector3.MoveTowards(current_IceCream.position,iceCreamPass.position,2 * Time.deltaTime);

                        if(Vector3.Distance(current_IceCream.position, iceCreamPass.position) < 0.1f)
                        {
                            invoke = true;
                        }

                    }
                    else
                    {
                        current_IceCream.position = Vector3.MoveTowards(current_IceCream.position,currentParm.position,2 * Time.deltaTime);
                    }
                   
                }
              break;
            
            case states.eating:

                break;
        }


    }


    void OnTriggerStay(Collider col)
    {
        if(col.GetComponent<Charector>())
        {
            if(current_IceCream != null && State != states.surving)
            {
                currentParm = col.GetComponent<Charector>().GetParm();
                currentCharector = col.GetComponent<Charector>();
                 

                State = states.surving;
            }
        }
    }



     private IEnumerator MoveObjectCoroutine(Transform obj, Vector3 target,float duration)
    {
       Vector3  targetPosition = target;
   
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            obj.position = Vector3.Lerp(obj.position, targetPosition, t);


           
            yield return null;
        }
        // Ensure the object reaches the exact target position
        obj.position = targetPosition;      

        
        yield break;
    }
}
