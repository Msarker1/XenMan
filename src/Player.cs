using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private float speed = 10f;
    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.08f;
    public LayerMask whatIsGround;
    public GameObject shots;



    private float borderX = -8.768562f;

    private float borderY = -2.540737f;
    private float maxHeight = 2.42f;
    private float shotPower = 2.3f;
    private float Health = 100f;




    private Vector2 v;
    private Rigidbody2D rb2D;
    private Animator animator;
    bool facingRight = true;
    bool shoot = false;




    // Use this for initialization
    void Start() {

        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update() {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        animator.SetBool("Ground", grounded);



        float x = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(x));


        Vector2 pos = rb2D.position;
        pos.x = Mathf.Clamp(pos.x, borderX, -borderX);
        pos.y = Mathf.Clamp(pos.y, borderY, -borderY);


        

        rb2D.velocity = new Vector2(x * speed, rb2D.velocity.y);

        if (grounded && Input.GetKeyDown(KeyCode.Space)  )
        {

            rb2D.AddForce(transform.up * 500f);
            animator.SetTrigger("Jump");
            animator.SetBool("shooting", false);


        }

        transform.position = pos;

        print(rb2D.position.y);
        if (Input.GetKeyDown(KeyCode.P) && grounded && x == 0)
        {


            animator.SetTrigger("shot");
            animator.SetBool("shooting", true);
            


            if (facingRight)
            {
                GameObject instance = Instantiate(shots, new Vector3(rb2D.position.x + 0.5f, rb2D.position.y, 0f), Quaternion.identity) as GameObject;
                instance.GetComponent<Rigidbody2D>().AddForce(transform.right * 1000f);

            }
            else
            {
                GameObject instance = Instantiate(shots, new Vector3(rb2D.position.x - 0.5f, rb2D.position.y, 0f), Quaternion.identity) as GameObject;
                instance.GetComponent<Rigidbody2D>().AddForce(force: -transform.right * 1000f);


            }



        }
        




        if (x > 0 && !facingRight)
            Flip();
        else if (x < 0 && facingRight)
            Flip();


    }




    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            animator.SetBool("hit", true);
           
           
        }

        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            animator.SetBool("hit", true);
        }

    }


    private void OnCollisionExit2D(Collision2D collision)
    {
       
        animator.SetBool("hit",false);
    }
    
    
}
