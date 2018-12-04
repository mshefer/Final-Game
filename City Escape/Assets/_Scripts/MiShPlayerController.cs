using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class MiShPlayerController : MonoBehaviour {

    public Text scoreText;         
    public Text winText;
    

    private Rigidbody2D rb2d;
    private int score;
    public float speed;
    public float jumpForce;
    public Boundary boundary;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb2d.AddForce(movement * speed);

       rb2d.position = new Vector3
       (
              Mathf.Clamp(rb2d.position.x, boundary.xMin, boundary.xMax),
              Mathf.Clamp(rb2d.position.y, boundary.yMin, boundary.yMax),
               0.0f
       );
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Star"))
        {
            other.gameObject.SetActive(false);
            score = score + 5;
            SetScoreText();
        }

        

        if (other.gameObject.CompareTag("Flag"))
        {
            other.gameObject.SetActive(true);
            score = score + 5;
            SetScoreText();
        }
        
    }

    void SetScoreText()
    {

        scoreText.text = "Score: " + score.ToString();


        if (score >= 10)
        {
           
            winText.text = "You Win!";
        }
    }


    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }


    }
}

