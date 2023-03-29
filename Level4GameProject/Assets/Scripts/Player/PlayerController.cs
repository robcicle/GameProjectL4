using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // PLAYER MOVEMENT
    public bool hasStarted = false;

    [SerializeField]
    private Transform[] trackYPos;

    private Rigidbody2D _rb;
    private int playerCurrTrack = 1;

    [SerializeField]
    float jumpForce = 4f;
    [SerializeField]
    float jumpCooldown = 2f;
    bool isGrounded = false;

    float jumpCooldownTimer;

    [SerializeField]
    LayerMask groundLayer;


    // PLAYER VALUES
    [SerializeField]
    int playerHealth = 4;
    int playerScore = 0;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        jumpCooldownTimer = jumpCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x, transform.position.y),
            new Vector2(transform.position.x + 0.5f, transform.position.y + 0.5f), groundLayer);

        jumpCooldownTimer -= Time.deltaTime;

        // Get the players inputs (W or S) decide wether to move up or down.
        // #TO IMPROVE ON THIS ADD INPUTS IN THE GAMECONTROLLER AND A SETTINGS MENU TO CHANGE SAID INPUTS
        if (Input.GetKeyDown(KeyCode.W) && hasStarted)
            MoveTrack(-1);
        else if (Input.GetKeyDown(KeyCode.S) && hasStarted)
            MoveTrack(1);
        else if (Input.GetKeyDown(KeyCode.Space) && hasStarted && isGrounded && jumpCooldownTimer < 0)
        {
            jumpCooldownTimer = jumpCooldown;
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void MoveTrack(int amount)
    {
        if (playerCurrTrack + amount <= 2 && playerCurrTrack + amount >= 0)
        {
            playerCurrTrack += amount;

            transform.position = new Vector2(transform.position.x, trackYPos[playerCurrTrack].position.y);
        }
    }

    public void Hit()
    {
        playerHealth -= 1;
        Debug.Log("WAH");
        Debug.Log("HIT: HEALTH - " + playerHealth);
    }

    public void Fix()
    {
        playerHealth += 1;
        Debug.Log("FIX: HEALTH - " + playerHealth);
    }

    public void Score(int amount)
    {
        playerScore += amount;
        Debug.Log("SCORE - " + playerScore);
    }
}