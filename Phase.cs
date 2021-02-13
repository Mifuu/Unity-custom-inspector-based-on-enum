using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Phase
{
    public string phaseName;
    public enum PhaseMovementType{Waypoint, SetInitial}
    public PhaseMovementType movementType;
    public int a;
    public int b;
    public int c;
}
