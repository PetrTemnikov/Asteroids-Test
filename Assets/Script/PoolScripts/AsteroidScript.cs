using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AsteriodType 
{
    large,
    small,
    medium,
    special
}
public class AsteroidScript : PoolObject
{
    public int Health = 10;
    public AsteriodType AsType = AsteriodType.small;
    public float StartSpeed = 1f;
    //public bool Special = false;
    public int Score = 100;

    private bool EnteredPlayField = false;
    private SpriteRenderer mysr;

    // Start is called before the first frame update
    void Start()
    {
        mysr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mysr.isVisible)
        { 
            EnteredPlayField = true; 
        }
        if (mysr.isVisible != true && EnteredPlayField)
        {
            ReturnToPool();
        }
    }

    //Функция разрушения астероида
    public void destroy() 
    {
        switch (AsType) 
        {
            case AsteriodType.large:
                //spawn 3 small
                break;
            case AsteriodType.small:
                ReturnToPool();
                GameObject poof = PoolManager.GetObject("Poof", transform.position, transform.rotation);
                break;
            case AsteriodType.medium:
                //spawn special
                break;
            case AsteriodType.special:
                //spawn powerup or lifeup
                break;
        }
    }

    //Функция отработки попадания по этому астероиду
    public void isHit(int damage)
    {
        Health -= damage;
        if (Health <= 0) destroy();
    }

}
