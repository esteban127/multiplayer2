using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController: NetworkBehaviour
{
    const int POTENCY = 300;
    [SyncVar] int axisXDirection = 1;
    [SyncVar] int axisYDirection = 1;
    [SyncVar] int playersCount = 0;
    [SyncVar] public Color myColor;
    [SyncVar] Color platformColor;
    Rigidbody rb;
    [SerializeField] Color[] colors;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();        
        GetComponentInChildren<Renderer>().material.color = colors[netId.Value];
        myColor = colors[netId.Value];
        floor.instance.SetColor(colors[2]);
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
                CmdMoveBall(new Vector3(0, 0, Input.GetAxisRaw("Vertical")) * Time.deltaTime * axisYDirection, new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0) * Time.deltaTime * axisXDirection);           
        }  
        
        if(transform.position.y < -2)
        {
            Destroy(gameObject);
        }
    }
    public void SetColor()
    {
        GetComponentInChildren<Renderer>().material.color = myColor;
        floor.instance.SetColor(platformColor);
    }

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.tag == "Player")
        {
            if (isLocalPlayer)
            {
                CmdColision(collision.gameObject);
            }
            SetColor();
        }        
    }
    [Command]
    public void CmdColision(GameObject collision)
    {
        int rand = Random.Range(0, 4);
        myColor = (colors[rand + 1]);
        rand = Random.Range(0, 4);
        platformColor = (colors[rand + 1]);
        switch (rand)
        {
            case 0:
                axisXDirection = -1;
                axisYDirection = 1;
                break;
            case 1:
                axisXDirection = 1;
                axisYDirection = 1;
                break;
            case 2:
                axisXDirection = -1;
                axisYDirection = -1;
                break;
            case 3:
                axisXDirection = 1;
                axisYDirection = -1;
                break;
        }
    }

    [Command] public void CmdMoveBall (Vector3 verticalMove, Vector3 horizontalMove)
    {
        if (horizontalMove.magnitude != 0)
        {
            rb.AddForce(horizontalMove*POTENCY);
        }
        if (verticalMove.magnitude != 0)
        {
            rb.AddForce(verticalMove*POTENCY);
        }

    }
}
