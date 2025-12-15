using UnityEngine;
using System.Collections.Generic;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance { get; private set; }
    public GameObject projectilePrefab;
    public int initialSize = 20;

    Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;

        for (int i = 0; i < initialSize; i++)
        {
            var go = Instantiate(projectilePrefab, transform);
            go.SetActive(false);
            pool.Enqueue(go);
        }
    }

    public GameObject Get()
    {
        if (pool.Count == 0)
        {
            var go = Instantiate(projectilePrefab, transform);
            go.SetActive(false);
            pool.Enqueue(go);
        }
        var item = pool.Dequeue();
        return item;
    }

    public void Return(GameObject go)
    {
        go.SetActive(false);
        pool.Enqueue(go);
    }
}
