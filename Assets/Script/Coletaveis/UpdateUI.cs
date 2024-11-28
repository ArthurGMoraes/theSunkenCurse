using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateUI : MonoBehaviour
{
    [SerializeField] private GameObject Object;
    private TextMeshProUGUI text;
    private string objID;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        objID = Object.GetComponent<Object>().ID;
    }

    private void LateUpdate()
    {
        text.text = PlayerPrefs.GetInt(objID).ToString();
    }
}
