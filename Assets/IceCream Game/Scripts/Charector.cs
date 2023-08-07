using UnityEngine;
using System.Collections;


public class Charector : MonoBehaviour
{   
    [SerializeField] private bool preferAny_desh;
    [SerializeField] private int desh_index;
    [SerializeField]  private Animator anim;
    private Rigidbody rb;

    private Vector3 currentTarget;
    private Vector3 initialPosition;
    private float initialTime;



    private enum state
    {
        order,waiting,eating,leave
    }

    [SerializeField] private state current_state;

  
    [SerializeField] private Transform shopCounter;
    [SerializeField] private Transform[] exits;

    [SerializeField] private Transform checkSphere;
    [SerializeField] private Transform handPam;
    [SerializeField] private LayerMask whatIsChartector;





    private Transform myexit;




    [SerializeField] private GameEvent orderEvets;



    
    void Start()
    {
        currentTarget = shopCounter.position;

        myexit = exits[Random.Range(0,exits.Length)];

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        switch(current_state)
        {
            case state.order:
                currentTarget = shopCounter.position;
                if(Vector3.Distance(transform.position,shopCounter.position) > 0.1f)
                {
                    if(Physics.CheckSphere(checkSphere.position,1,whatIsChartector))
                    {

                    }
                    else
                    {
                        transform.position = Vector3.MoveTowards(transform.position,currentTarget,2 * Time.deltaTime);

                    }       
                }
                else
                {
                    anim.SetTrigger("introduce");
                   // orderEvets.Raise();
                    current_state = state.waiting;
                    giveOrder();

                }
                break;

        
            case state.leave:
                currentTarget = myexit.position;
                if(Vector3.Distance(transform.position,currentTarget) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position,currentTarget,2 * Time.deltaTime);
                    transform.LookAt(myexit);
                }  
                else
                {
                    Destroy(gameObject);
                }     
                
                break;
            
            
        }

        anim.SetFloat("speed",GetSpeed()/10);
    }


    public void giveOrder()
    {
        StartCoroutine(orderRutine());
    }

    private IEnumerator orderRutine()
    {
        yield return new WaitForSeconds(3);
        if(preferAny_desh)
        {
            DeshSelectionManger.instance.showPad();
        }
        else
        {
            DeshSelectionManger.instance.onDeshSelected(desh_index);
        }
    }






    public float GetSpeed()
    {
        // Calculate the displacement
        Vector3 currentPosition = transform.position;
        float displacement = Vector3.Distance(initialPosition, currentPosition);

        // Calculate the elapsed time
        float elapsedTime = Time.time - initialTime;

        // Calculate the speed (displacement per unit time)
        float speed = displacement / elapsedTime;
        return speed;
    }



















    public void startEating()
    {
        if(current_state != state.eating)
        {
            anim.SetTrigger("eat");
            current_state = state.eating;
        }
      
    }



    public Transform GetParm()
    {
        return handPam;
    }

    
    public  void OnEatingFinshed()
    {
        Destroy(handPam.GetChild(0).gameObject);
    }

    public void leave()
    {
        current_state = state.leave;
    }
   
        
    
}
