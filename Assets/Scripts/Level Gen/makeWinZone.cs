using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeWinZone : MonoBehaviour
{
    public GameObject block;
    public GameObject winZone;

    public GameObject parentBlock;
    public GameObject currentBlock;
    float relativeRight;
    //public GameObject newColl;
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
        relativeRight = currRight;
        parentBlock = Instantiate(block, new Vector3(-7, -10, 5), Quaternion.identity);
        parentBlock.name = "sliceParent" + sliceNum;
        currentBlock = Instantiate(block, new Vector3(relativeRight, 0, 5), Quaternion.identity);
        currentBlock.transform.SetParent(parentBlock.transform);
        relativeRight+=5;
        currentBlock = Instantiate(block, new Vector3(relativeRight, 0, 5), Quaternion.identity);
        currentBlock.transform.SetParent(parentBlock.transform);

        currentBlock = Instantiate(winZone, new Vector3(relativeRight, 3, 5), Quaternion.identity);
        currentBlock.transform.SetParent(parentBlock.transform);

        relativeRight += 5;
        currentBlock = Instantiate(block, new Vector3(relativeRight, 0, 5), Quaternion.identity);
        currentBlock.transform.SetParent(parentBlock.transform);
    }
}
