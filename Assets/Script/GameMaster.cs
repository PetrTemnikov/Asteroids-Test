using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformType //Тип платформы, по умолчанию PC
{
    PC,
    PC_mouse_only,
    Smartphone
}

public class GameMaster : MonoBehaviour
{
    public PlatformType CurrentPlatformType = PlatformType.PC;
    public PoolSetup PoolSetup;
    public GUIMaster GUIM;
    public Player TheShip;
    
    // Start is called before the first frame update
    void Start()
    {
        //if (GUIM == null)
        //{
        //    GUIM = GetComponent<GUIMaster>();
        //}
        //if (!TheShip)
        //{
        TheShip = PoolManager.GetObject("TheShip", Vector3.zero, Quaternion.identity).GetComponent<Player>();
        //}
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Restart();
        }
    }

    public void Restart()
    {
        //Должен ли удалять все астероиды и пули с поля?

        //Debug.Log("Restart");
        if(!TheShip.isActiveAndEnabled)
        TheShip = PoolManager.GetObject("TheShip", Vector3.zero, Quaternion.identity).GetComponent<Player>();
        if (!TheShip.GetComponent<Rigidbody2D>().simulated) TheShip.GetComponent<Rigidbody2D>().simulated = true;
        TheShip.Restart();
        GUIM.LifeCheck();
        GUIM.ScoreCheck();
    }

}
