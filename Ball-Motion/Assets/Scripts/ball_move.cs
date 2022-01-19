using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_move : MonoBehaviour
{
    public float speed=1;
    private Rigidbody rigb;

    // Start is called before the first frame update
    void Start()
    {
        rigb = GetComponent<Rigidbody>();
    }
    
    /*private void FixedUpdate()
    {
        
    }*/
    
    // Update is called once per frame
    void Update()
    {
        float moveHoriz = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHoriz, 0.0f, moveVert);
        rigb.AddForce(movement * speed);
    }
}
