using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class scope : interactables
{
    private Animator anim;
    private bool isFilled;



    private GameObject currentBall;
    [SerializeField] private GameObject ballPrefabe;
    [SerializeField] private Transform ballPositon;
    [SerializeField] private Transform cup;



    [SerializeField] private GameEvent setCameraEvent;
    [SerializeField] private GameEvent resetCameraEvent;
    private bool invoke = false;
    

    private Vector3 startPose;


    private void Start()
    {
        anim = GetComponent<Animator>();
        startPose = transform.position;
        base.setup();
    }

    void OnTriggerStay(Collider col)
    {
        if(col.GetComponent<creamBOdy>())
        {
            if(!isFilled)
            {
                desableDrag();
                startCutting(col.GetComponent<creamBOdy>());
            }
        }

        if(col.GetComponent<iceCreamCup>())
        {
            if(isFilled)
            {
                desableDrag();
                StartDroping(col.GetComponent<iceCreamCup>());
            }
        }
    }

    private void startCutting(creamBOdy bOdy)
    {
       
        GetCurrentBall(bOdy.getTextre());
        transform.position = bOdy.GetPos();
        anim.SetTrigger("start");

      

        Vector3 forces = new Vector3(1,1,100);



        StartCoroutine(setScopeRutine());
     

        
    }

    private void Update()
    {
        if(isFilled)
        {
          
            if(Vector3.Distance(transform.position,cup.position) < 0.45f)
            {
                if(!invoke)
                {
                    invoke = true;
                    setCameraEvent.Raise();
                }
            }
            else
            {
                if(invoke)
                {
                    resetCameraEvent.Raise();
                }
            }
        }
    }







    private void StartDroping(iceCreamCup creamCup)
    {
        isFilled = false;
        transform.position = creamCup.getPos();
        anim.SetTrigger("drop");

        StartCoroutine(dropBall(creamCup));

       
    }

    public void onBallReach()
    {
        Destroy(currentBall);
        unableDrag();

        
     //   GetComponent<LeanDragTranslate>().setDrag(Vector3.one,1f ,true,true,false);

        transform.position = startPose;
       // moveObject(transform,startPose,2);

        resetCameraEvent.Raise();
        invoke = false;
        
    }




    private IEnumerator dropBall(iceCreamCup creamCup)
    {
  
        yield return new WaitForSeconds(1f);
        currentBall.transform.parent = null;
        currentBall.GetComponent<TrailRenderer>().enabled = true;
        moveObject(currentBall.transform,creamCup.getCurretBallPos(currentBall.GetComponent<Renderer>().material),2);

    }



    public void GetCurrentBall(Material mat)
    {
        isFilled = true;
        currentBall = Instantiate(ballPrefabe,ballPositon.position,Quaternion.identity,ballPositon);
        currentBall.GetComponent<Renderer>().material = mat;
        
        
    }



    private IEnumerator setScopeRutine()
    {
        yield return new WaitForSeconds(1f);
        transform.position += Vector3.up * 0.2f;
    }


    


    

    
}
