using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBallDrop : MonoBehaviour
{
    GameManager gameManager;
    
    private Rigidbody2D rb;
    bool inPlay; //false
    [SerializeField] float speed = 200f;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = Object.FindObjectOfType<GameManager>();  //
       

    }
    void Start()
    {
        rb.AddForce(Vector2.one * speed);
    }
    void Update()
    {
        if (gameManager.gameOver)
        {
            rb.velocity = Vector2.zero;
            Destroy(gameObject);
            return;
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bottom")
        {
            rb.velocity = Vector2.zero;
            Destroy(gameObject);

   
        }
    }
}
