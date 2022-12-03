using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardBrickManager : MonoBehaviour
{
    [SerializeField]
    private Sprite brokenBrick;
    [SerializeField]
    GameObject brickEfx, breakEfx;

    [SerializeField] GameObject liveDown;
    [SerializeField] GameObject extraBall;
    [SerializeField] GameObject scaleDown;
    [SerializeField] GameObject scaleUp;

    GameManager gameManager;
    BricksManager bricksManager;
    int randomNumber;
    int counter;
    private void Awake()
    {
        gameManager = Object.FindObjectOfType<GameManager>();
        bricksManager = Object.FindObjectOfType<BricksManager>();
    }
    private void Start()
    {
        counter = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Ball")
        {
            counter++;
            if (counter == 1)
            {
                Instantiate(brickEfx, transform.position, transform.rotation);
                RandomDrop();
                GetComponent<SpriteRenderer>().sprite = brokenBrick;
                gameManager.UpdateScore(+2);
            }
            else if(counter == 2)
            {
                Instantiate(breakEfx,transform.position,transform.rotation);
                
                Destroy(gameObject);
                gameManager.UpdateScore(+10);
                bricksManager.OneBrickDestroyed();
            }
        }
        
    }

    private void RandomDrop()
    {
        randomNumber = Random.Range(0, 101);
        if (randomNumber > 85)
        {
            Instantiate(extraBall, transform.position, transform.rotation);
        }
        else if (randomNumber > 50)
        {
            Instantiate(liveDown, transform.position, transform.rotation);
        }
        else if (randomNumber > 35)
        {
            Instantiate(scaleDown, transform.position, transform.rotation);
        }
        else if (randomNumber > 20)
        {
            Instantiate(scaleUp, transform.position, transform.rotation);
        }
    }
}
