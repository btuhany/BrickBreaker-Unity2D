using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksManager : MonoBehaviour
{
    [SerializeField]
    GameObject hardBrick;
    [SerializeField]
    GameObject standartBrick;
    [SerializeField]
    GameObject redBrick;

    public GameObject parentBricksManager;

    int randomNumber;
    int totalBrickNumber;
    bool IsbricksSpawnedFlag;
    GameObject currentBrick;
    Vector2 brickPosition = new Vector2(-7.7f,9f);

    Vector2 bricksInGamePosition = new Vector2(-7.7f, 4.2f);
    
   
    private void Update()
    {
        if(NoBricksLeft())
        {
            SpawnBricks();
        }
        MoveBricks();
    }

    private void SpawnBricks()
    {
        float oldBrickPositionX=brickPosition.x;
        for (int j = 0; j <= 3; j++)
        {
            for (int i = 0; i <= 7; i++)
            {

                if (((j == 2 && (i == 1 || i == 6))) || ((j==3) && !(i==0 || i==7)))
                {
                    brickPosition.x = brickPosition.x + 2.2f;
                    continue;
                }
                
                randomNumber = Random.Range(0, 100);
                if (randomNumber > 84)
                {
                    currentBrick = Instantiate(redBrick, brickPosition, transform.rotation);
                    currentBrick.transform.SetParent(parentBricksManager.transform);
                    totalBrickNumber++;

                }
                else if (randomNumber > 55)
                {
                    currentBrick = Instantiate(hardBrick, brickPosition, transform.rotation);
                    currentBrick.transform.SetParent(parentBricksManager.transform);
                    totalBrickNumber++;
                }
                else if (randomNumber >= 0)
                {
                    currentBrick = Instantiate(standartBrick, brickPosition, transform.rotation);
                    currentBrick.transform.SetParent(parentBricksManager.transform);
                    totalBrickNumber++;
                }

                brickPosition.x = brickPosition.x + 2.2f;

            }
            brickPosition.x = oldBrickPositionX;
            brickPosition.y = brickPosition.y - 0.7f;


        }
        IsbricksSpawnedFlag = true;
    }

    private bool NoBricksLeft()
    {
        if (totalBrickNumber == 0)
        {
            transform.position = brickPosition;
            return true;
        }
        else
            return false;
        
    }
    private void MoveBricks()
    {
        if (IsbricksSpawnedFlag)
        {
            transform.position = Vector2.MoveTowards(transform.position, bricksInGamePosition, 4.5f * Time.deltaTime);
            if (transform.position.y == bricksInGamePosition.y)
            {
                IsbricksSpawnedFlag = false;
            }
        }

           
    }
    public void OneBrickDestroyed()
    {
        totalBrickNumber--;
    }
}
