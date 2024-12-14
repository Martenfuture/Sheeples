using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepGroup
{
    public List<GameObject> sheeps = new List<GameObject>();
    public Vector3 middlePosition = Vector3.zero;
    public Vector3 targetDirection = Vector3.zero;
    public Vector3 lastPosition = Vector3.zero;
    public float targetDirectionStrength;
    public bool insideFinishArea;
}
