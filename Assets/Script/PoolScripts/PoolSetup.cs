using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Pool/BulletPool")]
public class PoolSetup : MonoBehaviour
{//Для управления static классом PoolManager.
    #region Unity scene settings
    [SerializeField]
    private PoolManager.PoolPart[] pools;
    #endregion

    private GameMaster GameMaster;

    #region Methods

    //Проверка соответствия имён
    void OnValidate()
    {
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i].name = pools[i].prefab.name;
        }
    }
    #endregion
    void Awake()
    {
        //GameMaster = GetComponentInParent<GameMaster>();
        Initialize();
    }

    void Initialize()
    {
        PoolManager.Initialize(pools);
    }


}
