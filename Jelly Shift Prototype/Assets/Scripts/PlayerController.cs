using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 10;
    public float verticalInput;
    public float horizontalInput;
    private Rigidbody playerRb;
    private float offSet = 0.1f;
    public float gravityModifier = 1;
    private bool canChangeShape = true;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        StartCoroutine(PlayerSizeCoroutine());
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerSizeChange();
    }

    // This method changes the player size with respect to the input
    void PlayerSizeChange()
    {
        if (!canChangeShape) return;
        verticalInput = Input.GetAxis("Vertical");
        if(verticalInput > 0 && transform.localScale.x > 0.2)
        {
            transform.localScale = new Vector3(transform.localScale.x - offSet, transform.localScale.y + offSet, transform.localScale.z);
        } else if(verticalInput < 0 && transform.localScale.y > 0.2)
        {
            transform.localScale = new Vector3(transform.localScale.x + offSet, transform.localScale.y - offSet, transform.localScale.z);
        }
        canChangeShape = false;
    }

    // This coroutine limits the size change
    IEnumerator PlayerSizeCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.025f);
            canChangeShape = true;
            PlayerSizeChange();
        }
    }

    // This method checks collision and performs tasks
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            StartCoroutine(HitPauseRoutine());
            GameManager.instance.GetDamage();
        }
    }

    // This method enables the hitback mechanism in the obstacle script
    IEnumerator HitPauseRoutine()
    {
        GameManager.instance.hitBrake = true;
        yield return new WaitForSeconds(0.3f);
        GameManager.instance.hitBrake = false;
    } 
}
