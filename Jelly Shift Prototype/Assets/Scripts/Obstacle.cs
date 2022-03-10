using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float movementSpeed = 5f;
    public int points = 5;

    // Update is called once per frame
    void Update()
    {
        Movement();
        AddScoreAndDestroy();
    }

    // This method moves the obstacle in the z axis according to the state of the game
    void Movement()
    {
        if(!GameManager.instance.hitBrake && !GameManager.instance.gameOver)
        {
            transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);
        }
        if(GameManager.instance.hitBrake)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }
    }

    // This method adds score to the game manager and destroys this object once it crosses player
    void AddScoreAndDestroy()
    {
        if(transform.position.z < -4)
        {
            GameManager.instance.AddScore(points);
            Destroy(gameObject);
        }
    }
}
