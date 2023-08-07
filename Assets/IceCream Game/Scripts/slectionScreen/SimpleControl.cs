using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace selectionScreen
{
    public class SimpleControl : MonoBehaviour
    {

        [SerializeField] private LayerMask walkable;
        private NavMeshAgent agent;
        [SerializeField] private Animator anim;
     

        private Camera cam;
        private Vector3 point;
        public bool allowed_toMove = true;


        [SerializeField] private LevelBox selectedLevel;



        
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            cam = Camera.main;
        }

    
        void Update()
        {
           
            if(Application.isEditor)
            {
                Editor();
            }
            else
            {
                Mobile();
            }




            


            float speedPercent = agent.velocity.magnitude / agent.speed;
            anim.SetFloat("MoveSpeed", speedPercent);
            
        }










      




        private void Editor()
        {
            if(Input.GetMouseButton(0))
            {
                bool act = false;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                

                if(Physics.Raycast(ray,out hit,Mathf.Infinity))
                {
                    LevelBox g = hit.collider.GetComponent<LevelBox>();

                    if(g != null)
                    {
                        selectedLevel = g;
                        agent.SetDestination(g.getPoint().position);
                    }
                   
                    else if(Physics.Raycast(ray,out hit,Mathf.Infinity,walkable) && allowed_toMove)
                    {
                        selectedLevel = null;
                        point = hit.point;       
                        agent.SetDestination(point); 
                    }
                }

                
            }
        }



     





        private void Mobile()
        {
        
            if(Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
            
                Ray ray = cam.ScreenPointToRay(touch.position);
                RaycastHit hit;
                    
                switch (touch.phase)
                {
                    
                        
                    case TouchPhase.Began:
                              
                        
                        break;

                    

                    case TouchPhase.Ended:
                        break;
                }
            }
        }


        public void TriggerAnim(string s)
        {
            anim.SetTrigger(s);
        }


        public bool isLevelSelcted()
        {
            return selectedLevel != null;
        }
        

    }
}