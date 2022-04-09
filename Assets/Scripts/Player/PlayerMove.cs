using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private GameManager _gameManager;
    private UIManager _uiManager;

    //public Button jumpButton;

    Rigidbody rb;

    public GameObject jumpSound;
    public GameObject speedBoostSound;
    public GameObject diamondSound;

    public int jumpForce;
    public float forwardSpeed;
    public float clampR;
    //private bool onTheRoad;
    public float clampL;
    public float swipeOffset = 0.5f;

    
    private float boostTimer;
    private bool boosting;
    private bool isGround;

    private float speedDamageTimer;
    private bool damagingSpeed;

    public Animator animator;
    public const string runString = "run";
    public const string jumpString = "jump";
    public const string danceString = "dance";

    void Start()
    {
        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        _uiManager = GameObject.FindWithTag("MainUI").GetComponent<UIManager>();
        _gameManager.SetPlayer(this);
        rb = GetComponent<Rigidbody>();

        boostTimer = 0;
        boosting = false;

        speedDamageTimer = 0;
        damagingSpeed = false;
    }

    private float firstTouchX;
    private float firstTouchY;

    private void Update()
    {
        /*jumpButton.onClick.AddListener(call: () =>
            GetJump()
            );
        if (touch.deltaPosition.y == 50f && (onTheRoad))
        {
                
            GetJump();
        }*/
        if(boosting)
        {
            boostTimer += Time.deltaTime;
            if(boostTimer >= 1)
            {
                forwardSpeed = 3.5f;
                boostTimer = 0;
                boosting = false;

            }
        }
        if (damagingSpeed)
        {
            speedDamageTimer += Time.deltaTime;
            if (speedDamageTimer >= 1)
            {
                forwardSpeed = 3.5f;
                speedDamageTimer = 0;
                damagingSpeed = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SpeedBoost")
        {
            boosting = true;
            forwardSpeed = 10;
            other.gameObject.SetActive(false);
            Instantiate(speedBoostSound);
            Destroy(GameObject.FindWithTag("SpeedBoostSound"), 1f);
        }
        if (other.tag == "SpeedDamage")
        {
            damagingSpeed = true;
            forwardSpeed = 1;
        }
        if (other.tag == "Collectible")
        {
            DiamondText.DiamondAmount += 1;
            other.gameObject.SetActive(false);
            Instantiate(diamondSound);
            Destroy(GameObject.FindWithTag("DiamondSound"),0.3f);
        }
        
        if (other.tag == "Finish")
        {
            _gameManager.EndGame();
            _uiManager.endUI.SetActive(true);
        }
    }
    private void FixedUpdate()
    {
        if (_gameManager.currentGameState != GameState.Start)
        {
            return;
        }
        Vector3 movePos = new Vector3(Mathf.Clamp((transform.position + GetInput()).x, clampL, clampR), (transform.position + GetInput()).y, (transform.position + GetInput()).z);
        rb.MovePosition(movePos);
    }
    private void MoveWithTransform()
    {
        Vector3 moveVector = GetInput();
        transform.position += moveVector;
    }
    private Vector3 GetInput()
    {
        Vector3 moveVector = new Vector3(0, 0, forwardSpeed * Time.deltaTime);
        float diff;
        if (Input.GetMouseButtonDown(0))
        {
            firstTouchX = Input.mousePosition.x;
            firstTouchY = Input.mousePosition.y;
        }
        else if (Input.GetMouseButton(0))
        {
            float lastTouchY = Input.mousePosition.y;
            float lastTouchX = Input.mousePosition.x;
            diff = lastTouchX - firstTouchX;
            moveVector += new Vector3(diff * Time.deltaTime, 0, 0);
            firstTouchX = lastTouchX;
            moveVector = new Vector3(Mathf.Clamp(moveVector.x, clampL, clampR), moveVector.y, moveVector.z );
            if ((lastTouchY-firstTouchY) > swipeOffset && isGround)
            {
                GetJump();
                firstTouchY = 0;
                lastTouchY = 0;
                Debug.Log(isGround);
                isGround = false;
                
            }
        }

        return moveVector;
    }
    public void GetJump()
    {
        if (rb.transform.position.y <= 0)
        {
            animator.Play(jumpString);
            rb.AddForce(new Vector3(0, 1, 4) * jumpForce);
            Instantiate(jumpSound);
            Destroy(GameObject.FindWithTag("JumpSound"), 1f);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Road")
        {
            isGround = true;
        }
        if (collision.gameObject.tag == "KillPlayer")
        {
            _gameManager.KillPlayer();
            _uiManager.tryAgainUI.SetActive(true);
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Road")
        {
            isGround = false;
        }
    }
}
