using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private GameObject restartButton;

    [Header("Knife Count Display")]
    [SerializeField]
    private GameObject panelKnifes;
    [SerializeField]
    private GameObject iconKnife;
    [SerializeField]
    private Color usedKnifeIconColor;

    public void DisplayRestartButton(){
        restartButton.SetActive(true);
    }

    public void SetInitialDisplayKnifeCount(int count){
        for (int i = 0; i < count; i++)
        {
            Instantiate(iconKnife, panelKnifes.transform);
        }
    }

    private int KnifeIconIndexToChange = 0;

    public void DecrementDisplayKnifeCount(){
        panelKnifes.transform.GetChild(KnifeIconIndexToChange).GetComponent<Image>().color = usedKnifeIconColor;
        KnifeIconIndexToChange++;
    }

}
