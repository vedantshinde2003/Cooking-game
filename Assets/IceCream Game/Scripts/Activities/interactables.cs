using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

[RequireComponent(typeof (InteractiveObject))]
public class interactables : MonoBehaviour
{
    
    [Header("parent essentiles")]

    public bool moveX = true;
    public bool moveY = true;
    public bool moveZ = true;
    public GameEvent onObjectReachEvent;


    [SerializeField] protected bool switchEvent;
    protected bool m_intereacting = false;
    public bool allowed_to_interect = true;
    private bool m_invoke = false;
    private float m_unactiveTime = 5;
    [SerializeField] private float m_maxUnactive_time = 5;




    public virtual void setup()
    {
        if(GetComponent<LeanFingerDown>())
        {
            GetComponent<LeanFingerDown>().enabled = true;
        }

        if(GetComponent<LeanFingerUp>())
        {
            GetComponent<LeanFingerUp>().enabled = true;
        }

        if(GetComponent<LeanDragTranslate>())
        {
            GetComponent<LeanDragTranslate>().enabled = true;
            GetComponent<LeanDragTranslate>().moveX = moveX;
            GetComponent<LeanDragTranslate>().moveY = moveY;
            GetComponent<LeanDragTranslate>().moveZ = moveZ;
        }

        if(GetComponent<LeanSelectableByFinger>())
        {
            GetComponent<LeanSelectableByFinger>().enabled = true;
        }

        GetComponent<InteractiveObject>().setup();

        this.enabled = true;
    }

    public void set( bool f)
    {
        allowed_to_interect = f;
    }


    public virtual void checkStatic()
    {
        if(!m_intereacting)
        {
            m_unactiveTime -= Time.deltaTime;
        }


        if(m_unactiveTime <= 0 && !m_invoke)
        {
            StartCoroutine(unactiveRutine());
            m_invoke = true;
        }
    }


   



    private IEnumerator unactiveRutine()
    {
        GetComponent<InteractiveObject>().Act();
        yield return new WaitForSeconds(3);
        m_invoke = false;
    }





    public virtual void onSelected()
    {
        m_intereacting = true;
        m_unactiveTime = m_maxUnactive_time;


        if(GetComponent<Stack>())
        {
            GetComponent<Stack>().onTap();
        }
    }

    public virtual void onDeselected()
    {
        m_intereacting = false;
    }

  
    

    protected void moveObject(Transform obj, Vector3 target,float duration)
    {
        StartCoroutine(MoveObjectCoroutine(obj,target,duration));
    }


    protected void rotateObject(Transform obj, Quaternion target)
    {
        StartCoroutine(RotateObjectCoroutine(obj,target));
    }





 


    private IEnumerator MoveObjectCoroutine(Transform obj, Vector3 target,float duration)
    {
       Vector3  targetPosition = target;
   
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            obj.position = Vector3.Lerp(obj.position, targetPosition, t);


            if(m_intereacting)
            {
                yield return null;
                break;
            }
            yield return null;
        }
        // Ensure the object reaches the exact target position
        obj.position = targetPosition;      

        if(onObjectReachEvent != null)
        {
            onObjectReachEvent.Raise();
        } 
        yield break;
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



    public void desableDrag()
    {
        GetComponent<LeanDragTranslate>().enabled = false;
    }

    public void unableDrag()
    {
        GetComponent<LeanDragTranslate>().enabled = true;
    }
  
    

}
