using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Sword;
    public GameObject Player;
    public Animator SwordAnim;

    public SpriteRenderer[] Swordsprite;
    public int Element = 0; // Red (Fire) = 0, Cyan (Ice) = 1, Green = 2, Ultimate 3
    public int Direction = 0; //North 0 , Right 1 , South 2,  Left 3
    public bool Hit = false;
    public bool IsResetUltimate = false;
    [Header("Projectile Setting")]
    public GameObject Projectile;
    public bool EnableProjectile = false;
    public float ProjectileCD = 6.0f;
    bool IsFire = false;

    public AudioSource HitVFX;

    // Define the key-value mappings for Direction and Element
    Dictionary<KeyCode, int> directionMappings = new Dictionary<KeyCode, int>()
    {
        { KeyCode.W, 0 },
        { KeyCode.D, 1 },
        { KeyCode.S, 2 },
        { KeyCode.A, 3 }
    };

    Dictionary<KeyCode, int> elementMappings = new Dictionary<KeyCode, int>()
    {
        { KeyCode.LeftArrow, 0 },
        { KeyCode.RightArrow, 1 },
        { KeyCode.DownArrow, 2 }
    };

    private IEnumerator ResetHitAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        Hit = false;      
    }

    private IEnumerator FireProjectile()
    {
        IsFire = true;
        yield return new WaitForSeconds(5f);
        GameObject clone;
        clone = Instantiate(Projectile, transform.position, transform.rotation);
        clone.GetComponent<Projectile>().Direction = Direction;
        clone.GetComponent<Projectile>().Element = Element;
        IsFire = false;
               
    }
    private IEnumerator ResetUltimate()
    {
        IsResetUltimate = true;
        Player.GetComponent<Player>().SkillPoint -= 2;
        yield return new WaitForSeconds(1f);
        Player.GetComponent<Player>().SkillPoint -= 2;
        yield return new WaitForSeconds(1f);
        Player.GetComponent<Player>().SkillPoint -= 2;
        yield return new WaitForSeconds(1f);
        Player.GetComponent<Player>().SkillPoint -= 2;
        yield return new WaitForSeconds(1f);
        Player.GetComponent<Player>().SkillPoint -= 2;
        yield return new WaitForSeconds(1f);
        IsResetUltimate = false;
        Player.GetComponent<Player>().ActiveUltimate = false;
        Element = 0;
    } 

    private void OnTriggerEnter2D(Collider2D collision) //For Enter Hitbox
    {
        if(collision.tag == "Enemy")
        {           
            int EnemyType = collision.gameObject.GetComponent<Enemy>().ElementType;
            if (Element == 3) //During Ultimate
            {
                Destroy(collision.gameObject);                
                Player.GetComponent<Player>().Score += 1;               
                HitVFX.Play();
            }
            else if (Element == EnemyType)
            {
                Destroy(collision.gameObject);
                if (Player.GetComponent<Player>().ActiveUltimate == false) Player.GetComponent<Player>().SkillPoint += 1;
                Player.GetComponent<Player>().Score += 1;
                Player.GetComponent<Player>().CurrentLV += 1;
                Player.GetComponent<Player>().CurrentLV += 1;

                HitVFX.Play();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision) //For Stay Hitbox
    {
        if (collision.tag == "Enemy")
        {            
            int EnemyType = collision.gameObject.GetComponent<Enemy>().ElementType;
            if (Element == 3) //During Ultimate
            {
                Destroy(collision.gameObject);                
                Player.GetComponent<Player>().Score += 1;                

                HitVFX.Play();
            }
            else if (Element == EnemyType)
            {
                Destroy(collision.gameObject);
                if (Player.GetComponent<Player>().ActiveUltimate == false) Player.GetComponent<Player>().SkillPoint += 1;
                Player.GetComponent<Player>().Score += 1;
                Player.GetComponent<Player>().SkillPoint += 1;
                Player.GetComponent<Player>().CurrentLV += 1;

                HitVFX.Play();
            }
        }
    }

    void Start()
    {
        Element = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Change Direction
        foreach (var mapping in directionMappings)
        {
            if (Input.GetKeyDown(mapping.Key))
            {
                Direction = mapping.Value;
                break;
            }
        }

        // Change Element
        foreach (var mapping in elementMappings)
        {
            if (Input.GetKeyDown(mapping.Key) && Element != 3)
            {
                Element = mapping.Value;
                break;
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Hit = true;
            StartCoroutine(ResetHitAfterDelay());
        }

        switch (Element)
        {
            case 0: 
                //Sword.GetComponent<SpriteRenderer>().color = Color.red;
                Sword.GetComponent<SpriteRenderer>().sprite = Swordsprite[0].sprite;
                break;
            case 1:
                //Sword.GetComponent<SpriteRenderer>().color = Color.cyan;
                Sword.GetComponent<SpriteRenderer>().sprite = Swordsprite[1].sprite;
                break;
            case 2:
                //Sword.GetComponent<SpriteRenderer>().color = Color.green;
                Sword.GetComponent<SpriteRenderer>().sprite = Swordsprite[2].sprite;
                break;
            case 3:
                Sword.GetComponent<SpriteRenderer>().sprite = Swordsprite[3].sprite;
                break;
        }

        switch (Direction)
        {
            case 0:
                /*                Sword.transform.localPosition = new Vector3(0, 2, 0);
                                Sword.transform.localEulerAngles = new Vector3(0, 0, 90);*/
                SwordAnim.SetInteger("Direction", 0);
                break;
            case 1:
              /*  Sword.transform.localPosition = new Vector3(1.2f, 0.6f, 0);
                Sword.transform.localEulerAngles = new Vector3(0, 0, 0);*/
                SwordAnim.SetInteger("Direction", 1);
                break;
            case 2:
                /* Sword.transform.localPosition = new Vector3(0, -1, 0);
                 Sword.transform.localEulerAngles = new Vector3(0, 0, 270);*/
                SwordAnim.SetInteger("Direction", 2);
                break;
            case 3:
                /*Sword.transform.localPosition = new Vector3(-1.2f, 0.6f, 0);
                Sword.transform.localEulerAngles = new Vector3(0, 0, 180);*/
                SwordAnim.SetInteger("Direction", 3);
                break;
        }

        if (Player.GetComponent<Player>().ActiveUltimate == true)
        {            
            Element = 3;
            if(!IsResetUltimate) StartCoroutine(ResetUltimate());
        }

        if(EnableProjectile && !IsFire)
        {
            StartCoroutine(FireProjectile());
        }
    }
}
