using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoofScript : PoolObject
{
    public float lifespan = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;
        if (lifespan < 0f)
        {
            lifespan = 1f;
            ReturnToPool();
        }
    }
}
