using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMovments : MonoBehaviour
{
    public static CameraMovments instnace;
    
    [SerializeField] private Transform track;
    [SerializeField] private Transform Aim;
    private Transform aimParent;

    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float tranferingFov = 60;


    [SerializeField] Scenario.cameraSettings awakeCamera;
    

    void Awake()
    {
        instnace = this;
        changeFov(30);
    }

    void Start()
    {
        aimParent = Aim.parent;

        
    }



   
  

    public void changeCamera(Vector3 pos,float speed,float finalFOv)
    {
        StartCoroutine(ShiftCameraRutine(pos,speed,finalFOv));
    }

    public void TrackShift(Vector3 pos,float targetY, float time,float finalFOv)
    {
        StartCoroutine(ShiftCameraTrackRutine(pos, targetY,time,finalFOv));
    }

    public void changeFov(float FOV)
    {
        StartCoroutine(changFov(FOV));
    }

    public void normalShift(Vector3 t, float s)
    {
        StartCoroutine(normalShiftRutine(t,s));
    }



    public void SetTargetFollow(Transform p,float number)
    {
        Aim.parent = p;
        Aim.localPosition = Vector3.zero;

        StartCoroutine(changFov(number));
    }
    public void resetSetTargetFollow()
    {
        Aim.parent = aimParent;
      
    }
    




    private IEnumerator normalShiftRutine(Vector3 target,float speed)
    {
        yield return new WaitForSeconds(0.4f);
            // Adjust Pos
        Vector3 targetPosition = target;
        // Calculate the jump start and end positions
        Vector3 startPosition = Aim.position;

        // Calculate the distance and time required for the movement
        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration_m = speed;

        // Move towards the target position
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime / duration_m;
            Aim.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
        // Ensure the object reaches the exact target position
        Aim.position = targetPosition;
    }



    private IEnumerator ShiftCameraRutine( Vector3 target, float time,float finalFOv)
    {
         // Adjust FOV
        float elapsedTime = 0f;
        float duration = time;
       

        while (elapsedTime < duration)
        {
            virtualCamera.m_Lens.FieldOfView = Mathf.MoveTowards( virtualCamera.m_Lens.FieldOfView, tranferingFov, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    
        yield return new WaitForSeconds(0.1f);


         // Adjust Pos
        Vector3 targetPosition = target;
        // Calculate the jump start and end positions
        Vector3 startPosition = Aim.position;

        // Calculate the distance and time required for the movement
        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration_m = time;

        // Move towards the target position
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime / duration_m;
            Aim.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
        // Ensure the object reaches the exact target position
        Aim.position = targetPosition;
       

        yield return new WaitForSeconds(0.1f);


       
        float elapsedTime_last = 0f;
        float duration_last = time;
      

        while (elapsedTime_last < duration_last + 0.1f)
        {
            virtualCamera.m_Lens.FieldOfView = Mathf.MoveTowards( virtualCamera.m_Lens.FieldOfView, finalFOv, elapsedTime_last / duration_last);
            elapsedTime_last += Time.deltaTime;
            yield return null;
        }

      
        
    }



    private IEnumerator changFov(float fov)
    {
        float elapsedTime = 0f;
        float duration = 1;
       

        while (elapsedTime < duration)
        {
            virtualCamera.m_Lens.FieldOfView = Mathf.MoveTowards( virtualCamera.m_Lens.FieldOfView, fov, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    
        virtualCamera.m_Lens.FieldOfView = fov;
    
    }

    private IEnumerator ShiftCameraTrackRutine(Vector3 target, float Y ,float time,float finalFOv)
    {
        //////////////////// // Adjust FOV
        float elapsedTime = 0f;
        float duration = time;
        while (elapsedTime < duration)
        {
            virtualCamera.m_Lens.FieldOfView = Mathf.MoveTowards( virtualCamera.m_Lens.FieldOfView, tranferingFov, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    
        yield return new WaitForSeconds(0.1f);




        /////////////// // Adjust Pos
        Vector3 targetPosition = target;
        Vector3 startPosition = Aim.position;

        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration_m = time;
        // Move towards the target position
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime / duration_m;
            Aim.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
        
        Aim.position = targetPosition;
       
        yield return new WaitForSeconds(0.1f);







        ///////////  // Adjust Pos Y

        Vector3 TargetY = new Vector3(track.position.x,Y,track.position.z);
        Vector3 startY = track.position;

        float duration_y = time;
        // Move towards the target position
        float elapsedTime_y = 0.0f;
        while (elapsedTime_y < 1.0f)
        {
            elapsedTime_y += Time.deltaTime / duration_y;
            track.position = Vector3.Lerp(startY, TargetY, elapsedTime_y);
            yield return null;
        }
        
        Aim.position = targetPosition;
       

        yield return new WaitForSeconds(0.1f);








        float elapsedTime_last = 0f;
        float duration_last = time;
      

        while (elapsedTime_last < duration_last + 0.1f)
        {
            virtualCamera.m_Lens.FieldOfView = Mathf.MoveTowards( virtualCamera.m_Lens.FieldOfView, finalFOv, elapsedTime_last / duration_last);
            elapsedTime_last += Time.deltaTime;
            yield return null;
        }

      
       // virtualCamera.m_Lens.FieldOfView  = finalFOv;
    }

    


}
