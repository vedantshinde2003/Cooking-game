using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopicMachine : interactables
{
    [SerializeField] private Transform handel;
    [SerializeField] private MeshFilter desplay;
    [SerializeField] private iceCreamCup currentIceCream;

    [SerializeField] private ParticleSystem vfx;
    [SerializeField] private ParticleSystemRenderer particleRenderer;
    [SerializeField] private Mesh[] meshes;
    public Mesh selectedMesh;

    

    private bool isHolding = false;


    [SerializeField] private float fireRate = 1;
    private bool invoke = false;

    public List<Transform> spots;
    public int currentIndex = 0;
    private bool isCompleted = false;

    void Start()
    {
       setUpthis();

       selectedMesh = meshes[0];
    }


    public void setUpthis()
    {
        spots = currentIceCream.getList();  
    }


    public void setUpMesh(Component sender, object data)
    {
        if(data is int)
        {
            int index = (int) data;
            selectedMesh = meshes[index];
            desplay.mesh = selectedMesh;
            SetParticleMesh(selectedMesh);
        }
    }



    private void Update()
    {
        if(isHolding && !isCompleted)
        {
            if(!invoke)
            {
                invoke = true;
                StartCoroutine(firReset());

                spots[currentIndex].gameObject.SetActive(true);
                spots[currentIndex].GetComponent<MeshFilter>().mesh = selectedMesh;

                currentIndex += 1;

                if(currentIndex >= spots.Count)
                {
                    if(!isCompleted)
                    {
                        isCompleted = true;
                        LevelMannger.instance.eventSwitch();
                    }
                }
            }
        }
    }
  
    private void SetParticleMesh(Mesh mesh)
    {
        if (mesh != null)
        {
            particleRenderer.mesh = mesh;
        }
        else
        {
            Debug.LogWarning("Invalid mesh provided. The Particle System mesh will not be changed.");
        }
    }




    public override void onDeselected()
    {
        base.onDeselected();

        vfx.Stop();
        Quaternion rot = Quaternion.Euler(0,0,45);
        isHolding = false;
        rotateObject(handel,rot);
    }

    public override void onSelected()
    {
        base.onSelected();

        Quaternion rot = Quaternion.Euler(0,0,135);
        isHolding = true;
        vfx.Play();
        rotateObject(handel,rot);

    }




    private IEnumerator firReset()
    {
        yield return new WaitForSeconds(fireRate);
        invoke = false;
    }
   

}
