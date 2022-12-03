using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField]
    float speed=18.0f;

    
    GameManager gameManager;
    float leftBoundary= -7.43f, rightBoundary= 7.43f;

    //ScaleVectors
    Vector2 scaleDown = new Vector2(0.31f, 0.3f);
    Vector2 scaleUp = new Vector2(0.92f, 0.3f);

    void Awake()
    {
        gameManager = Object.FindObjectOfType<GameManager>();  //
       
    }

    void Update()
    {
        if (gameManager.gameOver)
        {
            return;
        }
        float horizontalValue = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontalValue * Time.deltaTime * speed);

        // For making sure x coordinates doesn't cross the boundaries.
        Vector2 boundaryControl = transform.position;
        boundaryControl.x = Mathf.Clamp(boundaryControl.x, leftBoundary, rightBoundary);
        transform.position = boundaryControl;

       
        /*
          if (transform.position.x < leftBoundary)
              transform.position = new Vector2(leftBoundary, transform.position.y);
          if (transform.position.x > rightBoundary)
              transform.position = new Vector2(rightBoundary, transform.position.y);
      */
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="liveUp")
        {
            gameManager.UpdateLives(1);
            Destroy(collision.gameObject);
            gameManager.UpdateScore(5);
        }
        if (collision.gameObject.tag == "liveDown")
        {
            gameManager.UpdateLives(-1);
            gameManager.UpdateScore(-2);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "scaleDown")
        {
            transform.localScale=scaleDown;
            leftBoundary = -8.12f;
            rightBoundary = 8.12f;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "scaleUp")
        {
            transform.localScale = scaleUp;
            leftBoundary = -6.65f;
            rightBoundary = 6.65f;
            Destroy(collision.gameObject);
        }

    }
    public void ResetBoundary()
    {
        leftBoundary = -7.43f;
        rightBoundary = 7.43f;
    }
}
