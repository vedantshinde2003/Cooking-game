using UnityEngine;
using System.Collections;

public class Almari : MonoBehaviour
{
    [SerializeField] private Transform DoorLeft, DoorRight;

    private bool isOpen = false;

    public bool allowedInteact = true;


    public void closeDoors()
    {
        if(allowedInteact)
        {
            if(isOpen)
            {
                 Quaternion rot1 = Quaternion.Euler(0,0,0);
                Quaternion rot2 = Quaternion.Euler(0,0,0);

                StartCoroutine(RotateObjectCoroutine(DoorLeft,rot1));
                StartCoroutine(RotateObjectCoroutine(DoorRight,rot2));
            }
        }
    }

    public void intreact()
    {
        if(allowedInteact)
        {
            if(isOpen)
            {
                Quaternion rot1 = Quaternion.Euler(0,0,0);
                Quaternion rot2 = Quaternion.Euler(0,0,0);

                StartCoroutine(RotateObjectCoroutine(DoorLeft,rot1));
                StartCoroutine(RotateObjectCoroutine(DoorRight,rot2));
            }
            else
            {
                Quaternion rot1 = Quaternion.Euler(0,-90,0);
                Quaternion rot2 = Quaternion.Euler(0,90,0);

                StartCoroutine(RotateObjectCoroutine(DoorLeft,rot1));
                StartCoroutine(RotateObjectCoroutine(DoorRight,rot2));
            }

            isOpen = !isOpen;
        }
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
