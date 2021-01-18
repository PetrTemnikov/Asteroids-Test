using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PoolObject
{
    public GameMaster GM;
    public float FireRate = 1f;//1 bps default
    public Transform MuzzlePoint;
    public Rigidbody2D myBody;
    public SpriteRenderer exhaust;
    public float speed = 10f;
    public int Score = 0;
    public GUIMaster GUIM;

    
    private int Lives = 3;
    private float fireTime;
    private SpriteRenderer mysr;
    private bool isWrappingX = false;
    private bool isWrappingY = false;

    

    void Awake()//Вызов на первом его кадре
    {

            
    }
    // Start is called before the first frame update
    void Start()
    {
        if (GM == null)
        {
            GM = GetComponentInParent<GameMaster>();
        }
        if (GUIM == null)
        {
            GUIM = GetComponentInParent<GUIMaster>();
        }
        GUIM.LifeCheck();
        GUIM.ScoreCheck();
        if (!mysr)
            mysr = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //Управление клавиатурой
        if (GM.CurrentPlatformType == PlatformType.PC)
        {
            Vector2 tmpV2;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                tmpV2.x = 0f;
                tmpV2.y = 1f;
                myBody.AddRelativeForce(tmpV2 * speed);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                tmpV2.x = 0f;
                tmpV2.y = -0.5f;
                myBody.AddRelativeForce(tmpV2 * speed);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                myBody.AddTorque(.1f * speed);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                myBody.AddTorque(-.1f * speed);
            }

            if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) && Time.time > fireTime)
            {
                fireTime = Time.time + FireRate; // Remake as in asteroids


                //    //GameObject bullet = GM.objectPool.GetPooledObject();
                //    tmpV3 = transform.position;// new Vector3(transform.position);
                //tmpV3.y = transform.position.y + .1f;
                GameObject bullet = PoolManager.GetObject("Bullet", MuzzlePoint.position, transform.rotation/*Quaternion.identity*/);
                bullet.GetComponent<Rigidbody2D>().AddForce(transform.up/*.forward*/ * bullet.GetComponent<BulletScript>().speed); 
            }

            ScreenWrap();
        }//if (GM.CurrentPlatformType == PlatformType.PC)
    }//update

    //Функция переброса корабля с одного края на другой
    void ScreenWrap()
    {
        Vector3 viewportShipPos;
        if (mysr.isVisible)
        {
            isWrappingX = false;
            isWrappingY = false;
        }

        if (isWrappingX && isWrappingY) return;

        viewportShipPos = GUIM.GetViewportPosition(transform.position);
        Vector3 newPosition = transform.position;
        if (!isWrappingX && (viewportShipPos.x > 1 || viewportShipPos.x < 0))
        {
            newPosition.x = -newPosition.x;
            isWrappingX = true;
            //Debug.Log("isWrappingX");
        }
        if (!isWrappingY && (viewportShipPos.y > 1 || viewportShipPos.y < 0))
        {
            newPosition.y = -newPosition.y;
            isWrappingY = true;
            //Debug.Log("isWrappingY");
        }
        transform.position = newPosition;

    }//ScreenWrap

    //Функция обработки столкновений
    void OnCollisionEnter2D(Collision2D coll)
    {
        //Debug.Log("Ship Collided");
        AsteroidScript AStmp;
        if (coll.gameObject.GetComponent<AsteroidScript>() != null)
        {
            AStmp = coll.gameObject.GetComponent<AsteroidScript>();
            AStmp.destroy();
            isHit();
        }
    }

    //Функция повреждения корабля
    void isHit()
    {
        Lives--;
        if (Lives <= 0) destroy();
        GUIM.LifeCheck();
    }

    //Функция уничтожения корабля
    void destroy()
    {
        //Сообщить о проигрыше
        myBody.simulated = false;
        ReturnToPool();
        GameObject poof = PoolManager.GetObject("Poof", transform.position, transform.rotation);
    }

    //Получить количество жизней
    public int GetLives()
    {
        return Lives;
    }

    //Функция перезапуска корабля
    public void Restart() 
    {
        Score = 0;
        Lives = 3;
        transform.position = new Vector3(0, 0, 0);

        myBody.angularVelocity = 0f;
        myBody.velocity = Vector2.zero;
        transform.rotation = Quaternion.identity;
    }
}
