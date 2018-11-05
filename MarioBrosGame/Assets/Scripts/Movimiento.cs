using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour {

    public float velX = 0.1f;
    public float movX;
    public float inputX;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        inputX = Input.GetAxis("Horizontal");

        Debug.Log(inputX);
		
	}
}
