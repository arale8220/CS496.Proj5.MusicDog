using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterMovement : MonoBehaviour {
    float speed = 5f;

    private float verticalVeclocity;
    private float gravity = 14.0f;
    private float jumpForce = 4.0f;

    public LayerMask groundLayers;

    


    private int CurrentBones;

    private CharacterController controller;

    private TextMeshProUGUI CurrentScore;

    public bool isGrounded = false;


	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        CurrentScore =  GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {

        
        
        

        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.localEulerAngles = new Vector3(0, 180, 0);
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.localEulerAngles = new Vector3(0, 90, 0);
            this.transform.Translate(0, 0, speed * Time.deltaTime);
            //this.transform.Rotate(new Vector3(0, 90, 0));
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.localEulerAngles = new Vector3(0, -90, 0);
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
        

        



        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;

        }

    }



    
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            Destroy(collision.gameObject);
            CurrentBones++;
            CurrentScore.text = "Bones " +CurrentBones.ToString()+"/3";

        }

        isGrounded = true;
    }
    public void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }


}
