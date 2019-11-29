using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Vector3 outPos;
    float timer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public void InMove()
    {
        StopAllCoroutines();
        StartCoroutine(InMoveCourtin());
    }
    public void OutMove()
    {
        StopAllCoroutines();
        StartCoroutine(OutMoveCourtin());
    }
     IEnumerator InMoveCourtin()
    {
        outPos = transform.position;
        for(int i=0; timer < 1; i++)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(outPos, transform.parent.position, timer);
            yield return null;
        }
    }

    IEnumerator OutMoveCourtin()
    {
        for (int i = 0; timer >0; i++)
        {
            timer -= Time.deltaTime;
            transform.position = Vector3.Lerp(outPos, transform.parent.position, timer);
            yield return null;
        }
    }
}
