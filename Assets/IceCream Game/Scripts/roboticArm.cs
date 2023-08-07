using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roboticArm : MonoBehaviour
{
    [SerializeField] private Vector3 unlockedPosition;
    [SerializeField] private Vector3 lockedPosition;
    

    private Vector3 target;
    [SerializeField] private float Speed = 1;

    [SerializeField] private robotarmEvents handControl;

    void Start()
    {
        target = lockedPosition;
    }

    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,target,Speed * Time.deltaTime);

        if(target == unlockedPosition)
        {
            if(transform.position == target)
            {
                handControl.enabled = true;
            }
        }
    }

    public void Unlock()
    {
        target = unlockedPosition;
    }

    public void Lock()
    {
        target = lockedPosition;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(unlockedPosition, 0.1f);

         Gizmos.color = Color.blue;
        Gizmos.DrawSphere(lockedPosition, 0.05f);

        
    }
}
