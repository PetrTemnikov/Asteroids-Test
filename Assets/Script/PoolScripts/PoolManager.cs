using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public static class PoolManager
{
    private static PoolPart[] pools;    
    private static GameObject objectsParent;

    [System.Serializable]
    public struct PoolPart
    {
        public string name;             //Имя Pool’a
        public PoolObject prefab;       //Префаб, который должен быть наследником класса PoolObject
        public int count;               //Количество объектов внутри пула
        public ObjectPooling ferula;    //Сам пул пулов
        public GameObject subPoolGOInstance;
    }

    //Инициализирует все пулы переданные в аргументе и записывает их в пул пулов - в статичный массив pools, а так же в GameObject
    public static void Initialize(PoolPart[] newPools)
    {
        pools = newPools;
        objectsParent = new GameObject();
        objectsParent.name = "MasterPool";
        objectsParent.transform.SetParent(GameObject.Find("GameMaster").transform);

        //Debug.Log(pools.Length);

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i].subPoolGOInstance = new GameObject();
            pools[i].subPoolGOInstance.name = newPools[i].prefab.name;
            pools[i].name = newPools[i].prefab.name;
            pools[i].subPoolGOInstance.transform.SetParent(objectsParent.transform);

            if (pools[i].prefab != null)
            {
                pools[i].ferula = new ObjectPooling();
                pools[i].ferula.Initialize(pools[i].count, pools[i].prefab, pools[i].subPoolGOInstance.transform);
            }
        }
        //Debug.Break();
    }

    public static void Initialize(PoolPart[] newPools, string i_name, int i_count )
    { }

    //Возврат всех объектов во всех пулах в пулы
    public static void TotalRecall()
    {
        for (int i = 0; i < pools.Length; i++)
        {
            List<PoolObject> lpo =
            pools[i].ferula.GetAllObjects();

            foreach (PoolObject po in lpo)
            {
                po.ReturnToPool();
            }
        }
    }

    //Создание объектов по указанному имени, с вводом координат
    public static GameObject GetObject(string name, Vector3 position, Quaternion rotation)
    {
        GameObject result = null;
        if (pools != null)
        {
            for (int i = 0; i < pools.Length; i++)
            {
                if (string.Compare(pools[i].name, name) == 0)//Имена искомого в пулах и имя текущего пула совпали
                {
                    result = pools[i].ferula.GetObject().gameObject;
                    result.transform.position = position;
                    result.transform.rotation = rotation;
                    //result.transform.localScale = new Vector3(1, 1, 1); //Размер оставляем такой же как в префабе
                    result.SetActive(true);
                    //Debug.Log("PoolManager::GetObject resulted good\n");
                    return result;
                }
            }
        }
        Debug.Log("PoolManager::GetObject resulted with 0\n");
        return result;
    }
}

