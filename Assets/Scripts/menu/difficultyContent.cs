using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class difficultyContent : MonoBehaviour {

    public GameObject DifficultyObject;
    public static Info selectedTwelveNoteChart;
    public static string diffcultyLevel = "";
    public static long diffcultyOffset = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
        if (songContent.selectedTwelveNoteChart != null)
        {
            selectedTwelveNoteChart = songContent.selectedTwelveNoteChart;
            songContent.selectedTwelveNoteChart = null;

            Transform[] ts1 = transform.GetComponentsInChildren<Transform>(true);
            foreach (Transform t in ts1)
            {
                if (t.gameObject.name == "difficultyContent")
                    continue;

                Destroy(t.gameObject);
            }

            foreach (DifficultyLevels info in selectedTwelveNoteChart.difficultyLevels)
            {
                GameObject Song = Instantiate(DifficultyObject);
                Song.transform.SetParent(gameObject.transform);
                Song.transform.localScale = new Vector3(1, 1, 1);
                Song.transform.localPosition = new Vector3(Song.transform.localPosition.x, Song.transform.localPosition.y, 0);
                Song.name = info.difficulty;
                Transform[] ts = Song.transform.GetComponentsInChildren<Transform>(true);
                foreach (Transform t in ts)
                {
                    if (t.gameObject.name == "difficulty")
                    {
                        t.gameObject.GetComponent<Text>().text = info.difficulty;
                    }
                }
            }
        }
    }

    private string RetrieveDifficulty(string difficulty)
    {
        string result;
        switch (difficulty)
        {
            case "EasySingle":
                result = "Easy";
                break;
            case "MediumSingle":
                result = "Medium";
                break;
            case "HardSingle":
                result = "Hard";
                break;
            case "ExpertSingle":
                result = "Expert";
                break;
            case "EasyDrums":
                result = "Easy Drums";
                break;
            case "MediumDrums":
                result = "Medium Drums";
                break;
            case "HardDrums":
                result = "Hard Drums";
                break;
            case "ExpertDrums":
                result = "Expert Drums";
                break;
            default:
                result = "Expert";
                break;
        }

        return result;
    }

    public static void setDifficulty()
    {
        var regSprite = Resources.Load<Sprite>("Graphics/Trans");

        Transform[] ts = GameObject.Find("difficultyContent").transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts)
        {
            if (t.gameObject.name == "selected")
            {
                Image realmSelect = t.gameObject.GetComponent<Image>();
                realmSelect.sprite = regSprite;
            }
        }
    }
}
