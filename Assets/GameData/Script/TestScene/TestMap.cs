using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMap : MonoBehaviour
{
    public GameObject room;
    public GameObject mapObject;
    GameObject[] gameObjects;
    MapNode map;
    // Start is called before the first frame update
    void Awake()
    {
        map = new MapNode();

        map.MapCut(6);
        gameObjects = new GameObject[map.SetPos().Length];
        for(int i=0; i<gameObjects.Length; i++)
        {
            gameObjects[i] = Instantiate(room,Vector3.zero,Quaternion.identity);
            gameObjects[i].name = i.ToString();
            gameObjects[i].transform.localScale = map.SetSize()[i];
            gameObjects[i].transform.position = map.SetPos()[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    
}
