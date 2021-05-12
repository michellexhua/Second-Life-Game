using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//used to make blocks move vertically
public class VerticalBlock : MonoBehaviour
{
    float yMax = -5;
    float yMin = 5;
    bool moveUp;
    Vector3 upperPos;
    Vector3 lowerPos;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.transform.position.y < yMax)
        {
            moveUp = true;
        }
        else
        {
            moveUp = false;
        }
        this.setMoveSpan(yMax, yMin);
    }

    // Update is called once per frame
    void Update()
    {
        if (moveUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, upperPos, 1f * Time.deltaTime);
            //checks if we've hit upper bound
            if (Vector3.Distance(transform.position, upperPos) <= 0.01f)
            {
                moveUp = false;
                //Debug.Log("BUBUBUBU");
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, lowerPos, 1f * Time.deltaTime);
            //checks if we've hit lower bound
            if(Vector3.Distance(transform.position, lowerPos) <= 0.01f)
            {
                //Debug.Log("BUBUBUBU");
                moveUp = true;
            }
        }
    }
    //used for scaling block length, remember to use multiples of 5
    public void setLength(int blockLength)
    {

    }
    public void setMoveSpan(float yTop, float yBottom)
    {
        yMax = yTop;
        yMin = yBottom;
        upperPos = new Vector3(gameObject.transform.position.x, yMax, 5);
        lowerPos = new Vector3(gameObject.transform.position.x, yMin, 5);
    }
}
