using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewChar", menuName = "Character")]

public class Characters : ScriptableObject
{
    public GameObject characterSel;

    public Sprite image;

    public string charName;

}
