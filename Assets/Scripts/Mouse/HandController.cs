﻿using System;
using UnityEngine;

public class HandController : MonoBehaviour {

  public float maxSpeed;
  public bool isShaking;
  public float shakeAmount = 5f;

  private Vector2 shake = Vector2.zero;
  private Rigidbody2D rb;

  private void Start() {
    this.rb = this.GetComponent<Rigidbody2D>();
  }

  private Vector2 GetMousePos() {
    return Camera.main.ScreenToWorldPoint(Input.mousePosition);
  }

  private void Move() {
    Vector2 difference = this.GetMousePos() - (Vector2) this.transform.position;
    this.rb.MovePosition(
      this.rb.position
      + difference.normalized
      * Mathf.Min(this.maxSpeed * Time.fixedDeltaTime, difference.magnitude)
      + shake * Time.fixedDeltaTime
    );
  }

  private float RandomRange(float maxAngle) {
    return UnityEngine.Random.Range(-maxAngle, maxAngle);
  }

  private Vector2 AngleToVector(float angle) {
    return new Vector2((float) Math.Cos(angle), (float) Math.Sin(angle));
  }

  private void Shake() {
    float angle = this.RandomRange((float) Math.PI);
    this.shake = this.AngleToVector(angle) * this.shakeAmount;
  }

  private void FixedUpdate() {
    if(isShaking) {
      this.Shake();
    }
    Move();
  }
}
