using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    [SerializeField]
    GameObject breakingParticle;

    GameManager gameManager;
    BricksManager bricksManager;
    private void Awake()
    {
        gameManager = Object.FindObjectOfType<GameManager>();
        bricksManager = Object.FindObjectOfType<BricksManager>();
        
    }
    private void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag=="Ball")
        {
            Instantiate(breakingParticle, transform.position, transform.rotation);
            Destroy(gameObject);
            gameManager.UpdateScore(+5);
            bricksManager.OneBrickDestroyed();

        }
    }
}
