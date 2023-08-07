using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using EasyTransition;

namespace selectionScreen
{
    public class LevelBox : MonoBehaviour
    {
       [SerializeField] private Transform animationSpot;
       [SerializeField] private string animationTrigger;

       [SerializeField] private string sceneName;
       [SerializeField] private float waitingTime = 3f;


        private float currentTime = 0;
        private bool invoke = false;



        [SerializeField] private TransitionSettings transition;
        public float startDelay;

        
      


       private void OnTriggerEnter(Collider col)
       {
            if(col.GetComponent<SimpleControl>())
            {
                currentTime = 0;
            }
       }

       private void OnTriggerStay(Collider col)
       {
            if(col.GetComponent<SimpleControl>())
            {
                if(currentTime < waitingTime)
                {
                    currentTime += Time.deltaTime;
                }
                else
                {
                    if(!invoke)
                    {
                        invoke = true;
                        LoadLevel();
                    }
                }
            }
       }

        public void LoadLevel()
        {
            TransitionManager.Instance().Transition(sceneName, transition, startDelay);
        }

        


        public Transform getPoint()
        {
            return animationSpot;
        }
    
      
    }
}