using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    //Inputs
    [SerializeField] private KeyCode _left = KeyCode.A;
    [SerializeField] private KeyCode _right = KeyCode.D;
    [SerializeField] private KeyCode _jump = KeyCode.W;

    //movement
    [SerializeField] private float _maxSpeed = 10.0f;
    [SerializeField] private float _jumpForce = 8.0f;
    [SerializeField] private float _friction = 10.0f;
    [SerializeField] private float _fallThreashold = -10f;

    public Image healthBar;
    private float healthAmount = 100.0f;
    private float damage = 50;
    private Vector2 startingPos;

    private Rigidbody2D _rb = null;
    private bool _isGrounded = false;
    public bool bc = false;

    public CanvasGroup gameOverCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        if (!_rb)
        {
            Debug.Log("uh oh");
        }

        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerMovement();
        groundCheck();
        UpdateBC();

        if (healthAmount <= 0 || transform.position.y    <= _fallThreashold)
        {
            resetToStart();
            gameOver();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            Application.Quit();
        }
    }

    void GetPlayerMovement()
    {
        if (Input.GetKey(_left))
        {
            _rb.linearVelocityX = -1 * _maxSpeed;
            //_rb.linearVelocityX = Mathf.Lerp(_rb.linearVelocityX, -1 * _maxSpeed, Time.deltaTime * (_friction));
        }
        else if (Input.GetKey(_right))
        {
            _rb.linearVelocityX = 1 * _maxSpeed;
            //_rb.linearVelocityX = Mathf.Lerp(_rb.linearVelocityX, 1 * _maxSpeed, Time.deltaTime * (_friction));
        }
        else
        {
            _rb.linearVelocityX = Mathf.Lerp(_rb.linearVelocityX, 0, Time.deltaTime * _friction);
        }

        if (Input.GetKeyDown(_jump) && _isGrounded)
        {
            _rb.linearVelocityY = _jumpForce;
        }
    }

    void groundCheck()
    {
        RaycastHit2D groundHit = Physics2D.Raycast(transform.position, -1 * Vector2.up);

        if (groundHit)
        {
            float groundDist = Mathf.Abs(groundHit.point.y - transform.position.y);

            if(groundDist < (transform.localScale.y / 2) + .1f)
            {
                _isGrounded = true;
            }
            else
            {
                _isGrounded = false;
            }
        }
        else
        {
            _isGrounded = false;
        }
    }

    public void Hurt()
    {
        if (!bc)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            takeDamage(damage);
        }
    }

    public void takeDamage(float damage)
    {
        if (!bc)
        {
            healthAmount -= damage;
            healthAmount = Mathf.Clamp(healthAmount, 0, 100);
            _maxSpeed = 7f;
            healthBar.fillAmount = healthAmount / 100f;
        }
    }

    public void gameOver()
    {
        SceneManager.LoadScene(2);

       // gameOverCanvas.alpha = 1.0f;
       // gameOverCanvas.interactable = true;
       // gameOverCanvas.blocksRaycasts = true;
    }

    private void resetToStart()
    {
        transform.position = startingPos;
        _rb.linearVelocity = Vector2.zero;
        //sceneManager.LoadScene(sceneManager.getActiveScene().name);
    }

    private void UpdateBC()
    {
        bc = PlayerPrefs.GetInt("BCMode", 0) == 1;
    }
}
