using System.Collections.Generic;
using UnityEngine;
using StardropTools.Pool;

public class SpellManager : StardropTools.Singletons.SingletonCoreManager<SpellManager>
{
    [SerializeField] SpellDatabaseSO spellDB;
    [SerializeField] PoolCluster[] spellClusters;
    [SerializeField] Transform parentClusters;
    [SerializeField] bool createDatabasePools;
    [Space]
    [SerializeField] Transform parentSpells;
    [SerializeField] List<Spell> spellList;

    public override void Initialize()
    {
        base.Initialize();

        for (int i = 0; i < spellClusters.Length; i++)
            spellClusters[i].Initialize(i);

        SubscribeToEvents();
    }

    public override void SubscribeToEvents()
    {
        base.SubscribeToEvents();

        GameManager.Instance.OnPlayUpdate.AddListener(UpdateSpells);
    }

    void UpdateSpells()
    {
        for (int i = 0; i < spellList.Count; i++)
            spellList[i].UpdateSpell();
    }

    public SpellIdentifier ValidateSpell(int[] spellPoints)
        => spellDB.ValidateSpell(spellPoints);

    #region Pool

    public Spell SpawnSpell(int clusterIndex, int poolIndex, Vector3 position, Quaternion rotation, Transform parent)
    {
        var spell = spellClusters[clusterIndex].SpawnFromPool<Spell>(poolIndex, position, rotation, parent);

        spell.Initialize();

        if (spellList.Contains(spell) == false)
            spellList.Add(spell);

        return spell;
    }

    public Spell SpawnSpell(SpellIdentifier identifier, Vector3 position, Quaternion rotation)
        => SpawnSpell(identifier.PoolClusterIdentifier, identifier.SpellIndex, position, rotation, parentSpells);

    public void DespawnSpell(Spell spell)
    {
        spellList.Remove(spell);
        spell.Despawn(true);
    }

    public void DespawnAllSpells()
    {
        for (int i = 0; i < spellClusters.Length; i++)
            spellClusters[i].DespawnAllPools();

        spellList = new List<Spell>();
    }



    void CreateDatabasePools()
    {
        List<PoolCluster> clusterList = new List<PoolCluster>();

        // loop through database
        for (int i = 0; i < spellDB.SpellDBCount; i++)
        {
            var db = spellDB.GetDatabaseByIndex(i);

            if (db.SpellCount == 0)
                continue;

            // create PoolCluster component
            PoolCluster pool = Utilities.CreateEmpty("Cluster - " + db.SpellTier.ToString(), Vector3.zero, parentClusters).gameObject.AddComponent<PoolCluster>();

            for (int j = 0; j < db.SpellCount; j++)
            {
                var spell = db.GetSpellByIndex(j);

                // add pool to cluster if spell has a prefab
                if (spell.SpellPrefab != null)
                    pool.AddObjectPool(spell.SpellPrefab, 5);
            }

            // add cluster to list
            clusterList.Add(pool);
        }

        // refresh cluster array
        spellClusters = clusterList.ToArray();
    }
    #endregion



    protected override void OnValidate()
    {
        base.OnValidate();

        if (createDatabasePools)
        {
            CreateDatabasePools();
            createDatabasePools = false;
        }
    }
}