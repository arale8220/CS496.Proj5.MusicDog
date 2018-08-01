using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour {
    float speed = 5f;
    float rotSpeed = 2f;

    private float verticalVeclocity;
    private float jumpForce = 7000.0f;

    public LayerMask groundLayers;
    public Camera cam;
    public GameObject AudioP;

    


    private int CurrentBones=0;

    private CharacterController controller;

    private TextMeshProUGUI CurrentScore;


	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        CurrentScore =  GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
    }
	
	// Update is called once per frame
	void Update () {
        MoveCtrl();
        RotCtrl();
    }


    void MoveCtrl()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            //this.transform.localEulerAngles = new Vector3(0, 0, 0);
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            this.GetComponent<Animation>().Play("run");
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //this.transform.localEulerAngles = new Vector3(0, 180, 0);
            this.transform.Translate(Vector3.back * speed * Time.deltaTime);
            this.GetComponent<Animation>().Play("run");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //this.transform.localEulerAngles = new Vector3(0, 90, 0);
            this.transform.Translate(Vector3.right * speed * Time.deltaTime);
            //this.transform.Rotate(new Vector3(0, 90, 0));
            this.GetComponent<Animation>().Play("run");
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //this.transform.localEulerAngles = new Vector3(0, -90, 0);
            this.transform.Translate(Vector3.left * speed * Time.deltaTime);
            this.GetComponent<Animation>().Play("run");
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            this.GetComponent<Animation>().Play("stand");

        }

        if (!Input.anyKey) this.GetComponent<Animation>().Play("stand");

    }

    void RotCtrl()
    {
        float rotX = Input.GetAxis("Mouse Y") * rotSpeed;
        float rotY = Input.GetAxis("Mouse X") * rotSpeed;
        this.transform.localRotation *= Quaternion.Euler(0,rotY, 0);
        cam.transform.localRotation *= Quaternion.Euler(-rotX,0,0);
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            
            CurrentBones++;
            CurrentScore.text = "Acorns " +CurrentBones.ToString()+"/7";
            if (CurrentBones > 6)
            {
                GameObject.Find("Score").SetActive(false);
                GameObject.Find("Finished").transform.Find("ftxt").gameObject.SetActive(true);
                GameObject.Find("Finished").transform.Find("fbtn").gameObject.SetActive(true);
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "CollItem")
        {
            AudioP.GetComponents<AudioSource>()[2].Play();
        }
    }

}
