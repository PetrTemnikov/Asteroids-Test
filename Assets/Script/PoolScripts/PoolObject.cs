using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Pool/PoolObject")]
public class PoolObject : MonoBehaviour 
{
    //Вернуть объект в пул
    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}




