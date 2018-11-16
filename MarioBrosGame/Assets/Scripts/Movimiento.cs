using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour {

    //Variables movientos 
    public float velX = 0.03f;
    public float movX;
    public float inputX;

    //Variables salto
    public float fuerzaSalto = 350;
    public Transform pie;
    public float radioPie;
    public LayerMask suelo;
    public bool enSuelo;

    //Variables agachados
    public bool agachado;

    //Variables caida
    Rigidbody2D rb;
    public float caida;

    //Variables correr
    public bool correr;
    public int contadorTurbo = 0;

    //Variables turbo
    public bool turbo;

    //Variables de turbo salto
    public bool turboSalto;

    //Animaciones
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
		
	}

    private void FixedUpdate()
    {
        inputX= Input.GetAxis("Horizontal");
        if(!agachado)
        {
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
            }
        }
        else
        {

        }


        if (inputX != 0 && enSuelo)
        {
            animator.SetFloat("velX", 1);
        }
        else
        {
            animator.SetFloat("velX", 0);
        }

        //Salto

        enSuelo = Physics2D.OverlapCircle(pie.position, radioPie, suelo);
        if (enSuelo)
        {
            animator.SetBool("enSuelo", true);

            if (Input.GetKeyDown(KeyCode.Z) && !agachado)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, fuerzaSalto));
                animator.SetBool("enSuelo", false);
            }
        }
        else{
            animator.SetBool("enSuelo", false);
        }

        //Agacharse

        if(enSuelo && Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetBool("agachado", true);
            agachado = true;
        }
        else{
            animator.SetBool("agachado", false);
            agachado = false;
        }

        caida = rb.velocity.y;

        if(caida!= 0 || caida==0){
            animator.SetFloat("velY", caida);
        }


        //Correr

        if(inputX != 0)
        {
            if(Input.GetKey(KeyCode.X))
            {
                correr = true;
                velX = 0.06f;
                animator.SetBool("correr", true);
                //StartCoroutine(Turbo());
            }
            else{
                velX = 0.03f;
                correr = false;
                //turbo = false;
                animator.SetBool("correr", false);
                contadorTurbo = 0;
            }
        }
        else{
            correr = false;
            animator.SetBool("correr", false);
            contadorTurbo = 0;
        }

        /*if (inputX == 0)
        {
            animator.SetBool("correr", false);
            animator.SetBool("turbo", false);
            animator.SetBool("turboSalto", false);
        }*/

        //Turbo
        if (Input.GetKey(KeyCode.X) && correr && enSuelo)
        {
            StartCoroutine(Turbo());
        }else
        {
            turbo = false;
            animator.SetBool("turbo", false);
            StopAllCoroutines();
        }

        /*if (inputX > 0 || inputX < 0)
        {
            if (turbo && enSuelo)
            {
                animator.SetBool("turbo", true);
            }
            else
            {
                animator.SetBool("turbo", false);
            }
        }*/

        //Turbo salto
        if(inputX > 0 || inputX < 0)
        {
            if(turbo && Input.GetKey(KeyCode.X))
            {
                animator.SetBool("turboSalto", true);
            }
            else{
                animator.SetBool("turboSalto", false);
            }
        }
    }

    public IEnumerator Turbo()
    {
        while(contadorTurbo <= 15)
        {
            yield return new WaitForSeconds(0.5f);
            contadorTurbo++;
        }

        if(correr == true)
        {
            velX = 0.15f;
            correr = true;
            animator.SetBool("turbo", true);
        }
        else{
            StopCoroutine(Turbo ());
        }
    }
}
