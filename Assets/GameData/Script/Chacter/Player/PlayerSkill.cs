using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public bool skilCheck;
    LineRenderer laser;
    public float timer=0;

    // Start is called before the first frame update
    void Start()
    {
        laser = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (skilCheck)
        {
            if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2))
            {
                laser.SetPosition(0, transform.position);
                laser.SetPosition(2, (transform.right + transform.forward).normalized * timer + transform.position);
                timer += Time.deltaTime*3f;
            }
            if (Input.GetKeyUp(KeyCode.Keypad2) || Input.GetKeyUp(KeyCode.Alpha2))
            {
                transform.position = laser.GetPosition(2);
                Vector3[] a = new Vector3[3] { Vector3.zero, Vector3.zero, Vector3.zero };
                laser.SetPositions(a);
                skilCheck = false;
                timer = 0;
            }
        }
    }
}
