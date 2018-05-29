using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody2D rigidbody;
    DistanceJoint2D graplingHook;
    RaycastHit2D hit;
    float movementx;
    bool readyToJump;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        graplingHook = GetComponent<DistanceJoint2D>();
        readyToJump = false;
        graplingHook.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        movementx = Input.GetAxis("Horizontal");
        rigidbody.AddForce(new Vector2(movementx * 10,0 ));

        if (Input.GetButtonDown("Jump") && readyToJump) {
            rigidbody.AddForce(new Vector2(0, 500f));
            readyToJump = false;
        }

        if (Input.GetButtonDown("Jump")) {
            graplingHook.enabled = false;
        }

        if (Input.GetButtonDown("Fire1")) {

            if (graplingHook.enabled == false) {
                GetComponent<CircleCollider2D>().enabled = false;

                hit = Physics2D.Raycast(transform.position, new Vector2(rigidbody.velocity.x, 10f));


                GetComponent<CircleCollider2D>().enabled = true;
                Debug.Log("Raycast suoritettu");
                if (hit.collider != null)
                {
                    graplingHook.enabled = true;
                    readyToJump = false;
                    graplingHook.connectedAnchor = hit.point;
                    graplingHook.distance = Vector2.Distance(transform.position, hit.point);
                    Debug.Log("Tag of hit object : " + hit.collider.gameObject.tag);
                    GetComponent<LineRenderer>().SetPosition(1, hit.point);
                }
            }
            else {
                graplingHook.enabled = false;
            }
        }

        GetComponent<LineRenderer>().SetPosition(0, transform.position);

        if (graplingHook.enabled == true)
        {
            GetComponent<LineRenderer>().enabled = true;
        }
        if (graplingHook.enabled == false)
        {
            GetComponent<LineRenderer>().enabled = false;
        }
        Debug.Log("Rigidbody : " + rigidbody.velocity.x);


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.transform.position.y < transform.position.y) {
            readyToJump = true;
        }
    }
}
