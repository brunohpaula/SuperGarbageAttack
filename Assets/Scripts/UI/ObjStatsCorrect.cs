using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjStatsCorrect : MonoBehaviour
{

    [SerializeField]
    private Image img;
    [SerializeField]
    private Text txt;
    [SerializeField]
    private Text nameCat;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetImg(Image i)
    {
        img = i;

    }

    public void SetValue(string s)
    {
        txt.text = s;
    }

    public void SetNameCat(string s)
    {
        nameCat.text = s;
    }

}
