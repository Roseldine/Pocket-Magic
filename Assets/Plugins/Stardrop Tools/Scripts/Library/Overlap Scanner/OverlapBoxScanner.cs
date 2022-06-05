
using UnityEngine;

namespace StardropTools
{
    public class OverlapBoxScanner : OverlapScanner
    {
        [SerializeField] protected Vector3 boxScale = Vector3.one;

        public Vector3 BoxScale { get => boxScale; set => boxScale = value; }

        public override void Scan()
            => BoxScan(Position, Rotation);

        public void Scan(Vector3 position, Vector3 scale, Quaternion rotation)
            => BoxScan(position, scale, rotation);

        public void BoxScan(Vector3 position, Quaternion rotation)
        {
            colliders = Physics.OverlapBox(position + positionOffset, boxScale / 2, rotation, collidableLayers);
            ColliderCheck();
        }

        public void BoxScan(Vector3 position, Vector3 scale, Quaternion rotation)
        {
            colliders = Physics.OverlapBox(position + positionOffset, scale / 2, rotation, collidableLayers);
            ColliderCheck();
        }

#if UNITY_EDITOR
        protected override void OnDrawGizmos()
        {
            if (scannerGizmos.drawGizmos == false)
                return;

            Gizmos.color = scannerGizmos.color;
            Utilities.DrawCube(Position + positionOffset, boxScale, Rotation);
        }
#endif
    }

}