using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portion : interactables
{

    private Vector3 startPos;
    [SerializeField] private Transform model;
    private Quaternion normalRotation;
    private bool switch_invoke = false;

    private Animator anim;
    private bool m_working = false;


    [Header("Lequied")]
    [SerializeField] private Transform FillBlend;
    [SerializeField] private SkinnedMeshRenderer renderer_1,renderer_2;
    public float blend_1,blend_2;

    [SerializeField] private float targetBlend_1, targetBlend_2;
    [SerializeField] private int index_1,index_2 = 0;
  

    [SerializeField] private float fillSpeed = 2;
    [SerializeField] private bool changeCoolr;
    [SerializeField] private bool autoDrop = false;
    [SerializeField] private Material newColor;

    [Header("VFX")]
    [SerializeField] private ParticleSystem droping_fx;
    [SerializeField] private ParticleSystem wave_fx;
    [SerializeField] private Transform rayPoint;


    private Vector3 targetPosition;


    public void Start()
    {
        anim = GetComponent<Animator>();
        startPos = transform.position;
        normalRotation = transform.rotation;

        blend_1 = renderer_1.GetBlendShapeWeight(index_1);


        if(renderer_2 != null)
        {
            blend_2 = renderer_2.GetBlendShapeWeight(index_2);
        }

        FillBlend.gameObject.SetActive(true);


        if(model == null)
        {
            model = transform;
        }
    }




    public override void setup()
    {
        base.setup();
        startPos = transform.position;
    }


   

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.CompareTag("trigger")  && !isTaskDone())
        {

            if(autoDrop)
            {
                StartCoroutine(autoDroRutine(other.transform.GetChild(0)));
            }
       
            model.rotation = Quaternion.Slerp(model.rotation,other.transform.GetChild(0).rotation,5 * Time.deltaTime );
            act();
        }
    }



    private void OnTriggerExit(Collider other)
    {
        droping_fx.Stop();
        droping_fx.Clear();
    }


    private bool isTaskDone()
    {
        return  Mathf.Abs(blend_1 - targetBlend_1) < 0.05f;
    }


    void act()
    {

        if(droping_fx.isPlaying)
        {
            blend_1 = Mathf.MoveTowards(blend_1, targetBlend_1, fillSpeed * Time.deltaTime);
            renderer_1.SetBlendShapeWeight(index_1, blend_1);

            if(renderer_2 != null)
            {
                blend_2 = Mathf.MoveTowards(blend_2, targetBlend_2, fillSpeed * Time.deltaTime);
                renderer_2.SetBlendShapeWeight(index_2, blend_2);
            }
        }

        if(!droping_fx.isPlaying) 
        {
            droping_fx.Play();
        }


      
    }

  
    private void Update()
    {
        if(allowed_to_interect)
            base.checkStatic();

        if(switchEvent)
        {
            if(Mathf.Abs(blend_1 - targetBlend_1) < 1f)
            {
                droping_fx.Stop();
                if(!switch_invoke)
                {
                    onDeselected();
                    switch_invoke = true;
                    LevelMannger.instance.eventSwitch();
                }
            }
        }
    }





    public override void onSelected()
    {
        base.onSelected();

        if(GetComponent<Stack>())
        {
            return;
        }

        if(anim != null)
            anim.SetBool("isOpen",true);
    }

    public override void onDeselected()
    {

        if(m_working)
        {
            return;
        }

        base.onDeselected();

        if(GetComponent<Stack>())
        {
            return;
        }

        if(anim != null)
            anim.SetBool("isOpen",false);
        resetRutine();
    }


    private  void resetRutine()
    {   
        droping_fx.Stop();
        droping_fx.Clear();
        
        moveObject(transform,startPos,2);
        rotateObject(model,normalRotation);
    }


    IEnumerator autoDroRutine(Transform g)
    {
        m_working = true;
        moveObject(transform,g.position,5);

        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime / 5;
            if(isTaskDone())
            {
                break;
            }
            yield return null;
        }
      


        m_working = false;
        onDeselected();
    }
  
}

