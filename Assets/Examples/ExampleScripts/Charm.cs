using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Charm", menuName = "Charm")]
public class Charm : ScriptableObject {

    public enum CharmType
    {
        Potion = 1,
        Helmet = 2,
        Armour = 4,
        Ring = 8,
        Amulet = 16,
        Boots = 32,
        Gloves = 64,
        All = 127 // combination of all types
    }

    public CharmType charmType = CharmType.Potion;
    public Color color;
    public float level;
    public Sprite icon;

    public string GetDescription()
    {
        string desc = name + " (" + charmType.ToString() + ")\n";
        if (level > 0)
            desc += "Requires Level " + level;
        return desc;

    }
}
