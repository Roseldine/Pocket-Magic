using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Combat
{
    public class Projectile : Attack
    {
        public enum EProjectileType { traveler, hitScan }

        [SerializeField] OverlapBoxScanner hitBox;
        [Space]
        [SerializeField] EProjectileType projectileType;
        [SerializeField] float speed = 15;
        [SerializeField] float maxSpeed = 20;
        [SerializeField] float maxDistance = 128;
        [Space]
        [SerializeField] Vector3 originPos;
        [SerializeField] Vector3 targetPos;

        public float DistanceTraveled { get; private set; }

        public CoreEvent<Collider> OnHit { get => hitBox.OnEnter; }

        public override void Initialize()
        {
            base.Initialize();

        }

        public void UpdateProjectile()
        {
            hitBox.Scan();
            UpdateDistance();
        }

        void UpdateDistance()
        {
            DistanceTraveled = DistanceFrom(SpawnedPosition);

            if (DistanceTraveled > maxDistance)
                Despawn(true);
        }
    }
}