using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class make1sectL3 : MonoBehaviour
{
    public GameObject block;
    public GameObject testFlag;
    public GameObject collectable;
    public GameObject powerUp1;
    public GameObject powerUp2;
    public GameObject powerUp3;
    public GameObject powerUp4;
    public GameObject powerUp5;
    public GameObject powerUp6;
    public GameObject opmPowerUp;//holds instant-kill power up prefab
    public GameObject paladin;

    //these are used for heirarchy gen
    public GameObject parentBlock;
    public GameObject currentBlock;
    public GameObject newColl;
    public GameObject newPU;
    public GameObject newOpm;
    public GameObject newPaladin;

    //used to measure positions relative to the current level slice
    float sliceStart;
    float sliceEnd;

    //used to determine area that AI things can walk across, pass these to AI handlers when instantiated
    float walkStart;
    float walkEnd;

    //code to determine enemy types allowed
    int option = 0;
    bool allowPaladin = false;

    //code to generate Paladins
    int paladinNum = 0;//num of Paladins allowed on current section
    int paladinChance = 0;//chance of Lesser Sprite spawn on current block
    bool genPaladin = false;

    //code to generate power ups
    int powerUpNum = 0;
    int powerUpChance = 0;
    bool genPowerUp = false;
    int powerUpChoice = 0;
    GameObject powerUp = null;
    bool genOnePunch = false;//bool for instant-kill power up, which has separate rules
    int opmChance = 0;
    int opmNum = 0;

    //code to generate collectables
    int collNum = 0;//num of collectables allowed on current section
    int collChance = 0;
    bool genColl = false;


    float relativeRight = 0f;//pointer to currently generated x position
    int count = 0;//count of platforms
    int randStore = 0;
    int height = 0;//used to track platform height

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //code for entire section
    public void gen(float currRight, int sliceNum, float sliceLength)
    {
        sliceStart = currRight;
        relativeRight = sliceStart;
        sliceEnd = sliceStart + sliceLength;//receives slice length


        //roll dice for enemies option in current section
        option = Random.Range(1, 3);//option is 1 or 2

        //no enemies generated in section if option = 1
        if (option == 2)
        {
            allowPaladin = true;
        }
        else
        {
            allowPaladin = false;
        }

        walkStart = currRight;
        parentBlock = Instantiate(block, new Vector3(-7, -10, 5), Quaternion.identity);
        parentBlock.name = "sliceParent" + sliceNum;


        while (relativeRight <= sliceEnd)
        {
            if (height == 0)
            {
                currentBlock = Instantiate(block, new Vector3(relativeRight, 0, 5), Quaternion.identity);
                currentBlock.transform.SetParent(parentBlock.transform);
                walkEnd += 5;

                //code for collectables
                //roll dice. If collChance is 41 to 100, giving a 60% chance, collectable will be generated
                collChance = Random.Range(1, 101);//chance of collectable is number between 1 & 100
                if (collChance > 40)
                {
                    genColl = true;
                }
                else
                {
                    genColl = false;
                }

                //generate collectable if allowed
                if (genColl)
                {
                    //code to generate collectable at spawn point as child of currentBlock
                    newColl = Instantiate(collectable, new Vector3(relativeRight, 1, 5), Quaternion.identity);
                    newColl.transform.SetParent(currentBlock.transform);
                    collNum++;

                }

                //code for enemies
                if (allowPaladin)
                {
                    genPaladin = spawnPaladin(paladinChance);
                    //generate Paladin if allowed
                    if (genPaladin)
                    {
                        //as long as there are less than 4 already existing Lesser Sprites
                        if (paladinNum < 1)
                        {
                            //code to generate collectable at spawn point as child of currentBlock
                            newPaladin = Instantiate(paladin, new Vector3(relativeRight, 2, 5), Quaternion.identity);
                            newPaladin.transform.SetParent(currentBlock.transform);
                            paladinNum++;
                        }
                    }
                }

                //code for regular power ups
                genPowerUp = spawnPowerUp(powerUpChance);

                //generate power up if allowed
                if (genPowerUp)
                {
                    if (powerUpNum < 3)
                    {
                        powerUp = choosePowerUp(powerUpChoice);
                        //code to generate collectable at spawn point as child of currentBlock
                        newPU = Instantiate(powerUp, new Vector3(relativeRight, 3, 5), Quaternion.identity);
                        newPU.transform.SetParent(currentBlock.transform);
                        powerUpNum++;
                    }
                }

                //code for instant-kill power up
                genOnePunch = spawnInstantKill(opmChance);
                if (genOnePunch)
                {
                    if (opmNum < 2)
                    {
                        newOpm = Instantiate(opmPowerUp, new Vector3((relativeRight + 2f), 3, 5), Quaternion.identity);
                        newOpm.transform.SetParent(currentBlock.transform);
                        opmNum++;
                    }
                }
            }
            else if (height == 1)
            {
                currentBlock = Instantiate(block, new Vector3(relativeRight, 1, 5), Quaternion.identity);
                currentBlock.transform.SetParent(parentBlock.transform);
                walkEnd += 5;

                //code for collectables
                //roll dice. If collChance is 41 to 100, giving a 60% chance, collectable will be generated
                collChance = Random.Range(1, 101);//chance of collectable is number between 1 & 100
                if (collChance > 40)
                {
                    genColl = true;
                }
                else
                {
                    genColl = false;
                }

                //generate collectable if allowed
                if (genColl)
                {
                    //code to generate collectable at spawn point as child of currentBlock
                    newColl = Instantiate(collectable, new Vector3(relativeRight, 1, 5), Quaternion.identity);
                    newColl.transform.SetParent(currentBlock.transform);
                    collNum++;

                }

                //code for enemies
                if (allowPaladin)
                {
                    genPaladin = spawnPaladin(paladinChance);
                    //generate Paladin if allowed
                    if (genPaladin)
                    {
                        //as long as there are less than 4 already existing Lesser Sprites
                        if (paladinNum < 1)
                        {
                            //code to generate collectable at spawn point as child of currentBlock
                            newPaladin = Instantiate(paladin, new Vector3(relativeRight, 2, 5), Quaternion.identity);
                            newPaladin.transform.SetParent(currentBlock.transform);
                            paladinNum++;
                        }
                    }
                }

                //code for regular power ups
                genPowerUp = spawnPowerUp(powerUpChance);

                //generate power up if allowed
                if (genPowerUp)
                {
                    if (powerUpNum < 3)
                    {
                        powerUp = choosePowerUp(powerUpChoice);
                        //code to generate collectable at spawn point as child of currentBlock
                        newPU = Instantiate(powerUp, new Vector3(relativeRight, 3, 5), Quaternion.identity);
                        newPU.transform.SetParent(currentBlock.transform);
                        powerUpNum++;
                    }
                }

                //code for instant-kill power up
                genOnePunch = spawnInstantKill(opmChance);
                if (genOnePunch)
                {
                    if (opmNum < 2)
                    {
                        newOpm = Instantiate(opmPowerUp, new Vector3((relativeRight + 2f), 3, 5), Quaternion.identity);
                        newOpm.transform.SetParent(currentBlock.transform);
                        opmNum++;
                    }
                }
                //relativeRight += 5;
            }
            relativeRight += 5;//not sure if this is needed

            randStore = Random.Range(0, 4);
            if (randStore == 3)
            {
                Debug.Log("Height swap");
                if (height == 0)
                {
                    height = 1;
                    walkStart = relativeRight - 2.6f;
                    walkEnd = relativeRight + 2.5f;
                }
                else
                {
                    height = 0;
                    walkStart = relativeRight - 2.6f;
                    walkEnd = relativeRight + 2.5f;
                }
                relativeRight += 5;
            }

            
        }
        relativeRight += 5f;//move to next spot for new block
        count++;
        //Debug.Log(relativeRight + " out of " + sliceEnd);

        //currently 0, 1 will be stay on same height, 2 will be change height
        //test flag appears at end of slice to mark slice end
        currentBlock = Instantiate(testFlag, new Vector3(relativeRight, 5, 5), Quaternion.identity);
        currentBlock.transform.SetParent(parentBlock.transform);
        //Instantiate(testFlag, new Vector3(relativeRight-5, 5, 5), Quaternion.identity);

        //reset top level variables
        option = 0;
        allowPaladin = false;
        paladinNum = 0;
        paladinChance = 0;
        genPaladin = false;
        powerUpNum = 0;
        powerUpChance = 0;
        genPowerUp = false;
        powerUpChoice = 0;
        powerUp = null;
        genOnePunch = false;
        opmChance = 0;
        opmNum = 0;
        collNum = 0;
        collChance = 0;
        genColl = false;

        Debug.Log("level gen 1 done: " + count);
    }

    //functions for enemy spawning
    public bool spawnPaladin(int paladinChance)
    {
        //roll dice, Paladin has 50% spawn chance
        paladinChance = Random.Range(1, 101);
        if (paladinChance > 50) return true;
        else return false;
    }

    //functions for regular power up spawning
    public bool spawnPowerUp(int powerUpChance)
    {
        //roll dice. If powerUpChance is 67 to 100, giving a 33% chance, power up will be generated
        powerUpChance = Random.Range(1, 101);//chance of power ups is number between 1 & 100
        if (powerUpChance > 66) return true;
        else return false;
    }

    public GameObject choosePowerUp(int powerUpChoice)
    {
        powerUpChoice = Random.Range(1, 7);//powerUpChoice can be 1, 2, 3, 4, 5 or 6
        if (powerUpChoice == 1) return powerUp1;
        else if (powerUpChoice == 2) return powerUp2;
        else if (powerUpChoice == 3) return powerUp3;
        else if (powerUpChoice == 4) return powerUp4;
        else if (powerUpChoice == 5) return powerUp5;
        else if (powerUpChoice == 6) return powerUp6;
        else return null;
    }

    //function for instant-kill power up spawning
    public bool spawnInstantKill(int opmChance)
    {
        //roll dice, 50% chance of spawning an instant-kill power up on block creation
        opmChance = Random.Range(1, 101);
        if (opmChance > 50) return true;
        else return false;
    }
}
