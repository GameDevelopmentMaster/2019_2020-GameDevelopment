using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDraw : MonoBehaviour
{
    float timer;
    float count= 0.3f;
    public Vector3[] Vpos = new Vector3[4];

    public GameObject player;
    public Vector3 ground;
    LineRenderer Laser;
    MagicNpc magic;

    public ParticleSystem effect;
    bool oneEffect;
    // Start is called before the first frame update
    void Awake()
    {
        transform.GetComponent<TrailRenderer>().time = 0.1f;
        Laser = GetComponent<LineRenderer>();
        player = GameObject.Find("Player");
        magic = transform.GetComponentInParent<MagicNpc>();
        gameObject.SetActive(false);      
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 1f &&!effect.isPlaying)
        {
            magic.MagicDrawing();
            GetComponent<TrailRenderer>().time = 0.1f;
        }
        if(timer > 1f && !oneEffect)
        {
            effect.gameObject.transform.position = transform.position;
            oneEffect = true;
            effect.Play();
        }
        transform.position = Lerp(Vpos[0], Vpos[1], Vpos[2],Vpos[3], timer);
        Laser.SetPosition(0, Vpos[3]);
        Laser.SetPosition(1, transform.position);
        timer += Time.deltaTime * count;
    }

    Vector3 Lerp(Vector3 start,Vector3 startCenter, Vector3 endCenter, Vector3 end, float t)
    {
        Vector3 a = Vector3.Lerp(start, startCenter, t);
        Vector3 b = Vector3.Lerp(startCenter, endCenter, t);
        Vector3 c = Vector3.Lerp(endCenter, end, t);
        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);
        Vector3 data = Vector3.Lerp(d, e, t);
        return data;
    }
   public void DataReset()
    {
        oneEffect = false;
        timer = 0;
        ground = GameObject.Find("Ground").transform.position;
        Vpos[1].Set(Random.Range(player.transform.position.x-10, player.transform.position.x+10), Random.Range(player.transform.position.y - ground.y, player.transform.position.y+ 10), Vpos[1].z);
        Vpos[2].Set(Random.Range(player.transform.position.x - 10, player.transform.position.x+ 10), Random.Range(player.transform.position.y - ground.y, player.transform.position.y+ 10), Vpos[1].z);
        Vpos[3] = player.transform.position - player.transform.forward*0.1f - player.transform.up*0.1f;
        gameObject.SetActive(true);
        GetComponent<TrailRenderer>().time = 1;
        transform.rotation = Quaternion.LookRotation(player.transform.position);
    }
    public void SetPos(int Index, Vector3 Value)
    {
        Vpos[Index] = Value;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Attack(GetComponentInParent<Character>().CharacterData.attackDamage);
        }
    }

}
