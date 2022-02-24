using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    float speed = 3.0f;
    [SerializeField]
    float jumpForce = 7.0f;

    [SerializeField, Range(0.01f, 10f)]
    float rayDistance = 2f;
    [SerializeField]
    Color rayColor = Color.red;
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    Vector3 rayOrigin;

    Rigidbody2D rb2D;
    GameInputs gameInputs;
    SpriteRenderer sprR;
    Animator anim;

    [SerializeField]
    ArtScore score;

    [SerializeField]
    HealthScript hs;
    int damage = 1;
    
    IEnumerator dead, walk, timer;
    float walkTimer = 0f;
    [SerializeField, Range(0.1f, 2f)]
    float walkTimeLimit = 1f;

    bool touchingHead = false;
    AudioSource aud;
    [SerializeField]
    AudioClip walkSFX, jumpSFX, pickupSFX, hitSFX, winSFX, deadSFX;
    void Awake()
    {
        gameInputs = new GameInputs();
    }

    void OnEnable()
    {
        gameInputs.Enable();
    }
    
    void OnDisable()
    {
        gameInputs.Disable();
    }
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        sprR = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        gameInputs.Gameplay.Jump.performed += _=> Jump();
        gameInputs.Gameplay.Jump.canceled += _=> JumpCanceled();

        walkTimer = walkTimeLimit;
        gameInputs.Gameplay.AxisX.performed += _=>  StartWalk();
        gameInputs.Gameplay.AxisX.canceled += _=>  StopWalk();
    }

    void Update()
    {
        sprR.flipX = FlipSprite;
        if(!IsGrounding) //sirve ahorita, alomejor cuando incluya animación de salto habrá q cambiarla.
        {
            anim.SetFloat("Blend", 0);
        }

        if(hs.healthUI <= 0)
        {
            dead = TimeDead();
            StartCoroutine(dead);
        }
    }

    
    void FixedUpdate()
    {
        rb2D.position += (Vector2.right * Axis.x * speed * Time.fixedDeltaTime);
    }

    void Jump()
    {
        if(IsGrounding) //
        {
            aud.PlayOneShot(jumpSFX, 1f);
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("jump");
            touchingHead = false;
        }
    }

    void JumpCanceled()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, 0f);
    }

    void LateUpdate()
    {
        anim.SetFloat("Blend", Mathf.Abs(Axis.x));
        anim.SetBool("ground", IsGrounding);
    }

    IEnumerator WalkRoutine()
    {
        while(true)
        {
            if(IsGrounding) aud.PlayOneShot(walkSFX, 0.3f);
            yield return new WaitForSeconds(walkTimeLimit);
        }
    }

    void StartWalk()
    {
        if(IsGrounding)
        {
            walk = WalkRoutine();
            StartCoroutine(walk);
        }
        
    }

    void StopWalk()
    {
       StopCoroutine(walk);
    }


    Vector2 Axis => new Vector2(gameInputs.Gameplay.AxisX.ReadValue<float>(), gameInputs.Gameplay.AxisY.ReadValue<float>());

    bool FlipSprite => Axis.x > 0 ? false : Axis.x<0 ? true : sprR.flipX;
    bool IsGrounding => Physics2D.Raycast(transform.position + rayOrigin, Vector2.down, rayDistance, groundLayer) || touchingHead;
    public bool TouchingHead {get => touchingHead; set => touchingHead = value;}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = rayColor;
        Gizmos.DrawRay(transform.position + rayOrigin, Vector2.down * rayDistance);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("art"))
        {
            aud.PlayOneShot(pickupSFX, 1f);
            ArtScript art = col.GetComponent<ArtScript>();
            score.AddPoints(art.GetPoints);
            Destroy(col.gameObject);
        }
        if(col.CompareTag("vacio"))
        {
            dead = TimeDead();
            StartCoroutine(dead);
        }
        if(col.CompareTag("meta") && (score.score>=5))
        {
            timer = TimerW();
            StartCoroutine(timer);
        }
    }

    public void RunAnimationDamage()
    {
        aud.PlayOneShot(hitSFX, 1f);
        anim.SetTrigger("damage");
        hs.RemoveHealth(damage);
    } 

    IEnumerator TimeDead()
    {
        aud.PlayOneShot(deadSFX, 0.5f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
    }

    IEnumerator TimerW()
    {
        aud.PlayOneShot(winSFX, 1f);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(3);
    }

}
