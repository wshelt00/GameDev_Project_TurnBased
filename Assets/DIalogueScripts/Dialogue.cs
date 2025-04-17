using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Dialogue
{
    [TextArea(3, 10)]
    public string[] sentences;

    [TextArea(3, 10)]
    public string[] goodResponse;

    [TextArea(3, 10)]
    public string[] badResponse;

    public string name;
    

}
