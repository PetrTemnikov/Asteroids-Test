using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public float SpawnPeriod = 1f;
    public float SpawnTime = 1f;
    public GameMaster GM;

    private GameObject Asteroid; //Потом надо сделать массив на выбор астероидов
    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > SpawnTime)
        {
            SpawnTime = Time.time + SpawnPeriod;
            SpawnAsteroid();
        }
    }

    //Заспавнить астероид
    void SpawnAsteroid()
    {
        //Debug.Log("SpawnAsteroid");
        pos = GM.GUIM.RandomPlaceForAsteroid();
        Asteroid = PoolManager.GetObject("Asteroid_s1", pos, Quaternion.identity);
        //Debug.Log(Asteroid);
        Asteroid.GetComponent<Rigidbody2D>().AddForce(-pos * Asteroid.GetComponent<AsteroidScript>().StartSpeed, ForceMode2D.Impulse);
    }
}
