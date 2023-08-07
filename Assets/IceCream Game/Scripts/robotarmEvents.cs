using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyUnderware.Curvy.Controllers;

public class robotarmEvents : SplineController
{
    
    [SerializeField] public Transform objectToIntreact;
    [SerializeField] private Transform newParent;
    [SerializeField] private Vector3 finalPos;

    [SerializeField] private Transform platPosition;

    [SerializeField] private GameObject remove,Add;
    

    public void SetUpDroping(float waitingTime)
    {
        StartCoroutine(dropingRutine(waitingTime));
    }

    public void SetUpPicking(float waitingTime)
    {
        StartCoroutine(pickingRutine(waitingTime));
    }

    private IEnumerator pickingRutine(float time)
    {
        Speed = 0;
        yield return new WaitForSeconds(1);
        objectToIntreact.parent = platPosition;
     

        StartCoroutine(MoveObjectCoroutine(objectToIntreact,platPosition.position,false));
        yield return new WaitForSeconds(time);
        Speed = 1;
    }


    private IEnumerator dropingRutine(float time)
    {
        Speed = 0;
        yield return new WaitForSeconds(1);
        objectToIntreact.parent = newParent;

       // objectToIntreact.position = finalPos;
        if(Add != null)
            StartCoroutine(MoveObjectCoroutine(objectToIntreact,finalPos,true));
        else
            StartCoroutine(MoveObjectCoroutine(objectToIntreact,finalPos,false));
        yield return new WaitForSeconds(time);
        Speed = 1;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(finalPos, 0.05f);
        
    }


 
    private Vector3 targetPosition;
    [SerializeField]   private float jumpHeight = 0.2f;
    [SerializeField] private float jumpDuration = 0.5f;
    [SerializeField] private float moveDuration = 0.5f;

   
    private IEnumerator MoveObjectCoroutine(Transform obj, Vector3 target,bool swaap)
    {
        targetPosition = target;

        // Calculate the jump start and end positions
        Vector3 startPosition = obj.position;
        Vector3 jumpStartPosition = startPosition + new Vector3(0.0f, jumpHeight, 0.0f);
        Vector3 jumpEndPosition = startPosition + new Vector3(0.0f, jumpHeight, 0.0f);

        // Jump up
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime / jumpDuration;
            float moveT = Mathf.Sin(t * Mathf.PI * 0.5f); // Smoother movement
            obj.position = Vector3.Lerp(startPosition, jumpEndPosition, moveT);
            yield return null;
        }

        // Move towards the target position while going up
        t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime / moveDuration;
            obj.position = Vector3.Lerp(jumpEndPosition, targetPosition, t);
            yield return null;
        }

        // Ensure the object reaches the exact target position
        obj.position = targetPosition;

        if(obj.GetComponent<interactables>())
        {
            obj.GetComponent<interactables>().enabled = true;
            obj.GetComponent<interactables>().setup();
        }

        if(swaap)
        {
            remove.SetActive(false);
            Add.SetActive(true);
        }
        // Coroutine finished
        yield break;
    }
}


