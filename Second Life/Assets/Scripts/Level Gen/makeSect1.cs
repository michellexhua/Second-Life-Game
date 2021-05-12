using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class makeSect1 : MonoBehaviour
{
    public GameObject block;
    public GameObject testFlag;
    public GameObject collectable;
    public GameObject powerUp1;
    public GameObject powerUp2;
    public GameObject powerUp3;
    //for levels 2 and 3
    public GameObject powerUp4;
    public GameObject powerUp5;
    //for level 3
    public GameObject op;//holds instant-kill power up prefab
    public GameObject paladin;
    //for levels 1 and 2
    public GameObject lesserSprite;
    public GameObject greaterSprite;

    //these are used for heirarchy gen
    public GameObject parentBlock;
    public GameObject currentBlock;
    public GameObject newColl;
    public GameObject newPU;
    public GameObject newOP;
    public GameObject newPaladin;
    public GameObject newLesser;
    public GameObject newGreater;

    //used to measure positions relative to the current level slice
    float sliceStart;
    float sliceEnd;

    //used to determine area that AI things can walk across, pass these to AI handlers when instantiated
    float walkStart;
    float walkEnd;

    //code to generate enemies
    //Lesser Sprites
    int lessSpNum = 0;//num of Lesser Sprites allowed on current section
    int lessChance = 0;//chance of Lesser Sprite spawn on current block
    bool genLesser = false;
    //Greater Sprites
    int greatSpNum = 0;
    int greatChance = 0;//chance of Greater Sprite spawn on current block
    bool genGreater = false;
    //Paladins
    int paladinNum = 0;
    int paladinChance = 0;//chance of Lesser Sprite spawn on current block
    bool genPaladin = false;
    //bool isSpawned = false;

    //code to generate power ups
    int powerUpChance = 0;
    bool genPowerUp = false;
    int powerUpChoice = 0;
    GameObject powerUp = null;
    bool genOnePunch = false;//bool for instant-kill power up, which has separate rules
    int opChance = 0;

    //code to generate collectables
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
        Debug.Log("testing the code....");
    }

    //code for entire section
    public void gen(float currRight, int sliceNum, float sliceLength, int levelNum)
    {
        List<GameObject> lessers = new List<GameObject>();
        List<GameObject> greaters = new List<GameObject>();
        List<GameObject> paladins = new List<GameObject>();

        sliceStart = currRight;
        relativeRight = sliceStart;
        sliceEnd = sliceStart + sliceLength;//receives slice length

        //walkStart = currRight;
        parentBlock = Instantiate(block, new Vector3(-7, -10, 5), Quaternion.identity);
        parentBlock.name = "sliceParent" + sliceNum;

        while (relativeRight <= sliceEnd)
        {
            randStore = UnityEngine.Random.Range(0, 4);
            if (randStore == 3)
            {
                Debug.Log("Height swap");
                walkStart -= 2.5f;
                walkEnd += 2.5f;
                if (height == 0)
                {
                    foreach (GameObject enemy in lessers)
                    {
                        if (levelNum == 1)
                        {
                            Debug.Log(newLesser);
                            enemy.GetComponent<GolemController>().SetWalkStart(walkStart);
                            enemy.GetComponent<GolemController>().SetWalkEnd(walkEnd);
                        }
                        else
                        {
                            enemy.GetComponent<RedpinController>().SetWalkStart(walkStart);
                            enemy.GetComponent<RedpinController>().SetWalkEnd(walkEnd);
                        }

                    }
                    lessers.Clear();

                    foreach (GameObject enemy in greaters)
                    {
                        if (levelNum == 1)
                        {
                            enemy.GetComponent<QlikController>().SetWalkStart(walkStart);
                            enemy.GetComponent<QlikController>().SetWalkEnd(walkEnd);
                        }
                        else
                        {
                            enemy.GetComponent<DeathlingController>().SetWalkStart(walkStart);
                            enemy.GetComponent<DeathlingController>().SetWalkEnd(walkEnd);
                        }
                    }
                    greaters.Clear();

                    foreach (GameObject enemy in paladins)
                    {
                        enemy.GetComponent<PaladinController>().SetWalkStart(walkStart);
                        enemy.GetComponent<PaladinController>().SetWalkEnd(walkEnd);
                    }
                    paladins.Clear();

                    height = 1;
                    walkStart = relativeRight - 2.6f;
                    walkEnd = relativeRight + 2.5f;

                    currentBlock = Instantiate(block, new Vector3(relativeRight, 1, 5), Quaternion.identity);
                    currentBlock.transform.SetParent(parentBlock.transform);
                }
                else
                {
                    foreach (GameObject enemy in lessers)
                    {
                        if (levelNum == 1)
                        {
                            enemy.GetComponent<GolemController>().SetWalkStart(walkStart);
                            enemy.GetComponent<GolemController>().SetWalkEnd(walkEnd);
                        }
                        else
                        {
                            enemy.GetComponent<RedpinController>().SetWalkStart(walkStart);
                            enemy.GetComponent<RedpinController>().SetWalkEnd(walkEnd);
                        }
                    }
                    lessers.Clear();
                    
                    foreach (GameObject enemy in greaters)
                    {
                        if (levelNum == 1)
                        {
                            enemy.GetComponent<QlikController>().SetWalkStart(walkStart);
                            enemy.GetComponent<QlikController>().SetWalkEnd(walkEnd);
                        }
                        else
                        {
                            enemy.GetComponent<DeathlingController>().SetWalkStart(walkStart);
                            enemy.GetComponent<DeathlingController>().SetWalkEnd(walkEnd);
                        }
                    }
                    greaters.Clear();

                    foreach (GameObject enemy in paladins)
                    {
                        enemy.GetComponent<PaladinController>().SetWalkStart(walkStart);
                        enemy.GetComponent<PaladinController>().SetWalkEnd(walkEnd);
                    }
                    paladins.Clear();

                    height = 0;
                    walkStart = relativeRight - 2.6f;
                    walkEnd = relativeRight + 2.5f;

                    currentBlock = Instantiate(block, new Vector3(relativeRight, 0, 5), Quaternion.identity);
                    currentBlock.transform.SetParent(parentBlock.transform);
                }
                relativeRight += 5;
            }

            if (height == 0)
            {
                currentBlock = Instantiate(block, new Vector3(relativeRight, 0, 5), Quaternion.identity);
                currentBlock.transform.SetParent(parentBlock.transform);
                walkEnd += 5;

                //code for collectables
                generateColls();

                //code for enemies
                if (levelNum < 3)
                {
                    generateLessers(lessers);
                    generateGreaters(greaters);
                }
                else
                {
                    generatePaladins(paladins);

                    //code for instant-kill power up
                    generateOP();
                }

                //code for regular power ups
                generatePowerUps(levelNum);
                relativeRight += 5;
            }
            else if (height == 1)
            {
                currentBlock = Instantiate(block, new Vector3(relativeRight, 1, 5), Quaternion.identity);
                currentBlock.transform.SetParent(parentBlock.transform);
                walkEnd += 5;

                //code for collectables
                generateColls();

                //code for enemies
                if (levelNum < 3)
                {
                    generateLessers(lessers);
                    generateGreaters(greaters);
                }
                else
                {
                    generatePaladins(paladins);

                    //code for instant-kill power up
                    generateOP();
                }

                //code for regular power ups
                generatePowerUps(levelNum);
                relativeRight += 5;
            }
            //relativeRight += 5;

            //relativeRight += 5;//not sure if this is needed
            count++;
            //Debug.Log(relativeRight + " out of " + sliceEnd);
        }
        //currently 0, 1 will be stay on same height, 2 will be change height
        //test flag appears at end of slice to mark slice end
        currentBlock = Instantiate(testFlag, new Vector3(relativeRight, 5, 5), Quaternion.identity);
        currentBlock.transform.SetParent(parentBlock.transform);
        //Instantiate(testFlag, new Vector3(relativeRight-5, 5, 5), Quaternion.identity);
        foreach (GameObject enemy in lessers)
        {
            if (levelNum == 1)
            {
                Debug.Log(newLesser);
                enemy.GetComponent<GolemController>().SetWalkStart(walkStart);
                enemy.GetComponent<GolemController>().SetWalkEnd(walkEnd);
            }
            else
            {
                enemy.GetComponent<RedpinController>().SetWalkStart(walkStart);
                enemy.GetComponent<RedpinController>().SetWalkEnd(walkEnd);
            }

        }
        lessers.Clear();

        foreach (GameObject enemy in greaters)
        {
            if (levelNum == 1)
            {
                enemy.GetComponent<QlikController>().SetWalkStart(walkStart);
                enemy.GetComponent<QlikController>().SetWalkEnd(walkEnd);
            }
            else
            {
                enemy.GetComponent<DeathlingController>().SetWalkStart(walkStart);
                enemy.GetComponent<DeathlingController>().SetWalkEnd(walkEnd);
            }
        }
        greaters.Clear();

        foreach (GameObject enemy in paladins)
        {
            enemy.GetComponent<PaladinController>().SetWalkStart(walkStart);
            enemy.GetComponent<PaladinController>().SetWalkEnd(walkEnd);
        }
        paladins.Clear();
        //reset top level variables
        lessSpNum = 0;
        lessChance = 0;
        genLesser = false;
        greatSpNum = 0;
        greatChance = 0;
        genGreater = false;
        paladinNum = 0;
        paladinChance = 0;
        genPaladin = false;
        powerUpChance = 0;
        genPowerUp = false;
        powerUpChoice = 0;
        powerUp = null;
        genOnePunch = false;
        opChance = 0;

        Debug.Log("Section type 1 gen done: " + count);
    }

    //functions for spawning
    //code for collectables
    public void generateColls()
    {
        //roll dice, coll has 60% spawn chance
        collChance = UnityEngine.Random.Range(1, 101);
        if (collChance > 40) genColl = true;
        else genColl = false;
        //generate collectable
        if (genColl)
        {
            //code to generate collectable at spawn point as child of currentBlock
            newColl = Instantiate(collectable, new Vector3(relativeRight, 1, 2.5f), Quaternion.identity);
            newColl.transform.SetParent(currentBlock.transform);
            //collNum++;
        }
        //reset for next block
        collChance = 0;
        genColl = false;
    }

    //code for enemies
    //Lesser Sprites
    public void generateLessers(List<GameObject> lessers)
    {

        //roll dice, Lesser Sprite has 50% spawn chance
        lessChance = UnityEngine.Random.Range(1, 101);//chance of Lesser Sprites is number between 1 & 100
        if (lessChance > 50) genLesser = true;
        else genLesser = false;
        //generate Lesser Sprite
        if (genLesser)
        {
            //as long as there are less than 4 already existing Lesser Sprites
            if (lessSpNum < 4)
            {
                //code to generate collectable at spawn point as child of currentBlock
                newLesser = Instantiate(lesserSprite, new Vector3(relativeRight, 2, 5), Quaternion.identity);
                newLesser.transform.SetParent(currentBlock.transform);
                lessers.Add(newLesser);
                lessSpNum++;
            }
        }
        //reset for next block
        genLesser = false;
        lessChance = 0;

    }
    //Greater Sprites
    public void generateGreaters(List<GameObject> greaters)
    {
        //roll dice, Lesser Sprite has 25% spawn chance
        greatChance = UnityEngine.Random.Range(1, 101);//chance of collectibles is number between 1 & 100
        if (greatChance > 75) genGreater = true;
        else genGreater = false;
        //generate Greater Sprites
        if (genGreater)
        {
            //as long as there are less than 2 already existing Greater Sprites
            if (greatSpNum < 2)
            {
                //code to generate collectable at spawn point as child of currentBlock
                newGreater = Instantiate(greaterSprite, new Vector3(relativeRight, 2, 5), Quaternion.identity);
                newGreater.transform.SetParent(currentBlock.transform);
                greaters.Add(newGreater);
                greatSpNum++;
            }
        }

        //reset for next block
        genGreater = false;
        greatChance = 0;
    }
    //Paladins
    public void generatePaladins(List<GameObject> paladins)
    {
        //roll dice, Paladin has 50% spawn chance
        paladinChance = UnityEngine.Random.Range(1, 101);
        if (paladinChance > 50) genPaladin = true;
        else genPaladin = false;
        //generate Paladin
        if (genPaladin)
        {
            //as long as there are less than 4 already existing Lesser Sprites
            if (paladinNum < 1)
            {
                //code to generate Paladin at spawn point as child of currentBlock
                newPaladin = Instantiate(paladin, new Vector3(relativeRight, 2, 5), Quaternion.identity);
                newPaladin.transform.SetParent(currentBlock.transform);
                paladins.Add(newPaladin);
                paladinNum++;
            }
        }
    }

    //code for power ups
    //Regular Power Ups
    public void generatePowerUps(int levelNum)
    {
        //roll dice. If powerUpChance is 67 to 100, giving a 33% chance, power up will be generated
        powerUpChance = UnityEngine.Random.Range(1, 101);//chance of power ups is number between 1 & 100
        if (powerUpChance > 66) genPowerUp = true;
        else genPowerUp = false;
        //generate power up
        if (genPowerUp)
        {
            powerUp = choosePowerUp(levelNum);
            //code to generate collectable at spawn point as child of currentBlock
            newPU = Instantiate(powerUp, new Vector3(relativeRight + 2, 1, 2.5f), Quaternion.identity);
            newPU.transform.SetParent(currentBlock.transform);
            /*newPU = Instantiate(lesserSprite) as GameObject;
            newPU.transform.parent = currentBlock.transform;
            newPU.transform.localPosition = new Vector3(-1, 1, 0);
            newPU.transform.localRotation = Quaternion.identity;*/
        }
        //reset for next block
        powerUpChance = 0;
        genPowerUp = false;
        powerUpChoice = 0;
        powerUp = null;
    }
    public GameObject choosePowerUp(int levelNum)
    {
        if (levelNum == 1) powerUpChoice = UnityEngine.Random.Range(1, 4);//1 to 3 for level 1
        else powerUpChoice = UnityEngine.Random.Range(1, 6);//1 to 5 for levels 2 & 3

        if (powerUpChoice == 1) return powerUp1;
        else if (powerUpChoice == 2) return powerUp2;
        else if (powerUpChoice == 3) return powerUp3;
        else if (powerUpChoice == 4) return powerUp4;
        else if (powerUpChoice == 5) return powerUp5;
        else return null;
    }
    //Instant-kill Power Ups
    public void generateOP()
    {
        //roll dice, 50% chance of spawning an instant-kill power up on block creation
        opChance = UnityEngine.Random.Range(1, 101);
        if (opChance > 50) genOnePunch = true;
        else genOnePunch = false;
        if (genOnePunch)
        {
            newOP = Instantiate(op, new Vector3(relativeRight, 1, 2.5f), Quaternion.identity);
            newOP.transform.SetParent(currentBlock.transform);
            /*) as GameObject;
            newOpm.transform.parent = currentBlock.transform;
            newOpm.transform.localPosition = new Vector3(1, 1, 0);
            newOpm.transform.localRotation = Quaternion.identity;*/
        }
    }
}
