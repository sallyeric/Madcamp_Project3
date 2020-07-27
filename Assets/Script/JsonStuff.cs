using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stuff
{
    public int stage;
    public string name;
    //public Sprite icon;
}

public class JsonStuff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Stuff stuff = new Stuff();
        stuff.stage = 1;
        stuff.name = "구두";

      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
