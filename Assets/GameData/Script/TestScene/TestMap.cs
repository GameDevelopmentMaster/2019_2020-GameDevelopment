using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMap : MonoBehaviour
{
    public GameObject room;
    public GameObject roomParent;
    public GameObject mapObject;
    public GameObject lobby;
    GameObject[] gameObjects;
    MapNode map;
    // Start is called before the first frame update
    void Awake()
    {
        map = new MapNode();

        map.MapCut(6);
        gameObjects = new GameObject[map.SetPos().Length];
        GameObject[] roomType = new GameObject[map.SetPos().Length];
        for(int i=0; i<gameObjects.Length; i++)
        {
            if(i%2 == 0)
            {
                roomType[i] = new GameObject("LastNode " + i.ToString());
                roomType[i].transform.parent = roomParent.transform;
            }
            gameObjects[i] = Instantiate(room,roomParent.transform.GetChild(i/2));
            gameObjects[i].name = i.ToString();
            gameObjects[i].transform.localScale = map.SetSize()[i];
            gameObjects[i].transform.position = map.SetPos()[i];
            if(i %2 == 1)
            {
                GameObject lobbyObject = Instantiate(lobby);
                lobbyObject.transform.parent = roomParent.transform.GetChild(i / 2);
                //왼쪽 노드의 포지션 값 + 왼쪽 노드의 크기 값/2 + 로비 오브젝트 크기 값/2
                //포지션은 오브젝트 중앙에 위치 하기 때문에 크기의 절반과 로비 오브젝트 크기의 절반만큼 더해주면 정확하게 붙음
                if (gameObjects[i-1].transform.position.x != gameObjects[i].transform.position.x)
                {
                    lobbyObject.transform.localScale = new Vector3(Mathf.Abs((gameObjects[i - 1].transform.position.x - gameObjects[i].transform.position.x) / 2f), 10, 5);
                    lobbyObject.transform.position = new Vector3(gameObjects[i - 1].transform.position.x + gameObjects[i - 1].transform.localScale.x/2f + lobbyObject.transform.localScale.x/2f, 1, gameObjects[i-1].transform.position.z);
                }
                else
                {
                    lobbyObject.transform.localScale = new Vector3(5, 10,Mathf.Abs( (gameObjects[i - 1].transform.position.z - gameObjects[i].transform.position.z) / 2f));
                    lobbyObject.transform.position = new Vector3(gameObjects[i-1].transform.position.x, 1, gameObjects[i-1].transform.position.z + lobbyObject.transform.localScale.z/2f + gameObjects[i-1].transform.localScale.z/2f );
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    
}
