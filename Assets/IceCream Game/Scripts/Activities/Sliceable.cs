using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class Sliceable : interactables
{
     [Header("child ")]
    [SerializeField]
    private bool _isSolid = true;

    [SerializeField]
    private bool _reverseWindTriangles = false;

    [SerializeField]
    private bool _useGravity = false;

    [SerializeField]
    private bool _shareVertices = false;

    [SerializeField]
    private bool _smoothVertices = false;

    public bool IsSolid
    {
        get
        {
            return _isSolid;
        }
        set
        {
            _isSolid = value;
        }
    }

    public bool ReverseWireTriangles
    {
        get
        {
            return _reverseWindTriangles;
        }
        set
        {
            _reverseWindTriangles = value;
        }
    }

    public bool UseGravity 
    {
        get
        {
            return _useGravity;
        }
        set
        {
            _useGravity = value;
        }
    }

    public bool ShareVertices 
    {
        get
        {
            return _shareVertices;
        }
        set
        {
            _shareVertices = value;
        }
    }

    public bool SmoothVertices 
    {
        get
        {
            return _smoothVertices;
        }
        set
        {
            _smoothVertices = value;
        }
    }



    [SerializeField] private Material inside;


    public override void setup()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        transform.tag = "cutable";
    }

  
    void Update()
    {
        
    }


    public override void onSelected()
    {
        base.onSelected();
    }

    public override void onDeselected()
    {
        base.onDeselected();
    }

    public Material GetMaterial()
    {
        return inside;
    }

}
