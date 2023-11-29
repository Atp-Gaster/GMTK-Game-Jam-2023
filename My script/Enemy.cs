using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Player;
    public int speed = 1;
    public int AttackDMG = -1;    
    public int ElementType = 0;
    public bool IsMoving = true;
    public Animator anim;

    private void Start()
    {
        ElementType = Random.Range(0, 3);
        switch (ElementType)
        {
            case 0:
                anim.SetInteger("Type", 0);
                break;
            case 1:
                anim.SetInteger("Type", 1);
                break;
            case 2:
                anim.SetInteger("Type", 2);
                break;
        }
        speed = Random.Range(1, 3);
        Player = GameObject.Find("Player Object");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {               
        if (collision.tag == "Player")
        {         
            IsMoving = false;
            Player.GetComponent<Player>().TakeDMG();
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {            
            IsMoving = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // calculate distance to move
        if(IsMoving)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(this.transform.position, Player.transform.position, step);
        }

        if (this.transform.position.x > Player.transform.position.x) //Left filpx
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(this.transform.position.x < Player.transform.position.x) this.GetComponent<SpriteRenderer>().flipX = false;

        // Set color
        switch (ElementType)
        {
            case 0:
                anim.SetInteger("Type", 0);
                break;
            case 1:
                anim.SetInteger("Type", 1);
                break;
            case 2:
                anim.SetInteger("Type", 2);
                break;
        }
    }
}
