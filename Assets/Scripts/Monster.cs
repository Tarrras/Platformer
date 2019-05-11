﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{

  protected virtual void OnTriggerEnter2D(Collider2D collider)
  {
    Bullet bullet = collider.GetComponent<Bullet>();

    if (bullet)
    {
      ReceiveDamage();
    }
  }

  protected virtual void Awake() { }
  protected virtual void Start() { }
  protected virtual void Update() { }
}
