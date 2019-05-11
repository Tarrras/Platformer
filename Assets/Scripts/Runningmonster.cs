using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Runningmonster : Monster
{

  [SerializeField]
  private float speed = 2.0F;

  private SpriteRenderer sprite;
  private Vector3 direction;

  protected override void Awake()
  {
    sprite = GetComponentInChildren<SpriteRenderer>();
  }

  protected override void Start()
  {
    direction = transform.right;
  }

  protected override void Update()
  {
    Move();
  }

  protected override void OnTriggerEnter2D(Collider2D collider)
  {
    Unit unit = collider.GetComponent<Unit>();

    if (unit && unit is Character)
    {
      if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.3F) ReceiveDamage();
      else unit.ReceiveDamage();
    }
  }

  private void Move()
  {

    Collider2D[] coliders = Physics2D.OverlapCircleAll(
      transform.position + transform.up * 0.5F + transform.right * direction.x * 0.7F, 0.2F);

    if (coliders.Length > 0 && coliders.All(x => !x.GetComponent<Character>())) direction *= -1;

    transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
  }

}
