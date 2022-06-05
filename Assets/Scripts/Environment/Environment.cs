using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StardropTools;

public class Environment : CoreComponent
{
    public Transform playerSpawnPoint;
    public Transform playerTargetPoint;
    [Space]
    public Transform opponentSpawnPoint;
    public Transform opponentTargetPoint;

    public Vector3 PlayerSpawnPos { get => playerSpawnPoint.position; }
    public Vector3 PlayerTargetPos { get => playerTargetPoint.position; }

    public Vector3 OpponentSpawnPos { get => opponentSpawnPoint.position; }
    public Vector3 OpponentTargetPos { get => opponentTargetPoint.position; }
}
