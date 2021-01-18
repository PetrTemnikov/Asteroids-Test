using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("Pool/ObjectPooling")]
public class ObjectPooling
{
    #region Data
    List<PoolObject> objects;
    Transform objectsParent;
    #endregion

    //Добавление PoolObject'a к указанному родительскому пулу
    void AddObject(PoolObject sample, Transform objects_parent)
    {
        GameObject temp = GameObject.Instantiate(sample.gameObject);
        temp.name = sample.name;
        temp.transform.SetParent(objects_parent);
        objects.Add(temp.GetComponent<PoolObject>());
        if (temp.GetComponent<Animator>())
        {
            temp.GetComponent<Animator>().StartPlayback();
            
        }
        temp.SetActive(false);
    }

    //Инициализация пула указанного количества указанного типа в указанном массиве
    public void Initialize(int Count, PoolObject sample, Transform objects_parent)
    {
        objects = new List<PoolObject>(); //List init
        objectsParent = objects_parent;// Инициализируем локальную переменную для последующего использования
        for (int i = 0; i < Count; i++)
        {
            AddObject(sample, objects_parent); // Создаем объекты до указанного количества
        }
    }

    //Получить последний объект в пуле
    public PoolObject GetObject()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i].gameObject.activeInHierarchy == false)
            {
                return objects[i];
            }
        }
        AddObject(objects[0], objectsParent);
        return objects[objects.Count - 1];
    }

    //Получить весь список объектов
    public List<PoolObject> GetAllObjects()
    {
        List<PoolObject> lpo = new List<PoolObject>(objects);
        return lpo;
    }

    //Получить количество объектов в пуле
    public int GetPoolSize()
    {
        return objects.Capacity;
    }
}

