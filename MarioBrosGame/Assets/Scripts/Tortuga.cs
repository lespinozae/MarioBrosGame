using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tortuga : MonoBehaviour {

    public float posicionInicial;
    public float velTortuga;
    public float movimiento = 1f;
    public bool mirandoDerecha = true;
    Animator animator;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        posicionInicial = this.transform.position.x;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (mirandoDerecha)
        {
            if (this.transform.position.x > posicionInicial + movimiento)
            {
                mirandoDerecha = false;
                this.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                this.rb.velocity = new Vector3(velTortuga, rb.velocity.y, 0);
                animator.SetFloat("velX", velTortuga);
            }
        }
        else
        {
            if (this.transform.position.x < posicionInicial - movimiento)
            {
                mirandoDerecha = true;
                this.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                this.rb.velocity = new Vector3(-velTortuga, rb.velocity.y, 0);
                animator.SetFloat("velX", velTortuga);
            }
        }
    }
}
