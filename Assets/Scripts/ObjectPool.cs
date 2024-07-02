using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    T prefab;

    private List<T> pool = new List<T>();

    public ObjectPool(T newPrefab)
    {
        prefab = newPrefab;
    }

    public void DisableAll()
    {
        foreach (var item in pool)
        {
            item.gameObject.SetActive(false);
        }
    }

    public T GetItem()
    {
        foreach (var item in pool)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                return item;
            }
        }

        return CreateItem();
    }

    private T CreateItem()
    {
        var newItem = GameObject.Instantiate(prefab);
        newItem.gameObject.SetActive(false);
        pool.Add(newItem);
        return newItem;
    }

    public List<T> GetActiveObjects()
    {
        return pool.Where(x => x.gameObject.activeInHierarchy).ToList();
    }
}
