﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour {

    //Variables movientos 
    public float velX = 0.1f;
    public float movX;
    public float inputX;

    //Variables salto
    public float fuerzaSalto = 350;
    public Transform pie;
    public float radioPie;
    public LayerMask suelo;
    public bool enSuelo;

    //Animaciones
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>(); 
    }

    // Use this for initialization
    void Start () {
		
	}

    private void FixedUpdate()
    { 
        inputX= Input.GetAxis("Horizontal");

        if (inputX > 0)
        {
            movX = transform.position.x + (inputX * velX);
            transform.position = new Vector3(movX, transform.position.y, 0);
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (inputX < 0)
        {
            movX = transform.position.x + (inputX * velX);
            transform.position = new Vector3(movX, transform.position.y, 0);
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetFloat("velX", Mathf.Abs(inputX));
        }

        //Salto

        enSuelo = Physics2D.OverlapCircle(pie.position, radioPie, suelo);

        Debug.Log(enSuelo);

        if (enSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, fuerzaSalto));
        }
      }
}
