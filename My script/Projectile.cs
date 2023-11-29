using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    GameObject Player;
    public float speed;   
    public int Direction = 0;
    public int Element = 0;
    

    private void OnTriggerEnter2D(Collider2D collision) //For Enter Hitbox
    {
        if (collision.tag == "Enemy")
        {
            int EnemyType = collision.gameObject.GetComponent<Enemy>().ElementType;
            if (Element == 3) //During Ultimate
            {
                Destroy(collision.gameObject);
                Player.GetComponent<Player>().Score += 1;
                Player.GetComponent<Player>().SkillPoint += 1;
            }
            else if (Element == EnemyType)
            {
                Destroy(collision.gameObject);
                if (Player.GetComponent<Player>().ActiveUltimate == false) Player.GetComponent<Player>().SkillPoint += 1;
                Player.GetComponent<Player>().Score += 1;
                Player.GetComponent<Player>().CurrentLV += 1;
                Player.GetComponent<Player>().CurrentLV += 1;
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
                Player.GetComponent<Player>().SkillPoint += 1;
            }
            else if (Element == EnemyType)
            {
                Destroy(collision.gameObject);
                if (Player.GetComponent<Player>().ActiveUltimate == false) Player.GetComponent<Player>().SkillPoint += 1;
                Player.GetComponent<Player>().Score += 1;
                Player.GetComponent<Player>().SkillPoint += 1;
                Player.GetComponent<Player>().CurrentLV += 1;
            }
        }
    }
    private void Start()
    {
        Player = GameObject.Find("Player Object");
    }

    void Update()
    {
        //copy
        Vector3 pos = this.transform.localPosition;

        switch (Element)
        {
            case 0:
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 1:
                GetComponent<SpriteRenderer>().color = Color.cyan;
                break;
            case 2:
                GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case 3:
                GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
        }
        switch (Direction)
        {
            case 0:
                pos.y = pos.y + (speed * 1);
                this.transform.localEulerAngles = new Vector3(0, 0, 90);                              
                break;
            case 1:
                pos.x = pos.x + (speed * 1);
                this.transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case 2:
                pos.y = pos.y + (speed * -1);
                this.transform.localEulerAngles = new Vector3(0, 0, 270);               
                break;
            case 3:
                pos.x = pos.x + (speed * -1);
                this.transform.localEulerAngles = new Vector3(0, 0, 180);
                break;
        }       
        //paste
        this.transform.localPosition = pos;
    }
}
