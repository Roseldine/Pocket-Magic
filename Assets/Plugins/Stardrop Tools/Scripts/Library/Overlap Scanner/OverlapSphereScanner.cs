
using UnityEngine;

namespace StardropTools
{
    public class OverlapSphereScanner : OverlapScanner
    {
        [SerializeField] protected float radius = 1;

        public float Radius { get => radius; set => radius = value; }

        public override void Scan() => SphereScan(Position);

        public void SphereScan(Vector3 position)
        {
            colliders = Physics.OverlapSphere(position + positionOffset, radius, collidableLayers);
            ColliderCheck();
        }

        public void SphereScan(Vector3 position, float radius)
        {
            colliders = Physics.OverlapSphere(position + positionOffset, radius, collidableLayers);
            ColliderCheck();
        }


        protected override void OnDrawGizmos()
        {
            if (scannerGizmos.drawGizmos == false)
                return;

            Gizmos.color = scannerGizmos.color;
            Gizmos.DrawWireSphere(Position + positionOffset, radius);
        }
    }
}


