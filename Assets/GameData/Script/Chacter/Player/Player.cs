using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    Vector3 prePos;
    Vector3 mousedownDir;
    Vector3 mouseupDir;
    LineRenderer laser;
    CameraMove move;
    Rigidbody rigidbodyCom;
    PlayerSkill playerSkill;
    GameObject a, b;
    bool magicShild = false;
    float magicShildTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        CharacterInit(CharacterList.player);
        playerSkill = GetComponent<PlayerSkill>();
        rigidbodyCom = GetComponent<Rigidbody>();
        laser = GetComponent<LineRenderer>();
        move = transform.GetComponentInChildren<CameraMove>();
    }

    
    // Update is called once per frame
    void Update()
    {
        #region Attack
        if (Input.GetMouseButton(1))
        {
            transform.localEulerAngles += new Vector3(0, Input.GetAxisRaw("Mouse X"));
        }
        if (Input.GetMouseButtonDown(0))
        {
            Collider[] hit;
            hit = Physics.OverlapSphere(transform.position+transform.up, transform.localScale.x * 20);
            if(hit.Length != 0)
            {
                int index = 0;
                float min = Vector3.SqrMagnitude(hit[0].transform.position-transform.position);
                for(int i=1; i<hit.Length; i++)
                {
                    float data = Vector3.SqrMagnitude(hit[i].transform.position - transform.position);
                    if (min > data && hit[i].CompareTag("Monster"))
                    {
                        index = i;
                        min = data;
                    }
                }
                if(hit[index].CompareTag("Monster"))
                AttakAnimation(hit[index].transform.position);
            }
        }
        //if(Input.GetMouseButtonUp(0))
        //{
        //    Time.timeScale = 1;
        //    mouseupDir = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z+3 - Camera.main.transform.position.z));
        //    laser.SetPosition(1, mouseupDir);
        //    laser.SetPosition(2, mouseupDir);
        //    RaycastHit raycast = new RaycastHit();
        //    if (Physics.BoxCast(transform.position,new Vector3(0.0001f,0.0001f,0.0001f),mouseupDir-mousedownDir,out raycast))
        //    {
        //        if(raycast.transform.GetComponent<SwordNPC>() != null)
        //        {
        //            raycast.transform.GetComponent<SwordNPC>().CountAdd();
        //            raycast.transform.GetComponentInParent<MagicDraw>().MagicDrawing();
        //        }
        //    }
        //}
        #endregion

        #region Hide
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CharacterData.speed += 10;

        }
        #endregion

        #region MagicShild
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            magicShild = true;
        }
        if (magicShild)
        {
            magicShildTimer += Time.deltaTime;
        }
        if(magicShildTimer > 0.3f)
        {
            magicShild = false;
            magicShildTimer = 0;
        }
        #endregion

        #region Teleport
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerSkill.skilCheck = true;
            laser.SetPosition(0, (-transform.right + transform.forward) * 10);
            laser.SetPosition(1, transform.position);
            laser.SetPosition(2, (transform.right + transform.forward) * 10f);
        }
        if (!playerSkill.skilCheck)
        {
            Vector3 h = Input.GetAxis("Horizontal") * CharacterData.speed * Vector3.right * Time.deltaTime;
            Vector3 v = Input.GetAxis("Vertical") * CharacterData.speed * Vector3.forward * Time.deltaTime;
            transform.Translate(h + v);
        }
        #endregion
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            collision.gameObject.GetComponent<Character>().Attack(CharacterData.attackDamage);
        }
    }
   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
    }
    

    IEnumerator DestroyObject(GameObject destroyobject)
    {
        while(destroyobject.transform.localScale.x > 0.3f)
        {
            destroyobject.transform.localScale -= Vector3.one * Time.deltaTime;
            yield return null;
        }
        destroyobject.SetActive(false);
        yield return new WaitForSeconds(1f);
        Destroy(destroyobject);
    }

    #region Attack_Method
    void AttakAnimation(Vector3 MonsterPos)
    {
        StartCoroutine(MovePlayer(MonsterPos));
    }

    public override void Attack(float damageValue)
    {
        if (!magicShild)
        {
            CharacterData.hp -= damageValue;
        }
    }

    IEnumerator MovePlayer(Vector3 MonsterPos)
    {
        float timer =0;
        Vector3 nowPos = transform.position;
        Quaternion nowRot = transform.rotation;
        while(timer < 0.5f)
        {
            transform.position = Vector3.Lerp(nowPos, MonsterPos, timer);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(MonsterPos-transform.position), timer);
            timer += Time.deltaTime;
            yield return null;
        }
    }
    #endregion

}
