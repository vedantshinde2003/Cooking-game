using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefabe;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;


    public static ObjectPool instance;

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objetpools = new Queue<GameObject>();

            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefabe);
                obj.SetActive(false);
                objetpools.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objetpools);
        }
        
    }


    public GameObject SpwanFromPool(string tag, Vector3 pos, Quaternion rot)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("nahi yeah rey add");
            return null;
        }

        GameObject objToSpwan = poolDictionary[tag].Dequeue();

        objToSpwan.SetActive(true);

        objToSpwan.transform.position = pos;
        objToSpwan.transform.rotation = rot;


        poolDictionary[tag].Enqueue(objToSpwan);

        if(objToSpwan.GetComponent<ParticleSystem>())
        {
            objToSpwan.GetComponent<ParticleSystem>().Play();
            return null;
        }
        else
        {
            return objToSpwan;
        }


        
    }

    public CFX_AutoDestructShuriken GetCFX(string t)
    {
        if(!poolDictionary.ContainsKey(t))
        {
            Debug.Log("nahi yeah rey add");
            return null;
        }

        GameObject objToSpwan = poolDictionary[t].Peek();
        return objToSpwan.GetComponent<CFX_AutoDestructShuriken>();
    }
}


