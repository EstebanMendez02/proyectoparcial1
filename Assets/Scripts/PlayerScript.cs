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
    
    IEnumerator dead;
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
        gameInputs.Gameplay.Jump.performed += _=> Jump();
        gameInputs.Gameplay.Jump.canceled += _=> JumpCanceled();
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
        if(IsGrounding || GameManager.instance.GetEnemy.top)
        {
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("jump");
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

    Vector2 Axis => new Vector2(gameInputs.Gameplay.AxisX.ReadValue<float>(), gameInputs.Gameplay.AxisY.ReadValue<float>());

    bool FlipSprite => Axis.x > 0 ? false : Axis.x<0 ? true : sprR.flipX;
    bool IsGrounding => Physics2D.Raycast(transform.position + rayOrigin, Vector2.down, rayDistance, groundLayer) || GameManager.instance.GetEnemy.top;


    void OnDrawGizmosSelected()
    {
        Gizmos.color = rayColor;
        Gizmos.DrawRay(transform.position + rayOrigin, Vector2.down * rayDistance);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("art"))
        {
            ArtScript art = col.GetComponent<ArtScript>();
            score.AddPoints(art.GetPoints);
            Destroy(col.gameObject);
        }
        if(col.CompareTag("vacio"))
        {
            SceneManager.LoadScene(1);
        }
        if(col.CompareTag("meta") && (score.score>=5))
        {
            SceneManager.LoadScene(2);
        }
    }

    public void RunAnimationDamage()
    {
        anim.SetTrigger("damage");
        hs.RemoveHealth(damage);
    } 

    IEnumerator TimeDead()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }

}
