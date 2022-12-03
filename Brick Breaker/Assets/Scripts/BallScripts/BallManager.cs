using System.Collections;

using System.Diagnostics.Tracing;

using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField]
    Transform ballStartPosition;

    [SerializeField]
    Transform paddleTransform; //this will be used for calculating the force vector for the ball just before the starting.

    GameManager gameManager;
    PaddleController paddleController;
    Vector2 previousPaddleTransform = Vector2.zero;
    Vector2 forceDirection;
    Vector2 paddleDefaultScale = new Vector2(0.6f, 0.3f);
    
    private Rigidbody2D rb;
    bool inPlay; //false
    float speed = 450f;
    float newSpeed; //for calculating the starting speed
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = Object.FindObjectOfType<GameManager>();  //
        paddleController = Object.FindObjectOfType<PaddleController>();
    }
    void Update()
    {
        if(gameManager.gameOver)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        
        if (!inPlay)
        {
            forceDirection = CalculateStartingForceVector();
            transform.position = ballStartPosition.position;
        }
        if(Input.GetKeyDown(KeyCode.Space) && !inPlay)
        {
            
            inPlay = true;
            if (forceDirection.x > 0.7f || forceDirection.x < -0.7f)
            {
                newSpeed = speed * 0.8f;
            }
            else
            {
                newSpeed = speed;
            }
                
            rb.AddForce(forceDirection * newSpeed);
            print("BallStartedForceVector = " + forceDirection + "Starting Speed" + newSpeed);
        }
        if (Input.GetKeyDown(KeyCode.L) && inPlay)
        {
            rb.velocity = Vector2.zero;
            inPlay = false;
            gameManager.UpdateTotal();
            gameManager.UpdateLives(-1);//lives -1
            paddleController.transform.localScale = paddleDefaultScale;
            paddleController.ResetBoundary();
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Bottom")
        {
            rb.velocity = Vector2.zero;
            inPlay=false;
            gameManager.UpdateTotal();
            gameManager.UpdateLives(-1);//lives -1
            paddleController.transform.localScale = paddleDefaultScale;
            paddleController.ResetBoundary();
            
        }
    }

    private Vector2 CalculateStartingForceVector()
    {
    
        Vector2 paddleVector = new Vector2(paddleTransform.position.x, 0);
        Vector2 velocityVector= (paddleVector - previousPaddleTransform)  / Time.deltaTime; 
        previousPaddleTransform = paddleVector;
        velocityVector.x = Mathf.Clamp(velocityVector.x/7, -1, 1); //scaling the values between -1,1
        //print("BallForceVector.x = " + velocityVector.x);
        Vector2 forceDirection = new Vector2(velocityVector.x, 1);
        return forceDirection;  
    }
}
