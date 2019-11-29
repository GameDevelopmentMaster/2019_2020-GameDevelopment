using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTestScneen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject[] meshCut = MeshCut.Cut(collision.gameObject, transform.position, Vector3.Cross(transform.position, collision.transform.position).normalized, collision.transform.GetComponent<MeshRenderer>().material);
        for(int i=0; i<meshCut.Length; i++)
        {
            meshCut[i].AddComponent<Rigidbody>();
        }
    }
}
