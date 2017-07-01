using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsScreen : MonoBehaviour {

    public GameObject objRight;

    [SerializeField]
    private Text rightTable;
    [SerializeField]
    private Text missedTable;

    [SerializeField]
    private Text errorsTable;

    [SerializeField]
    private Text finalScore;

    

    // Use this for initialization
    void Start ()
    {
        //rightTable.text = "";
        //missedTable.text = "";
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    


    private string GetName(int idx)
    {
        switch (idx)
        {
            case 0:
                return "PAPER";


            case 1:
                return "GENERIC";


            case 2:
                return "PLASTIC";


            case 3:
                return "GLASS";

        }
        return null;
    }

    public void Prepare(int [,] errors, int[] rights, int[] missed, int score)
    {
        Debug.Log("preparing");

        rightTable.text = "";
        missedTable.text = "";
        errorsTable.text = "";
        


        List<int> existingIdx = new List<int>();

        //display Rights
        for (int i = 0; i < rights.Length; i++)
        {
            Debug.Log("rights, i = " + i + ", val is " + rights[i]);
            //this means that category existed ingame
           if ((rights[i] > 0) || (missed[i] > 0))
            {
                existingIdx.Add(i);
                rightTable.text = rightTable.text + GetName(i) + ": " + rights[i].ToString() + "; ";
                /*
                GameObject objRightClone = Instantiate(objRight);
                objRight.GetComponent<ObjStatsCorrect>().SetValue(rights[i].ToString());
                objRight.GetComponent<ObjStatsCorrect>().SetNameCat(GetName(i));
                objRightClone.transform.parent = rightTable.transform;
                */
            }

        }

        //display Missed
        for (int i = 0; i < missed.Length; i++)
        {
            Debug.Log("missed, i = " + i + ", val is " + missed[i]);
            //this means that category existed ingame
            if (existingIdx.IndexOf(i) >= 0)
            {
                missedTable.text = missedTable.text + GetName(i) + ": " + missed[i].ToString() + "; ";
                /*
                GameObject objRightClone = Instantiate(objRight);
                objRight.GetComponent<ObjStatsCorrect>().SetValue(missed[i].ToString());
                objRight.GetComponent<ObjStatsCorrect>().SetNameCat(GetName(i));
                objRightClone.transform.parent = missedTable.transform;
                */
            }

        }

        
        int[] existingIndexes = existingIdx.ToArray();
        
        for (int i = 0; i < existingIndexes.Length; i++)
        {
            for (int x = 0; x < existingIndexes.Length; x++)
            {
                if (x == i)
                    continue;
                else
                {
                    if (errors[i,x] > 0)
                    {
                        errorsTable.text = errorsTable.text+ GetName(x) + " in " + GetName(i) + " BIN: " + errors[i, x]+"\n";
                    }
                }
            }
        }
        

        finalScore.text = finalScore.text + " " + score.ToString();

    }





}
