using UnityEngine;
using UnityEngine.UI;
using System;

class SampleSquare : Square
{
    public Sprite TileImage;
    public String TerrainType;

    public override Vector3 GetCellDimensions()
    {
        return GetComponent<Renderer>().bounds.size;
    }

    public override void MarkAsHighlighted()
    {
        GetComponent<Renderer>().material.color = new Color(0.75f, 0.75f, 0.75f); ;
    }

    public override void MarkAsPath()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }

    public override void MarkAsReachable()
    {
        GetComponent<Renderer>().material.color = Color.magenta;
    }

    public override void UnMark()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}

