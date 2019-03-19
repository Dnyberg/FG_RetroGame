using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public GameObject ObjectToPool;
    public string PoolName;
    public int AmountToPool;
    public bool ShouldExpand;
}

public class TheCoolerObjectPooler : MonoBehaviour
{
    public const string DefaultRootObjectPoolName = "Pooled Objects";

    public static TheCoolerObjectPooler SharedInstance;
    public string RootPoolName = DefaultRootObjectPoolName;
    public List<ObjectPoolItem> ItemsToPool;
    public List<GameObject> PooledObjects;


    void Awake()
    {
        SharedInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (string.IsNullOrEmpty(RootPoolName))
            RootPoolName = DefaultRootObjectPoolName;

        GetParentPoolObject(RootPoolName);

        PooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem Item in ItemsToPool)
        {
            for (int i = 0; i < Item.AmountToPool; i++)
            {
                GameObject Obj = (GameObject)Instantiate(Item.ObjectToPool);
                Obj.SetActive(false);
                PooledObjects.Add(Obj);
            }
        }
    }

    private GameObject GetParentPoolObject(string ObjectPoolName)
    {
        // Use the root object pool name if no name was specified
        if (string.IsNullOrEmpty(ObjectPoolName))
            ObjectPoolName = RootPoolName;

        GameObject ParentObject = GameObject.Find(ObjectPoolName);

        // Create the parent object if necessary
        if (ParentObject == null)
        {
            ParentObject = new GameObject();
            ParentObject.name = ObjectPoolName;

            // Add sub pools to the root object pool if necessary
            if (ObjectPoolName != RootPoolName)
                ParentObject.transform.parent = GameObject.Find(RootPoolName).transform;
        }

        return ParentObject;
    }

    public GameObject GetPooledObject(string Tag)
    {    
        for (int i = 0; i < PooledObjects.Count; i++)
        {         
            if (!PooledObjects[i].activeInHierarchy && PooledObjects[i].tag == Tag)
            {
                return PooledObjects[i];
            }
        }

        foreach (ObjectPoolItem Item in ItemsToPool)
        {
            if (Item.ObjectToPool.tag == Tag)
            {
                if (Item.ShouldExpand)
                {
                    GameObject Obj = (GameObject)Instantiate(Item.ObjectToPool);
                    Obj.SetActive(false);
                    PooledObjects.Add(Obj);
                    return Obj;
                }
            }
        }
        return null;
    }

    private GameObject CreatePooledObject(ObjectPoolItem Item)
    {
        GameObject Obj = Instantiate<GameObject>(Item.ObjectToPool);

        // Get the parent for this pooled object and assign the new object to it
        var ParentPoolObject = GetParentPoolObject(Item.PoolName);
        Obj.transform.parent = ParentPoolObject.transform;

        Obj.SetActive(false);
        PooledObjects.Add(Obj);
        return Obj;
    }
}

