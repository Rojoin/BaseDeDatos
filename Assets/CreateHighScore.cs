using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHighScore : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI text;
    [SerializeField] ScoreDTB dtb;
    [SerializeField] private List<GameObject> scores;
    void Start()
    {
        scores = new List<GameObject>();
        foreach (var VARIABLE in dtb.obj)
        {
            var aux = Instantiate(text, this.transform);
            aux.text = VARIABLE.name + ":" + VARIABLE.score;
            scores.Add(aux.gameObject);
        }
    }
    public void Delete()
    {
        foreach (var VARIABLE in scores)
        {
            Destroy(VARIABLE.gameObject);
        }
    }

    
}
