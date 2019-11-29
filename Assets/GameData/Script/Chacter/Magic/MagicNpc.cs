using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicNpc : Character
{
    float timer = 0;
    public MagicDraw magic;
    GameObject[] magicdraw = new GameObject[5];
    public Material magicdrawMaterial;
    public GameObject magicCircleImage;
    bool _bDestroy = false;
    // Start is called before the first frame update
    void Start()
    {
        CharacterInit(CharacterList.magic);
        magic = transform.GetChild(0).GetComponent<MagicDraw>();
        MagicDrawing();
    }

    // Update is called once per frame
    void Update()
    {
        if(CharacterData.maxHp != CharacterData.hp)
        {
            Debug.Log(CharacterData.hp);
        }
    }

    void StatDraw()
    {
        Mesh mesh = new Mesh();
    }
    public void MagicDrawing()
    {
        Vector3 Pos = transform.position + transform.up * 5;
        for (int i = 0; i < magicdraw.Length; i++)
        {
            if (magicdraw[i] == null)
            {
                magicdraw[i] = new GameObject();
                magicdraw[i].AddComponent<LineRenderer>().startWidth = 0.1f;
                magicdraw[i].GetComponent<LineRenderer>().endWidth = 0;
                magicdraw[i].GetComponent<LineRenderer>().positionCount = 2;
                magicdraw[i].GetComponent<LineRenderer>().material = magicdrawMaterial;
            }
        }
        magicdraw[0].transform.GetComponent<LineRenderer>().SetPosition(0, Pos + transform.up); ;
        magicdraw[1].transform.GetComponent<LineRenderer>().SetPosition(0, Pos + transform.right - transform.up);
        magicdraw[2].transform.GetComponent<LineRenderer>().SetPosition(0, Pos - (transform.right * 3) / 2);
        magicdraw[3].transform.GetComponent<LineRenderer>().SetPosition(0, Pos + (transform.right * 3) / 2);
        magicdraw[4].transform.GetComponent<LineRenderer>().SetPosition(0, Pos - transform.right - transform.up);
        magic.SetPos(0, Pos);
        StartCoroutine(MagicDrawCircle(magicdraw));
        transform.GetChild(0).gameObject.SetActive(false);

    }
    IEnumerator MagicDrawCircle(GameObject[] gameObjects)
    {
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime * 0.7f;
            for (int i = 1; i < gameObjects.Length; i++)
            {
                gameObjects[i - 1].transform.GetComponent<LineRenderer>().SetPosition(1, Vector3.Lerp(gameObjects[i - 1].transform.GetComponent<LineRenderer>().GetPosition(0), gameObjects[i].transform.GetComponent<LineRenderer>().GetPosition(0), timer));
                gameObjects[4].transform.GetComponent<LineRenderer>().SetPosition(1, Vector3.Lerp(gameObjects[4].transform.GetComponent<LineRenderer>().GetPosition(0), gameObjects[0].transform.GetComponent<LineRenderer>().GetPosition(0), timer));
            }
            yield return null;
        }
        Vector3[] vector3s = new Vector3[2];
        vector3s[0] = vector3s[1] = Vector3.zero;
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].GetComponent<LineRenderer>().SetPositions(vector3s);
        }
        timer = 0;
        magic.DataReset();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && !_bDestroy)
        {
            if (CharacterData.hp <= 0)
            {
                _bDestroy = true;
                GameObject[] meshList = MeshCut.Cut(gameObject, collision.transform.position, Vector3.Cross(collision.transform.position, 
                                                    transform.position).normalized, GetComponent<MeshRenderer>().material);
                meshList[1].transform.localScale = meshList[0].transform.localScale;
                meshList[1].AddComponent<MeshCollider>().convex = true;
                for (int i = 0; i < meshList.Length; i++)
                {
                    meshList[i].AddComponent<Rigidbody>();
                    meshList[i].GetComponent<MeshCollider>().sharedMesh = meshList[i].GetComponent<MeshFilter>().mesh;
                    StartCoroutine(DestroyObject(meshList[i]));
                }
            }
        }
    }

    IEnumerator DestroyObject(GameObject destroyObject)
    {
        while (destroyObject.transform.localScale.x > 0.3f)
        {
            destroyObject.transform.localScale -= destroyObject.transform.localScale.normalized * Time.deltaTime;
            yield return null;
        }
        Destroy(destroyObject);
    }
}
