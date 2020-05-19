using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowPoolControl : MonoBehaviour
{
    public static shadowPoolControl instance;

    public GameObject shadowPrefab;

    int shadowCount = 8;
    private Queue<GameObject> availableObj = new Queue<GameObject>();

    private void Awake()
    {
        instance = this;
        //初始化对象池
        FillPool();

    }

    public void FillPool()
    {
        for(int i = 0; i < shadowCount; i++)
        {
            var newShadow = Instantiate(shadowPrefab);
            newShadow.transform.SetParent(transform);
            //取消启用，返回对象池
            ReturnPool(newShadow);
        }
    }

    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        availableObj.Enqueue(gameObject);
    }

    public GameObject GetFromPool()
    {
        if(availableObj.Count == 0)
        {
            FillPool();
        }
        var outShadow = availableObj.Dequeue();

        outShadow.SetActive(true);
        return outShadow;
    }
}
