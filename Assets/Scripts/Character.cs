using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit
{

  [SerializeField]
  private float speed = 3.0F;

  [SerializeField]
  private int lives = 5;

  public int Lives
  {
    get { return lives; }
    set
    {
      if (value < 5)
      {
        lives = value;
        livesBar.Refresh();
      }
    }
  }

  private LivesBar livesBar;

  [SerializeField]
  private float jumpForse = 15.0F;

  private bool isGrounded = false;

  private Bullet bullet;

  private State State
  {
    get { return (State)animator.GetInteger("State"); }
    set { animator.SetInteger("State", (int)value); }
  }

  new private Rigidbody2D rigidbody;
  private Animator animator;
  private SpriteRenderer sprite;

  private void Awake()
  {
    livesBar=FindObjectOfType<LivesBar>();

    rigidbody = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    sprite = GetComponentInChildren<SpriteRenderer>();

    bullet = Resources.Load<Bullet>("Bullet");
  }

  private void Update()
  {
    if (isGrounded) State = State.Idle;

    if (Input.GetButton("Horizontal"))
    {
      Run();
    }

    if (isGrounded && Input.GetButtonDown("Jump"))
    {
      Jump();
    }

    if (Input.GetButtonDown("Fire1")) Shoot();
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    // Unit unit = collision.gameObject.GetComponent<Unit>();
    // if (unit) ReceiveDamage();
  }

  private void FixedUpdate()
  {
    CheckGround();
  }


  private void Run()
  {
    Vector3 direction = transform.right * Input.GetAxis("Horizontal");
    transform.position = Vector3.MoveTowards(transform.position, transform.position + direction,
        speed * Time.deltaTime);

    sprite.flipX = direction.x < 0.0F;


    if (isGrounded) State = State.Run;
  }

  private void Jump()
  {
    rigidbody.AddForce(transform.up * jumpForse, ForceMode2D.Impulse);
  }

  public override void ReceiveDamage()
  {
    Lives--;


    rigidbody.velocity = Vector3.zero;
    rigidbody.AddForce(transform.up * 5.0F, ForceMode2D.Impulse);

    Debug.Log(lives);
  }

  private void Shoot()
  {
    Vector3 position = transform.position;
    position.y += 0.8F;
    Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

    newBullet.Parent = gameObject;
    newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -0.2F : 1.0F);
  }

  private void CheckGround()
  {
    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);

    isGrounded = colliders.Length > 1;

    if (!isGrounded) State = State.Jump;
  }



}

public enum State
{
  Idle,
  Run,
  Jump
}
