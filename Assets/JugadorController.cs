using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JugadorController : MonoBehaviour
{
    private bool up = false;
    private bool left = false;
    private bool right = false;
    private bool shoot = false;
    private float playerSpeed = 5f;
    private float jumpPower = 2.5f;
    private bool grounded = false;
    private Rigidbody2D rgb;
    private Animator anim;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && grounded)
        {
            up = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            left = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            right = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            shoot = true;
        }
        if (!(up && left && right && shoot))
        {
            anim.SetTrigger("esperando");
        }
    }
    private void FixedUpdate()
    {
        if (up)
        {
            anim.SetTrigger("saltando");
            rgb.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
            up = false;
        }

        if (left)
        {
            if (grounded)
            {
                anim.SetTrigger("caminando");
            }
            transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
            transform.localScale = new Vector3(-2f,2f,1f);
            left = false;
        }

        if (right)
        {
            if (grounded)
            {
                anim.SetTrigger("caminando");
            }
            transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
            transform.localScale = new Vector3(2f, 2f, 1f);
            right = false;
        }

        if (shoot)
        {
            if (grounded)
            {
                anim.SetTrigger("disparando");
            }
            shoot = false;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "piso")
        {
            grounded = true;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "obstaculo")
        {
            anim.SetTrigger("muriendo");
            Invoke("gameOver", 0.7f);
        }

        if (collision.gameObject.tag == "objeto")
        {
            this.gameObject.SetActive(false);
            text.text = "GANASTE";
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "piso")
        {
            grounded = false;
        }
    }
    private void gameOver()
    {
        this.gameObject.SetActive(false);
        text.text = "PERDISTE";
    }
}
