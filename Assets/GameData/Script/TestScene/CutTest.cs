using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutTest : MonoBehaviour
{
    GameObject[] gameObjects;
    Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
      
    }

    public void SetCut(Vector3 pos, Vector3 Radian)
    {
       gameObjects = MeshCut.Cut(gameObject, pos, Radian, material);
    }
    public void SetCut(Vector3 pos, Vector3 Radian, Material Setmaterial)
    {
        gameObjects = MeshCut.Cut(gameObject, pos, Radian, Setmaterial);
    }
}
