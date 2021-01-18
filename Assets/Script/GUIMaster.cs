using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GUIMaster : MonoBehaviour
{
    public Camera MainCamera;
    public List<Image> Lifes;
    public Text ScoreText;
    public GameMaster GM;
    public GameObject Background;//Игровое поле

    private float maximumScreenDimension;
    private Vector3 OO;
    private Vector3 II;


    // Start is called before the first frame update
    void Start()
    {
        OO = MainCamera.ViewportToWorldPoint(Vector3.zero);
        II = MainCamera.ViewportToWorldPoint(Vector3.one);

        Background.transform.localScale = new Vector3(Screen.width / 64 + 5, Screen.height / 64 + 5, 1);
        if (II.x >= II.y)
            maximumScreenDimension = II.x * 1.3f;
        else maximumScreenDimension = II.y * 1.3f;

    }

    //void Awake() //Первичные инициализации пройдены
    //{

    //}

    // Update is called once per frame
    void Update()
    {

    }

    //Функция выбора случайного места для астероида. Вне поля видимости, но в зоне быстрого долёта астероидов. 
    public Vector3 RandomPlaceForAsteroid()
    {
        Vector3 result;// = new Vector3(Random.value, Random.value, 0);
        //result = MainCamera.ViewportToWorldPoint(result);
        result = Random.insideUnitCircle * maximumScreenDimension;

        if ((result.x <= II.x && result.x >= OO.x) || (result.y <= II.y && result.y >= OO.y))//Астероид хочет заспауниться внутри поля
        {
            if(System.Math.Abs(result.x)> System.Math.Abs(result.y))
            {
                if (result.y >= 0)
                {
                    //Debug.Log("Upper side");
                    result.y = II.y + .3f;
                }
                else
                {
                    //Debug.Log("Lower side");
                    result.y = OO.y - .3f;
                } 
            }
            else
            {
                if (result.x >= 0)
                {
                    //Debug.Log("Right side");
                    result.x = II.x + .3f;
                }
                else 
                {
                    //Debug.Log("Left side");
                    result.x = OO.x - .3f;
                }
            }
        }
        //Debug.Break();
        result.z = transform.position.z;
        return result;
    }

    //Функция получения координат объекта находящегося в поле видимости камеры
    public Vector3 GetViewportPosition(Vector3 pos)
    {   
        Vector3 vpos = MainCamera.WorldToViewportPoint(pos);
        return vpos;
    }

    //Функция привода отображаемых жизней в соответствие с количеством жизней корабля
    public void LifeCheck()
    {
        //Debug.Log("LifeCheck");
        int lifeCounter = 0;
        foreach (Image lifeIcon in Lifes)
        {
            if (lifeCounter > /*GetComponent<GameMaster>()*/GM.TheShip.GetLives()-1)
                 lifeIcon.enabled = false;
            else lifeIcon.enabled = true;
            lifeCounter++;
        }
    }

    //Функция привода отображаемых очков в соответствие со счётом игрока
    public void ScoreCheck()
    {
        ScoreText.text = /*GetComponent<GameMaster>()*/GM.TheShip.Score.ToString();
    }
}
