using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private float speed = 2f;
    private Vector2 v;
    private Rigidbody2D rb2D;
    
    private Animator animator;
    bool facingRight = false;
    private Transform target;
    private Animation ani;
    private BoxCollider2D box;
    private float Health = 150f;

    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animation>();
        box = GetComponent<BoxCollider2D>();
       
    }

    void Update()
    {
        Vector3 playerPos = new Vector3(target.position.x, transform.position.y, transform.position.z);

        float x = Time.deltaTime*speed;
        
        transform.position = Vector3.MoveTowards(transform.position, playerPos, x);
        // if nothing is collided then walk animation towards player
        animator.SetBool("walk", true);

        

        if (target.position.x < transform.position.x && facingRight)
            Flip();
        else if (target.position.x > transform.position.x  && !facingRight)
            Flip();

        
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            animator.SetBool("kick",true);
            animator.SetBool("walk", false);
            collision.gameObject.SendMessage("ApplyDamage", 0.125f);
        }

        if (collision.gameObject.tag == "shot")
        {
            animator.SetBool("hit", true);
            

        }
        

    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool("kick", true);
            animator.SetBool("walk", false);
            
            collision.gameObject.SendMessage("ApplyDamage", 0.125f);
        }

        


    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        animator.SetBool("kick", false);
        animator.SetBool("walk", true);
       
            animator.SetBool("hit", false);

        

    }

    public void ApplyDamage(float hit)
    {
        print(Health);
        Health -= hit;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    

    
}
