using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class difficultyButton : MonoBehaviour {
    

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(GoToTarget);
    }

    void GoToTarget()
    {
        setDifficulty();

        foreach (DifficultyLevels info in difficultyContent.selectedTwelveNoteChart.difficultyLevels)
        {
            if (info.difficulty == gameObject.name)
            {
                difficultyContent.diffcultyLevel = info.jsonPath;
                difficultyContent.diffcultyOffset = info.offset;
            }
        }
    }

    public void setDifficulty()
    {
        difficultyContent.setDifficulty();
        var tarSprite = Resources.Load<Sprite>("Graphics/CharacterObjectTargeted");

        Transform[] ts = gameObject.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts)
        {
            if (t.gameObject.name == "selected")
            {
                Image realmSelect = t.gameObject.GetComponent<Image>();
                realmSelect.sprite = tarSprite;
                GameObject.Find("start").GetComponent<Button>().interactable = true;
            }
        }

    }
}
