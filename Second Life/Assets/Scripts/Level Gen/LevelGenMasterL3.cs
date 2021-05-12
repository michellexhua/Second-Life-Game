using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenMasterL3 : MonoBehaviour
{
    GameObject player;
    GameObject grabSlice;
    float check = 0;
    bool endCheck = false;
    //initialize variables
    int levelSection = 1;
    int randGen = 0;
    float currRightPos = 5f;//records rightmost point of level generated
    float playerPos = 0;
    public static float levelSlice = 0f;//size of each generated level section
    int randLength = 0;
    int levelNum = 3;//level 3
    string lastZoneTag = null;//to check if last section was death zone, prevents consecutive death zones

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        randGen = Random.Range(1, 5);//1 of 5 platform types chosen at random
        //randGen = 1;
        //normal or moving section, 10 to 20 5x5 blocks long
        randLength = Random.Range(10, 21) * 5;
        //randGen = 1;//testing
        levelSlice += (float)randLength;

        if (randGen == 1)
        {
            Debug.Log("Level section flat" + levelSection + "=" + randGen + ", length = " + levelSlice);
            gameObject.GetComponent<makeSect1>().gen(currRightPos, levelSection, levelSlice, levelNum);

        }
        if (randGen == 2)
        {
            Debug.Log("Level section Jump" + levelSection + "=" + randGen);
            gameObject.GetComponent<makeSect2>().gen(currRightPos, levelSection, levelSlice, levelNum);

        }
        if (randGen == 3)
        {
            Debug.Log("Level section Horiz" + levelSection + "=" + randGen + ", length = " + levelSlice);
            gameObject.GetComponent<makeHorizPlatform>().gen(currRightPos, levelSection, levelSlice);
        }
        if (randGen == 4)
        {
            Debug.Log("Level section Vert" + levelSection + "=" + randGen);
            gameObject.GetComponent<makeVerticPlatform>().gen(currRightPos, levelSection, levelSlice);
        }
        if (randGen == 5)
        {
            //Debug.Log("Level section Death " + levelSection + "=" + randGen);
            gameObject.GetComponent<makeDeathZone>().gen(currRightPos, levelSection, levelSlice);
        }
        levelSection++;
        currRightPos += levelSlice + 5;
        randLength = 0;
        check = currRightPos - (levelSlice / 2);
    }

    // Update is called once per frame
    void Update()
    {
        //currRightPos + levelSlice / 2
        playerPos = player.transform.position.x;
        if (playerPos >= check && levelSection <= 10)
        {
            //Debug.Log("Time for kill" + playerPos + "/" + currRightPos + levelSlice / 2);
            Debug.Log("Time for kill" + playerPos + "/" + currRightPos + levelSlice / 2 + "Levelsection:" + levelSection);
            if (levelSection >= 2)
            {
                grabSlice = GameObject.Find("sliceParent" + (levelSection - 2));
                if (grabSlice != null)
                {
                    Destroy(grabSlice);
                }
            }

            Debug.Log("AAAA");

            randGen = Random.Range(1, 5);//1 of 5 platform types chosen at random
            //randGen = 1;
            //normal or moving section, 10 to 20 5x5 blocks long
            randLength = Random.Range(10, 21) * 5;

            //randGen = 1;//testing
            levelSlice = (float)randLength;

            if (randGen == 1)
            {
                Debug.Log("Level section flat" + levelSection + "=" + randGen + ", length = " + levelSlice);
                gameObject.GetComponent<makeSect1>().gen(currRightPos, levelSection, levelSlice, levelNum);
            }
            if (randGen == 2)
            {
                Debug.Log("Level section Jump" + levelSection + "=" + randGen);
                gameObject.GetComponent<makeSect2>().gen(currRightPos, levelSection, levelSlice, levelNum);

            }
            if (randGen == 3)
            {
                Debug.Log("Level section Horiz" + levelSection + "=" + randGen + ", length = " + levelSlice);
                gameObject.GetComponent<makeHorizPlatform>().gen(currRightPos, levelSection, levelSlice);
            }
            if (randGen == 4)
            {
                Debug.Log("Level section Vert" + levelSection + "=" + randGen);
                gameObject.GetComponent<makeVerticPlatform>().gen(currRightPos, levelSection, levelSlice);
            }
            if (randGen == 5)
            {
                //Debug.Log("Level section Death " + levelSection + "=" + randGen);
                //check if preceeding section was a death zone
                /*lastZoneTag = GameObject.Find("sliceParent" + (levelSection - 1)).tag;
                //only make new death zone if last zone was not death zone
                if (!string.Equals(lastZoneTag, "deathZone"))
                {
                    gameObject.GetComponent<makeDeathZone>().gen(currRightPos, levelSection, levelSlice);
                }*/
                gameObject.GetComponent<makeDeathZone>().gen(currRightPos, levelSection, levelSlice);
            }
            levelSection++;
            currRightPos += levelSlice + 5;
            randLength = 0;
            check = currRightPos - (levelSlice / 2);

            //levelSlice = 0f;
            lastZoneTag = null;
        }
        else if (levelSection == 11)
        {
            if (endCheck == false)
            {
                //Debug.Log("No kill" + playerPos + "/" + currRightPos);
                gameObject.GetComponent<makeWinZone>().gen(currRightPos, levelSection, levelSlice);
                endCheck = true;
            }

        }
        else
        {
            check = currRightPos - levelSlice / 2;
            //limit player from moving off-screen
            player.GetComponent<PlayerController>().SetMinX(check);
            Debug.Log("No kill" + playerPos + "/" + check);
        }

    }
}