using UnityEngine;
using System.Collections;

public class ScoreBoard02 : MonoBehaviour {

	string[] scoreData;
	
	void Start () {
		
		//Get old scores from PlayerPrefs
		//We'll assume it's a top-5 board for this example
		scoreData = new string[5];
		
		for(int i = scoreData.Length; i > 0; --i)
		{
			//ie get string named "Highscore5", if blank set to/return "500,WUP"
			scoreData[i-1] = PlayerPrefs.GetString("Highscore"+i, (i*100).ToString() + ',' + "WUP");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static int ParseScore(string str) {
		
		//return int parsed out of string index 0 returned by splitting the string at commas
		return int.Parse(str.Split(new char[]{','})[0]);
	}

	//Sort this array, simple bubble sort with premature exit
	void SortScores() {
		
		bool swapped;
		for(int i = scoreData.Length - 1; i > 0; --i)
		{
			swapped = false;
			for(int j = 0; j < i; ++j)
			{
				if(ParseScore(scoreData[j]) > ParseScore(scoreData[j+1]))
				{
//					Swap<string>(ref scoreData[j], ref scoreData[j+1]); // ERROR CAUSING LINE, commented it out temp
					swapped = true;
				}
			}
			if(!swapped)
			{
				break;
			}
		}
	}
}
