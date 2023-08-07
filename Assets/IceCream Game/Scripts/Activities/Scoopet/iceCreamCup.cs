using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceCreamCup : MonoBehaviour
{
    [SerializeField] private Transform point;
    [SerializeField] private Transform decoParent;

    [SerializeField] private GameObject[] balls;
    private int currentIndex = 0;


    public List<Transform> decoSpots;
 
    void Awake()
    {
        foreach(Transform c in decoParent)
        {
            decoSpots.Add(c);
        }
    }

 
    void Update()
    {
        
    }

    public Vector3 getPos()
    {
        return point.position;
    }

    public Vector3 getCurretBallPos(Material mat)
    {
        balls[currentIndex].GetComponent<Renderer>().material = mat;
        return balls[currentIndex].transform.position;
    }

    public void onBallReach()
    {
        balls[currentIndex].SetActive(true);
        currentIndex += 1;

        if(currentIndex > 2)
        {
            LevelMannger.instance.eventSwitch();
        }
    }
    

    public void onDropingStarted()
    {

    }



    public List<Transform> getList()
    {
        return decoSpots;
    }
}
