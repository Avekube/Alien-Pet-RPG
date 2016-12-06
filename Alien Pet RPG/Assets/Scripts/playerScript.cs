using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour
{

    public int speed;
    public Vector2 currentDirection;
    public AudioClip playerWalkingClip;

    protected Animator anim;
    //protected AudioSource playerWalkingSoundSource;

    private Vector2 movement;

    


    void Start ()
    {
        speed = 50;
        anim = GetComponent<Animator>();
        movement = new Vector2();
	}
	
	void Update ()
    {
        movePlayer();
        
        changeAnimationStates();

    }

    protected void movePlayer()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        Vector2 inputVector = new Vector2(inputX, inputY);

        movement = getNextDirection(inputVector)*speed;
    }

    protected Vector2 getNextDirection(Vector2 inputtedDirection)
    {
        Vector2 resultVector = new Vector2();
        speed = 50; //please change this later from being hardcoded
        
        switch (inputtedDirection.ToString())
        {
            case "(0.0, -1.0)": //down
                resultVector = Vector2.down;
                currentDirection = Vector2.down;
                break;              
            case "(0.0, 1.0)": //up
                resultVector = Vector2.up;
                currentDirection = Vector2.up;
                break;
            case "(-1.0, 0.0)": //left
                resultVector = Vector2.left;
                currentDirection = Vector2.left;
                break;
            case "(1.0, 0.0)":  //right
                resultVector = Vector2.right;
                currentDirection = Vector2.right;
                break;
            case "(0.0, 0.0)":
                speed = 0;
                break;
        }

       return resultVector;
    }

    protected void changeAnimationStates()
    {
        Debug.Log(movement.normalized.ToString());
        Vector2 temp = new Vector2();
        
        switch (movement.normalized.ToString())
        {
            case "(0.0, -1.0)": //down
                anim.SetBool("isWalking", true);
                temp = Vector2.down;
                break;
            case "(-1.0, 0.0)": //left
                anim.SetBool("isWalking", true);
                temp = Vector2.left;
                break;
            case "(1.0, 0.0)":  //right
                anim.SetBool("isWalking", true);
                temp = Vector2.right;
                break;
            case "(0.0, 1.0)": //up
                anim.SetBool("isWalking", true);
                temp = Vector2.up;
                break;
            case "(0.0, 0.0)":
                anim.SetBool("isWalking", false);
                temp = Vector2.zero;
                anim.SetFloat("currentX", currentDirection.x);
                anim.SetFloat("currentY", currentDirection.y);
                break;
        }
        anim.SetFloat("inputY", temp.y);
        anim.SetFloat("inputX", temp.x);
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = movement;
    }
}
