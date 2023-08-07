using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelMannger : MonoBehaviour
{
    public int currentIndex = 0;
    [SerializeField] private Scenario[] scenarios;
   

    public static LevelMannger instance;

   

    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        StartCoroutine(loadNextSceneRutine());
    }

    

    public void eventSwitch()
    {
        currentIndex += 1;


        if(currentIndex < scenarios.Length)
        {
            StartCoroutine(loadNextSceneRutine());
        }
        else
        {
            print("all evets done");
        }
    }

 


    public void resetCameraFollow()
    {
        CameraMovments.instnace.resetSetTargetFollow();
    }

    public void setCameraFov(float f)
    {
        CameraMovments.instnace.changeFov(f);
    }




    private IEnumerator loadNextSceneRutine()
    {



        Scenario currentScene = scenarios[currentIndex];

        yield return new WaitForSeconds(currentScene.startDelay);

       
        if(currentScene.playVfx_onremove)
        {
            foreach(GameObject g in currentScene.elementsToRemove)
            {
                if(g.GetComponent<DissolveController>())
                {
                    g.GetComponent<DissolveController>().StartDissolve();
                }
                else
                {
                    g.AddComponent<DissolveController>().StartDissolve();
                }
                
            }

            yield return new WaitForSeconds(2);
        }

       

        foreach(GameObject g in currentScene.elementsToRemove)
        {
            g.SetActive(false);
        }
         foreach(GameObject g in currentScene.elementsToAdd)
        {
            g.SetActive(true);
        }


        if(currentScene.Scenario_parent != null)
        {
            currentScene.Scenario_parent.SetActive(true);
        }

        
        if(currentScene.removePrivous_scenario)
        {
            if(scenarios[currentIndex - 1].Scenario_parent != null)
                scenarios[currentIndex - 1].Scenario_parent.SetActive(false);
            else
                print("no Privious Scenario Object");
        }


      

        

        switch(currentScene.camera.actions)
        {
            case Scenario.cameraActions.AxesShift:
                CameraMovments.instnace.changeCamera(currentScene.camera.cameraTarget.position,0.5f,currentScene.camera.zoom);
                break;

            case Scenario.cameraActions.normalShift:
                CameraMovments.instnace.normalShift(currentScene.camera.cameraTarget.position,0.5f);
                break;

            case Scenario.cameraActions.TrackShift:
                CameraMovments.instnace.TrackShift(currentScene.camera.cameraTarget.position,currentScene.camera.TrackShift_y,0.5f, currentScene.camera.zoom);
                break;
            
            case Scenario.cameraActions.setFollow:
                CameraMovments.instnace.SetTargetFollow(currentScene.camera.cameraTarget,currentScene.camera.zoom);
                break;
                
            case Scenario.cameraActions.none:
                break;
        }

        if(currentScene.camera.changeFov)
        {
            CameraMovments.instnace.changeFov(currentScene.camera.zoom);
        }



        
    
        if(currentScene.Event != null)
        {
            currentScene.Event.Raise();
        }


        yield return null;
    }
}


[System.Serializable]
public class Scenario
{
    public string title;
    public GameObject Scenario_parent;

    public float startDelay = 0;
    public bool removePrivous_scenario;
    public GameObject[] elementsToRemove;
    public bool playVfx_onremove;
    public GameObject[] elementsToAdd;
    public GameEvent Event;




    [System.Serializable]
    public class cameraSettings
    {
        public string camera;
        public Transform cameraTarget;
        public float zoom;
        public float TrackShift_y = 1.4f;
        
        public cameraActions actions;
        public bool changeFov;
    }

    public cameraSettings camera;


    public enum cameraActions
    {
        none, normalShift, AxesShift ,setFollow, TrackShift
    }
    
}

