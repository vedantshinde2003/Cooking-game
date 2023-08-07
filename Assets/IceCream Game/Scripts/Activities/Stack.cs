using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [SerializeField] protected bool isSlective;
    [SerializeField] private float vert,forw;
    [SerializeField]  private Vector3 outPosition;
    [SerializeField] private bool eventSwitch_afterOut;

    [SerializeField] private GameObject groundHit;
    [SerializeField] private Transform vfx_point;
    
    [SerializeField] private ParticleSystem glow;



    private void Start()
    {
        if(vfx_point == null)
            vfx_point = transform;
    }

    public void onTap()
    {
        if(isSlective)
        {
            isSlective = false;
            StartCoroutine(FridgeRutine());
            setGlow(false);
        }
    }

    private IEnumerator FridgeRutine()
    {
      

        Vector3 pos = transform.position + Vector3.forward * forw+ Vector3.up * vert;

        StartCoroutine(MoveObjectCoroutine(transform,pos,false));

        yield return new WaitForSeconds(2);
        if(eventSwitch_afterOut)
        {
            LevelMannger.instance.eventSwitch();
        }

        StartCoroutine(MoveObjectCoroutine(transform,outPosition,true));
        yield return new WaitForSeconds(1);

      
       

    }


    private IEnumerator MoveObjectCoroutine(Transform obj, Vector3 target,bool endFlag)
    {
       Vector3  targetPosition = target;
      
        Vector3 startPosition = obj.position;
     

       
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime/2 ;
            obj.position = Vector3.Lerp(obj.position, targetPosition, t);
            yield return null;
        }


        obj.position = targetPosition; 
        

        if(endFlag)
        {
            Instantiate(groundHit,vfx_point.position,Quaternion.identity);
            GetComponent<interactables>().setup();
            Destroy(this);
        }      
        yield break;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(outPosition, 0.1f);
    }


    public void setGlow(bool trigger)
    {
        if(trigger)
        {
            glow.Play();
        }
        else
        {
            glow.Stop();
        }
    }

}