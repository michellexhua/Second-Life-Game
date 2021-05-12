using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class make1sectL2 : MonoBehaviour
{
    public GameObject block;
    public GameObject testFlag;
    public GameObject collectable;
    public GameObject powerUp1;
    public GameObject powerUp2;
    public GameObject powerUp3;
    public GameObject powerUp4;
    public GameObject powerUp5;
    public GameObject lesserSprite;
    public GameObject greaterSprite;

    //these are used for heirarchy gen
    public GameObject parentBlock;
    public GameObject currentBlock;
    public GameObject newColl;
    public GameObject newPU;
    public GameObject newLesserSp;
    public GameObject newGreaterSp;

    //used to measure positions relative to the current level slice
    float sliceStart;
    float sliceEnd;

    //used to determine area that AI things can walk across, pass these to AI handlers when instantiated
    float walkStart;
    float walkEnd;

    //code to determine enemy types allowed
    int option = 0;
    bool allowLesser = false;
    bool allowGreater = false;

    //code to generate Lesser Sprites
    int lessSpNum = 0;//num of Lesser Sprites allowed on current section
    int lessChance = 0;//chance of Lesser Sprite spawn on current block
    bool genLesser = false;

    //code to generate Greater Sprites
    int greatSpNum = 0;
    int greatChance = 0;//chance of Greater Sprite spawn on current block
    bool genGreater = false;

    //code to generate power ups
    int powerUpNum = 0;
    int powerUpChance = 0;
    bool genPowerUp = false;
    int powerUpChoice = 0;
    GameObject powerUp = null;

    //code to generate collectables
    //int collNum = 0;//num of collectables allowed on current section
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

        walkStart = currRight;
        parentBlock = Instantiate(block, new Vector3(-7, -10, 5), Quaternion.identity);
        parentBlock.name = "sliceParent" + sliceNum;

        //roll dice for enemies option in current section
        decideEnemies();

        while (relativeRight <= sliceEnd)
        {
            if (height == 0)
            {
                currentBlock = Instantiate(block, new Vector3(relativeRight, 0, 5), Quaternion.identity);
                currentBlock.transform.SetParent(parentBlock.transform);
                walkEnd += 5;

                //generate collectable spawns
                generateColls();

                //generate enemy spawns, Lesser Sprites and Greater Sprites
                generateLessers();
                generateGreaters();

                //generate power up spawns
                generatePowerUps();
            }
            else if (height == 1)
            {
                currentBlock = Instantiate(block, new Vector3(relativeRight, 1, 5), Quaternion.identity);
                currentBlock.transform.SetParent(parentBlock.transform);
                walkEnd += 5;

                //generate collectable spawns
                generateColls();

                //generate enemy spawns, Lesser Sprites and Greater Sprites
                generateLessers();
                generateGreaters();

                //generate power up spawns
                generatePowerUps();
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
        allowLesser = false;
        allowGreater = false;
        lessSpNum = 0;
        lessChance = 0;
        genLesser = false;
        greatSpNum = 0;
        greatChance = 0;
        genGreater = false;
        powerUpNum = 0;
        powerUpChance = 0;
        genPowerUp = false;
        powerUpChoice = 0;
        powerUp = null;
        //collNum = 0;
        collChance = 0;
        genColl = false;
        Debug.Log("level gen 1 done: " + count);
    }

    //functions for spawning
    public void generateColls()
    {
        //roll dice. If collChance is 41 to 100, giving a 60% chance, collectable will be generated
        collChance = Random.Range(1, 101);//chance of collectable is number between 1 & 100
        if (collChance > 40) genColl = true;
        else genColl = false;

        //generate collectable if allowed
        if (genColl)
        {
            //code to generate collectable at spawn point as child of currentBlock
            newColl = Instantiate(collectable, new Vector3(relativeRight, 1, 5), Quaternion.identity);
            newColl.transform.SetParent(currentBlock.transform);
            //collNum++;
        }

        //reset for next block
        collChance = 0;
        genColl = false;
    }

    //Enemies choice
    public void decideEnemies()
    {
        option = Random.Range(1, 5);//option is 1, 2, 3 or 4

        //no enemies generated in section if option = 1. else
        
        if (option == 2) allowLesser = true;//allow only Lesser Sprites
        else if (option == 3) allowGreater = true;//allow only Greater Sprites
        else if (option == 4)
        {
            //allow both Lesser and Greater Sprites
            allowLesser = true;
            allowGreater = true;
        }
    }

    //Lesser Sprites
    public void generateLessers()
    {
        if (allowLesser)
        {
            //roll dice. If lessChance is 26 to 100, giving a 75% chance, Lesser Sprite will be generated
            lessChance = Random.Range(1, 101);//chance of Lesser Sprites is number between 1 & 100
            if (lessChance > 25) genLesser = true;
            else genLesser = false;

            //generate Lesser Sprite
            if (genLesser)
            {
                //as long as there are less than 4 already existing Lesser Sprites
                if (lessSpNum < 4)
                {
                    //code to generate collectable at spawn point as child of currentBlock
                    //newLesserSp = Instantiate(lesserSprite, new Vector3(relativeRight, 1, 5), Quaternion.identity);
                    newLesserSp = Instantiate(lesserSprite) as GameObject;
                    newLesserSp.transform.parent = currentBlock.transform;
                    newLesserSp.transform.localPosition = new Vector3(-1, 1, 0);
                    newLesserSp.transform.rotation = Quaternion.identity;
                    //newLesserSp.transform.SetParent(currentBlock.transform);
                    lessSpNum++;
                }
            }
        }

        //reset for next block
        genLesser = false;
        lessChance = 0;
    }

    //Greater Sprites
    public void generateGreaters()
    {
        if (allowGreater)
        {
            //roll dice. If collChance is 51 to 100, giving a 50% chance, Greater Sprite will be generated
            greatChance = Random.Range(1, 101);//chance of collectibles is number between 1 & 100
            if (greatChance > 50) genGreater = true;
            else genGreater = false;

            //generate Greater Sprites
            if (genGreater)
            {
                //as long as there are less than 2 already existing Greater Sprites
                if (greatSpNum < 2)
                {
                    //code to generate collectable at spawn point as child of currentBlock
                    newGreaterSp = Instantiate(greaterSprite) as GameObject;
                    newGreaterSp.transform.parent = currentBlock.transform;
                    newGreaterSp.transform.localPosition = new Vector3(1, 1, 0);
                    newGreaterSp.transform.rotation = Quaternion.identity;
                    //newGreaterSp = Instantiate(greaterSprite, new Vector3(relativeRight, 1, 5), Quaternion.identity);
                    //newGreaterSp.transform.SetParent(currentBlock.transform);
                    greatSpNum++;
                }
            }
        }

        //reset for next block
        genGreater = false;
        greatChance = 0;
    }

    //Power Ups
    public void generatePowerUps()
    {
        //roll dice. If powerUpChance is 67 to 100, giving a 33% chance, power up will be generated
        powerUpChance = Random.Range(1, 101);//chance of power ups is number between 1 & 100
        if (powerUpChance > 66) genPowerUp = true;
        else genPowerUp = false;
        //generate power up
        if (genPowerUp)
        {
            powerUp = choosePowerUp(powerUpChoice);
            //code to generate collectable at spawn point as child of currentBlock
            newPU = Instantiate(lesserSprite) as GameObject;
            newPU.transform.parent = currentBlock.transform;
            newPU.transform.localPosition = new Vector3(0, 2, 0);
            newPU.transform.rotation = Quaternion.identity;
            //newPU = Instantiate(powerUp, new Vector3(relativeRight, 2, 5), Quaternion.identity);
            //newPU.transform.SetParent(currentBlock.transform);
            powerUpNum++;
        }

        //reset for next block
        powerUpChance = 0;
        genPowerUp = false;
        powerUpChoice = 0;
        powerUp = null;
    }

    public GameObject choosePowerUp(int powerUpChoice)
    {
        powerUpChoice = Random.Range(1, 7);//powerUpChoice can be 1, 2, 3, 4 or 5
        if (powerUpChoice == 1) return powerUp1;
        else if (powerUpChoice == 2) return powerUp2;
        else if (powerUpChoice == 3) return powerUp3;
        else if (powerUpChoice == 4) return powerUp4;
        else if (powerUpChoice == 5) return powerUp5;
        else return null;
    }
}
