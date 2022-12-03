using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBrick : MonoBehaviour
{
    [SerializeField]
    private Sprite broken1Brick;
    [SerializeField]
    private Sprite broken2Brick;
    [SerializeField]
    GameObject brickEfx, breakEfx;
    [SerializeField]
    GameObject liveUpDrop;
   

    GameManager gameManager;
    BricksManager bricksManager;
    int counter;

    int randomValue;
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
        if (collision.gameObject.tag == "Ball")
        {
            counter++;
            if (counter == 1)
            {
                GetComponent<SpriteRenderer>().sprite = broken1Brick;
                gameManager.UpdateScore(+1);
            }
            else if (counter == 2)
            {
                Instantiate(brickEfx, transform.position, transform.rotation);
                Instantiate(breakEfx, transform.position, transform.rotation);
                randomValue = Random.Range(0, 9);
                if (randomValue >= 5)
                {
                    Instantiate(liveUpDrop, transform.position, transform.rotation);
                }
                GetComponent<SpriteRenderer>().sprite = broken2Brick;
                gameManager.UpdateScore(+3);
            }
            else if (counter == 3)
            {
                
                Destroy(gameObject);
                gameManager.UpdateScore(+15);
                bricksManager.OneBrickDestroyed();
            }
        }

    }
}
