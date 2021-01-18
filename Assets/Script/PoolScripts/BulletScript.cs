using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : PoolObject
{
    public GameMaster GM;
    public int damage = 10;
    public float speed = 600f;

    private bool Hit = false;
    private float lifespan = 1f;
    // Start is called before the first frame update
    void Start()
    {
        if (GM == null)
            GM = GetComponentInParent<GameMaster>();
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

    //Обработка попадания пули
    void OnTriggerEnter2D(Collider2D coll)
    {
        //Debug.Log("Bullet hit something!");
        AsteroidScript AStmp;
        if (coll.gameObject.GetComponent<AsteroidScript>() != null)
        {
            AStmp = coll.gameObject.GetComponent<AsteroidScript>();
            AStmp.isHit(damage);
            GM.TheShip.Score += AStmp.Score;
            GM.GUIM.ScoreCheck();
            lifespan = 1f;
            ReturnToPool();
        }
    }
}
