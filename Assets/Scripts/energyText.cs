using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class energyText : MonoBehaviour
{
     Text energy;
    // Start is called before the first frame update
    void Start()
    {
        energy = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        energy.text = "Energy: " + Player.energy;
    }
}
