using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private int totalScore;
    private float timer;

    private bool pause;

    [SerializeField]
    private Text UIscore;
    [SerializeField]
    private Text UItimer;
    [SerializeField]
    private float levelDuration;


    private CameraScroller camMovement;


    //register ingame data = errors and rights
    private int[,] errors;

    private int[] rights;

    private int[] missed;


    public GameObject statsScreen;

    public GameObject help;

    public GameObject about;



    public GameObject street, street_middle, pavement;
    public int height_y = 8;
    public int width_x = 70;
    private Transform streetholder;
    // Use this for initialization

    void Start()
    {
        pause = false;
        camMovement = GameObject.FindWithTag("MainCamera").GetComponent<CameraScroller>();
        errors = new int[4,4];
        rights = new int[4];
        missed = new int[4];
        StartLevel();
    }

	void StartLevel () 
    {
        StreetSetup();
    
        timer = levelDuration;
        totalScore = 0;
        //PrepareGarbageNotShown();
    }


	// Update is called once per frame
	void Update ()
    {
		if (!pause)
        {
            if (timer > 0f)
            {
                timer -= Time.deltaTime;
                if (timer < 0f)
                    timer = 0f;
            }
                
            
            UItimer.text = timer.ToString();
                        
            UIscore.text = "SCORE: "+ totalScore.ToString();

            //Means that level Ended
            if (timer <= 0)
            {
                EndLevel();
            }
        }
	}


    private void EndLevel()
    {
        camMovement.SetRolling(false);


        CountRestGarbage();

        statsScreen.SetActive(true);
        statsScreen.GetComponent<StatsScreen>().Prepare(errors, rights, missed, totalScore);

        Pause(true);
    }


    public void ShowAbout()
    {
        about.SetActive(true);
        Pause(true);
    }

    public void ShowHelp()
    {
        help.SetActive(true);
        Pause(true);
    }

    public void CloseAbout()
    {
        about.SetActive(false);
        Pause(false);
    }

    public void CloseHelp()
    {
        help.SetActive(false);
        Pause(false);
    }


    public void Pause(bool paused)
    {
        pause = paused;

        camMovement.SetRolling(!paused);

        if (paused)
        {
            GameObject.FindWithTag("player1").GetComponent<PlayerOneControls>().enabled = false;
            GameObject.FindWithTag("player2").GetComponent<PlayerTwoControls>().enabled = false;
        }
        else
        {
            GameObject.FindWithTag("player1").GetComponent<PlayerOneControls>().enabled = true;
            GameObject.FindWithTag("player2").GetComponent<PlayerTwoControls>().enabled = true;
        }
    }

    public void ManipulateScore(int val)
    {
        totalScore += val;
    }


    //deprecated since now every garbage has trigger collider
    //if the garbage is before the truck, transforms it collider in trigger so it can pass through the truck
    //the collider will be turned into collision again (!trigger) at GarbageScoring
    /*
    private void TransformColliderInTrigger(GameObject[] allGarbage)
    {
        foreach (GameObject garbage in allGarbage)
        {
            if (garbage.transform.position.x <= -6f)
            {
                garbage.GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }
    }


    //transforms colliders from objects before camera in triggers - they will become real colliders again after passing through truck, at garbagescoring.cs
    public void PrepareGarbageNotShown()
    {
        GameObject[] allGarbage = GameObject.FindGameObjectsWithTag("GarbageCartoon");

        TransformColliderInTrigger(allGarbage);

        allGarbage = GameObject.FindGameObjectsWithTag("GarbageGeneric");

        TransformColliderInTrigger(allGarbage);

        allGarbage = GameObject.FindGameObjectsWithTag("GarbagePlastic");

        TransformColliderInTrigger(allGarbage);

        

    }
    */


    //applies penalties for garbage left on the floor when level ends
    private void ApplyPenalty(int penalty, int numberOfGarbage, int type)
    {
        totalScore += penalty * numberOfGarbage;

        missed[type] += numberOfGarbage;
    }

    //applies penalties for garbage left on the floor when level ends
    private void CountRestGarbage()
    {
        int penalty = 0;

        GameObject[] allGarbage = GameObject.FindGameObjectsWithTag("GarbageCartoon");

        if (allGarbage.Length > 0)
        { 
            penalty = allGarbage[0].GetComponent<GarbageScoring>().scoreMissed;

            ApplyPenalty(penalty, allGarbage.Length, 0);

            foreach (GameObject g in allGarbage)
                Destroy(g);
        }
        allGarbage = GameObject.FindGameObjectsWithTag("GarbageGeneric");

          
        if (allGarbage.Length > 0)
        {
            if (penalty >= 0)
                penalty = allGarbage[0].GetComponent<GarbageScoring>().scoreMissed;

            ApplyPenalty(penalty, allGarbage.Length, 1);

            foreach (GameObject g in allGarbage)
                Destroy(g);
        }
        allGarbage = GameObject.FindGameObjectsWithTag("GarbagePlastic");

        if (allGarbage.Length > 0)
        {
            if (penalty >= 0)
                penalty = allGarbage[0].GetComponent<GarbageScoring>().scoreMissed;

            ApplyPenalty(penalty, allGarbage.Length, 1);

            foreach (GameObject g in allGarbage)
                Destroy(g);
        }
    }



    //translates the errors into a matrix
    public void RegisterError(string tagBin, string tagObj)
    {
        int binIdx = 0;
        int garbageIdx = 0;
        switch (tagBin)
        {
            case "GarbageCartoon":
                binIdx = 0;
                break;

            case "GarbageGeneric":
                binIdx = 1;
                break;

            case "GarbagePlastic":
                binIdx = 2;
                break;

            case "GarbageGlass":
                binIdx = 3;
                break;
        }
        switch (tagObj)
        {
            case "GarbageCartoon":
                garbageIdx = 0;
                break;

            case "GarbageGeneric":
                garbageIdx = 1;
                break;

            case "GarbagePlastic":
                garbageIdx = 2;
                break;

            case "GarbageGlass":
                garbageIdx = 3;
                break;
        }

        Debug.Log("added 1 to " + tagBin + "," + tagObj + "; this means" + binIdx + "," + garbageIdx);
        errors[binIdx,garbageIdx]++;
    }


    //translates the right moves into an array
    public void RegisterRight(string tagObj)
    {
        
        int garbageIdx = 0;
        
        switch (tagObj)
        {
            case "GarbageCartoon":
                garbageIdx = 0;
                break;

            case "GarbageGeneric":
                garbageIdx = 1;
                break;

            case "GarbagePlastic":
                garbageIdx = 2;
                break;

            case "GarbageGlass":
                garbageIdx = 3;
                break;
        }

        rights[garbageIdx]++;
        Debug.Log("GOT RIGHT " +tagObj +", "+ garbageIdx);
    }


    //translates the misses into an array
    public void RegisterMiss(string tagObj)
    {

        int garbageIdx = 0;

        switch (tagObj)
        {
            case "GarbageCartoon":
                garbageIdx = 0;
                break;

            case "GarbageGeneric":
                garbageIdx = 1;
                break;

            case "GarbagePlastic":
                garbageIdx = 2;
                break;

            case "GarbageGlass":
                garbageIdx = 3;
                break;
        }

        missed[garbageIdx]++;
        Debug.Log("MISSED " + tagObj + ", " + garbageIdx);
    }



    void StreetSetup()
    {
        //Setting room size
        height_y = 3;
        width_x = 70;
    

        for (int x = -71; x < width_x + 1; x++)
        {
            for (int y = -3; y < height_y + 1; y++)
            {
                GameObject toInstantiate;
                float mid = y / 2;
                if (y == mid)
                {
                    toInstantiate = street_middle;
                }
                else {
                    toInstantiate = street;
                }
                
                if (x == -71 || x == width_x || y == -4 || y == height_y)
                {
                    toInstantiate = pavement;
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(streetholder);
                
            }
        }
    }

}
