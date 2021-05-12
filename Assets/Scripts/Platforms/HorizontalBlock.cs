using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//used to make a horizontally moving platform
public class HorizontalBlock : MonoBehaviour
{
    float xMax = -5;
    float xMin = 5;
    bool moveRight;
    Vector3 rightPos;
    Vector3 leftPos;
    float counter;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.transform.position.x < xMax)
        {
            moveRight = true;
        }
        else
        {
            moveRight = false;
        }
        this.setMoveSpan(xMax, xMin);
    }

    // Update is called once per frame
    void Update()
    {
        if(counter <= 0)
        {
            if (moveRight)
            {
                transform.position = Vector3.MoveTowards(transform.position, rightPos, 1f * Time.deltaTime);
                //checks if we've hit upper bound
                if (Vector3.Distance(transform.position, rightPos) <= 0.01f)
                {
                    counter = 2;
                    moveRight = false;
                    //Debug.Log("BUBUBUBU");
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, leftPos, 1f * Time.deltaTime);
                //checks if we've hit lower bound
                if (Vector3.Distance(transform.position, leftPos) <= 0.01f)
                {
                    counter = 2;
                    //Debug.Log("BUBUBUBU");
                    moveRight = true;
                }
            }
        }
        else
        {
            counter-= Time.deltaTime;
        }
        
    }
    //used for scaling block length, remember to use multiples of 5
    public void setLength(int blockLength)
    {

    }
    public void setMoveSpan(float xLeft, float xRight)
    {
        xMax = xRight;
        xMin = xLeft;
        rightPos = new Vector3(xMax, gameObject.transform.position.y, 5);
        leftPos = new Vector3(xMin, gameObject.transform.position.y, 5);
    }
}
