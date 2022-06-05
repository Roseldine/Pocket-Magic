using System.Collections;
using UnityEngine;
using StardropTools;

public class PlayerManager : StardropTools.Singletons.SingletonCoreManager<PlayerManager>
{
    [SerializeField] Player player;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Vector3 createPosition;

    public Player Player { get => player; }
    public Vector3 PlayerPos { get => player.Position; }
    public float PlayerPosX { get => player.PosX; }
    public float PlayerPosY { get => player.PosY; }
    public float PlayerPosZ { get => player.PosZ; }

    public override void Initialize()
    {
        base.Initialize();
        CreatePlayer();
    }

    public override void LateInitialize()
    {
        base.LateInitialize();
        player.Initialize();
    }

    void CreatePlayer()
    {
        if (player == null && playerPrefab != null)
        {
            player = Instantiate(playerPrefab).GetComponent<Player>();
            player.Position = createPosition;
        }

        else
            Debug.Log("Player exists or Prefab is null");
    }
}