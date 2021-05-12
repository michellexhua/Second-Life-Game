using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//horizontal moving platforms
public class makeHorizPlatform : MonoBehaviour
{
    public GameObject block;
    public GameObject movingBlockH;
    public GameObject testFlag;
    //used to gen hierarchy
    public GameObject parentBlock;
    public GameObject currentBlock;
    //used to measure positions relative to the current level slice
    float sliceStart;
    float sliceEnd;
    float walkStart;
    float walkEnd;
    //pointer to currently generated x position
    float relativeRight = 0f;
    int randStore;
    int height;
    int count = 0;
    public bool recentMove;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void gen(float currRight, int sliceNum, float sliceLength)
    {
        parentBlock = Instantiate(block, new Vector3(-7, -10, 5), Quaternion.identity);
        parentBlock.name = "sliceParent" + sliceNum;


        sliceStart = currRight;
        relativeRight = sliceStart;
        sliceEnd = sliceStart + sliceLength;
        walkStart = currRight;
        recentMove = true;
        while (relativeRight <= sliceEnd)
        {
            //note that 2 consecutive moving platforms are an impassible obstacle, plus we need to check overflow of slices
            if (sliceEnd - relativeRight <= 30 || count == 0)
            {
                Debug.Log("END SLICE PART");
                randStore = 0;
            }
            else if (sliceEnd - relativeRight > 30 && !recentMove)
            {
                randStore = Random.Range(0, 7);
            }
            else{
                randStore = Random.Range(0, 4);
            }
            
            if (randStore == 3)
            {
                Debug.Log("Height swap");
                if (height == 0)
                {
                    height = 1;
                    
                    currentBlock = Instantiate(block, new Vector3(relativeRight, 2, 5), Quaternion.identity);
                    currentBlock.transform.SetParent(parentBlock.transform);
                }
                else
                {
                    height = 0;
                    
                    currentBlock = Instantiate(block, new Vector3(relativeRight, 0, 5), Quaternion.identity);
                    currentBlock.transform.SetParent(parentBlock.transform);
                }
                recentMove = false;
            }
            //jump of 5 units
            else if (randStore == 2)
            {
                relativeRight += 5;

                currentBlock = Instantiate(block, new Vector3(relativeRight, 0, 5), Quaternion.identity);
                currentBlock.transform.SetParent(parentBlock.transform);
                recentMove = false;
            }
            //jump of 10 units
            else if (randStore == 1)
            {
                relativeRight += 10;

                currentBlock = Instantiate(block, new Vector3(relativeRight, 0, 5), Quaternion.identity);
                currentBlock.transform.SetParent(parentBlock.transform);
                recentMove = false;
            }
            //do regular block placement at same level
            else if (randStore == 0)
            {
                if (height == 0)
                {
                    currentBlock = Instantiate(block, new Vector3(relativeRight, 0, 5), Quaternion.identity);
                    currentBlock.transform.SetParent(parentBlock.transform);
                   
                }
                else if (height == 1)
                {
                    currentBlock = Instantiate(block, new Vector3(relativeRight, 2, 5), Quaternion.identity);
                    currentBlock.transform.SetParent(parentBlock.transform);
                    


                }
                recentMove = false;
            }
            //5 unit moving plat
            
            else if (randStore == 4)
            {
                relativeRight += 5;
                
                currentBlock = Instantiate(movingBlockH, new Vector3(relativeRight, 0, 5), Quaternion.identity);
                currentBlock.GetComponent<HorizontalBlock>().setMoveSpan(relativeRight-5, relativeRight + 5);
                relativeRight += 5;
                currentBlock.transform.SetParent(parentBlock.transform);
                recentMove = true;
            }
            //10 unit moving plat
            else if (randStore == 5)
            {
                relativeRight += 5;

                currentBlock = Instantiate(movingBlockH, new Vector3(relativeRight, 0, 5), Quaternion.identity);
                currentBlock.GetComponent<HorizontalBlock>().setMoveSpan(relativeRight - 5, relativeRight + 5);
                currentBlock.transform.SetParent(parentBlock.transform);
                relativeRight += 5;
                currentBlock = Instantiate(movingBlockH, new Vector3(relativeRight, 0, 5), Quaternion.identity);
                currentBlock.GetComponent<HorizontalBlock>().setMoveSpan(relativeRight - 5, relativeRight + 5);
                relativeRight += 5;

                currentBlock.transform.SetParent(parentBlock.transform);
                recentMove = true;
            }
            //15 unit moving plat
            else if (randStore == 6)
            {
                relativeRight += 5;

                currentBlock = Instantiate(movingBlockH, new Vector3(relativeRight, 0, 5), Quaternion.identity);
                currentBlock.GetComponent<HorizontalBlock>().setMoveSpan(relativeRight - 5, relativeRight + 5);
                currentBlock.transform.SetParent(parentBlock.transform);
                relativeRight += 5;
                currentBlock = Instantiate(movingBlockH, new Vector3(relativeRight, 0, 5), Quaternion.identity);
                currentBlock.GetComponent<HorizontalBlock>().setMoveSpan(relativeRight - 5, relativeRight + 5);
                currentBlock.transform.SetParent(parentBlock.transform);
                relativeRight += 5;
                currentBlock = Instantiate(movingBlockH, new Vector3(relativeRight, 0, 5), Quaternion.identity);
                currentBlock.GetComponent<HorizontalBlock>().setMoveSpan(relativeRight - 5, relativeRight + 5);
                relativeRight += 5;

                currentBlock.transform.SetParent(parentBlock.transform);
                recentMove = true;
            }
            
            relativeRight += 5f;
            count++;
            //Debug.Log(relativeRight + " out of " + sliceEnd);
        }
        //currently 0, 1 will be stay on same height, 2 will be change height
        count = 0;
        currentBlock = Instantiate(testFlag, new Vector3(relativeRight, 5, 5), Quaternion.identity);
        currentBlock.transform.SetParent(parentBlock.transform);
        //Instantiate(testFlag, new Vector3(relativeRight-5, 5, 5), Quaternion.identity);
        Debug.Log("level gen horizontal platform done: " + count);

    }
}
