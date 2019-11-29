using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct CharacterData
{
    public float hp;
    public float attackTimer;
    public float speed;
    public readonly float maxHp;
    public float attackDamage;

    public CharacterData(float hpValue = 100f,float attackTimerValue = 2f, float speedValue = 2f,float attackDamageValue = 20f)
    {
        maxHp = hp = hpValue;
        attackTimer = attackTimerValue;
        speed = speedValue;
        attackDamage = attackDamageValue;
    }
}

public enum CharacterList
{
    player,magic,sword
};

public interface CharacterInterface
{
    void Attack(float damage);
}

public class Character : MonoBehaviour,CharacterInterface
{
    Dictionary<CharacterList, CharacterData> character;
    CharacterData characterData = new CharacterData();
    public ref CharacterData CharacterData
    {
       get => ref characterData;
    }
   
    public Character()
    {
        
        character = new Dictionary<CharacterList, CharacterData>()
        {
            [CharacterList.player] = new CharacterData(200f, 1f, 2f, 30f),
            [CharacterList.magic] = new CharacterData(150f, 2f, 3f, 50f),
            [CharacterList.sword] = new CharacterData(400f, 1.5f, 3f, 35f)
        };
    }
    
    public virtual void Attack(float damage)
    {
        characterData.hp -= damage;
    }

    protected void CharacterInit(CharacterList characterListValue)
    {
        characterData = character[characterListValue];
    }

    private void OnCollisionEnter(Collision collision)
    {

        collision.gameObject.GetComponent<Character>().Attack(CharacterData.attackDamage);
    }
}
