using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using Assets.Scripts;

public class DrawCut : MonoBehaviour
{
   // public Transform boxVis;
    Vector3 pointA;
    Vector3 pointB;

    private LineRenderer cutRender;
    private bool animateCut;



    Camera cam;

    void Start() {
        cam = FindObjectOfType<Camera>();
        cutRender = GetComponent<LineRenderer>();
        cutRender.startWidth = .005f;
        cutRender.endWidth = .005f;
    }

    void Update()
    {
        if(Application.isEditor)
        {
            Editor();
        }
        else
        {
            mobile();
        }
    }


    void Editor()
    {
       
            
   
        Vector3 mouse = Input.mousePosition;
        mouse.z = cam.transform.position.z;

        if (Input.GetMouseButtonDown(0))
        {
            pointA = cam.ScreenToWorldPoint(mouse);
        }

        if (Input.GetMouseButton(0))
        {
            animateCut = false;
            cutRender.SetPosition(0,pointA);
            cutRender.SetPosition(1,cam.ScreenToWorldPoint(mouse));
            cutRender.startColor = Color.gray;
            cutRender.endColor = Color.gray;
        }

        if (Input.GetMouseButtonUp(0)) {
            pointB = cam.ScreenToWorldPoint(mouse);
            CreateSlicePlane();
            cutRender.positionCount = 2;
            cutRender.SetPosition(0,pointA);
            cutRender.SetPosition(1,pointB);
            animateCut = true;
        }

        if (animateCut)
        {
            cutRender.SetPosition(0,Vector3.Lerp(pointA,pointB,1f));
        }
    }

    void mobile()
    {
        if(Input.touchCount == 1)
        {
            
             Touch touch = Input.GetTouch(0);
            Vector3 mouse = touch.position;
            mouse.z = cam.transform.position.z;
           // g.SetActive(false);
  
                
            switch (touch.phase)
            {
                    
                case TouchPhase.Began:
                    pointA = cam.ScreenToWorldPoint(mouse);                     
                    break;

                   
                case TouchPhase.Moved:
                    animateCut = false;
                    cutRender.SetPosition(0,pointA);
                    cutRender.SetPosition(1,cam.ScreenToWorldPoint(mouse));
                    cutRender.startColor = Color.gray;
                    cutRender.endColor = Color.gray;     
                    break;


                case TouchPhase.Ended:
                    pointB = cam.ScreenToWorldPoint(mouse);
                    CreateSlicePlane();
                    cutRender.positionCount = 2;
                    cutRender.SetPosition(0,pointA);
                    cutRender.SetPosition(1,pointB);
                    animateCut = true;
                    break;


            }
            
            if (animateCut)
            {
                cutRender.SetPosition(0,Vector3.Lerp(pointA,pointB,1f));
            }

        }





  
        

     
       
    }

    void CreateSlicePlane() 
    {
        Vector3 pointInPlane = (pointA + pointB) / 2;
        
        Vector3 cutPlaneNormal = Vector3.Cross((pointA-pointB),(pointA-cam.transform.position)).normalized;
        Quaternion orientation = Quaternion.FromToRotation(Vector3.up, cutPlaneNormal);
        //boxVis.rotation = orientation;
       // boxVis.localScale = new Vector3(10, 0.25f, 10);
       // boxVis.position = pointInPlane;

        Plane plane = new Plane();
            plane.SetNormalAndPosition(
            cutPlaneNormal,
            pointInPlane);

        var direction = Vector3.Dot(Vector3.up, cutPlaneNormal);

        if (direction < 0)
        {
            plane = plane.flipped;
        }
        
        var all = Physics.OverlapBox(pointInPlane, new Vector3(20, 0.01f, 20), orientation);
        
        //Ray ray = new Ray(pointA, (pointB - pointA).normalized);
        //var all = Physics.RaycastAll(ray);
        {
            foreach (var hit in all)
            {
                if(hit.gameObject.tag == "cutable")
                {
                    Sliceable i = hit.GetComponent<Sliceable>();
                    MeshFilter filter = hit.gameObject.GetComponentInChildren<MeshFilter>();
                    if(filter != null)
                    {
                        if(i == null)
                        {
                            Material mat;
                            if(hit.gameObject.GetComponent<MeshRenderer>().materials.Length > 1)
                            {
                                mat = hit.gameObject.GetComponent<MeshRenderer>().materials[1];
                            }
                            else
                            {
                                mat = hit.gameObject.GetComponent<MeshRenderer>().materials[0];
                            }

                            Cutter.Cut(hit.gameObject, pointInPlane, cutPlaneNormal, mat);
                        }
                        else
                            Cutter.Cut(hit.gameObject, pointInPlane, cutPlaneNormal, i.GetMaterial());
                    }
                }

                    



            }
        }
        
    }
}
