using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoController : MonoBehaviour
{
    private bool up = false;
    private bool left = false;
    private bool right = false;
    private bool shoot = false;
    private float playerSpeed = 5f;
    private float jumpPower = 2.5f;
    private bool grounded = false;
    public GameObject personaje;
    private Rigidbody2D rgb;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        anim = personaje.GetComponent<Animator>();
        Invoke("isGrounded", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.U) && grounded)
        {
            up = true;
        }

        if (Input.GetKey(KeyCode.H))
        {
            left = true;
        }

        if (Input.GetKey(KeyCode.K))
        {
            right = true;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            shoot = true;
        }
        
        
    }

    private void FixedUpdate()
    {
        if (up)
        {
            anim.SetTrigger("jump");
            rgb.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
            up = false;
        }

        if (left)
        {
            if (grounded)
            {
                anim.SetTrigger("walk");
            }
            transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
            transform.localScale = new Vector3(-1f, 1f, 1f);
            left = false;
        }

        if (right)
        {
            if (grounded)
            {
                anim.SetTrigger("walk");
            }
            transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
            transform.localScale = new Vector3(1f, 1f, 1f);
            right = false;
        }

        if (shoot)
        {
            if (grounded)
            {
                anim.SetTrigger("attack");
            }
            shoot = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "piso")
        {
            grounded = true;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "obstaculo")
        {
            anim.SetTrigger("death");
            Invoke("gameOver", 1f);
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "piso")
        {
            grounded = false;
        }
    }
    
    private void isGrounded()
    {
        if (!(up && left && right && shoot))
        {
            anim.SetTrigger("idle");
        }
        Invoke("isGrounded", 1f);
    }

    private void gameOver()
    {
        this.gameObject.SetActive(false);
    }
}
