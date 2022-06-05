
using UnityEngine;

namespace StardropTools.Combat
{
    public class ProjectileTrajectory : CoreComponent
    {
        public enum ETrajectoryType { linear, hitScan, gravity, parabolic }

        [Header("Components")]
        [SerializeField] Projectile projectile;
        [SerializeField] LayerMask collidableLayers;
        [SerializeField] float collisionCheckRadius = .1f;
        [SerializeField] bool initializeRaycastHit;

        [Header("Values")]
        [SerializeField] ETrajectoryType trajectoryType;
        [SerializeField] float speed = 10;
        [SerializeField] float drag = 0;
        [SerializeField] float gravity = 9.18f;
        [Space]
        [SerializeField] Vector3 predictedPosition;
        [SerializeField] System.Collections.Generic.List<Vector3> trajectoryPoints;

        [Header("Non Linear Trajectory")]
        [SerializeField] int timeResolution = 16;
        [Range(0, 1)] [SerializeField] float timeBetweenPoints = .1f;

        Vector3 startPosition;
        Vector3 startVelocity;
        Vector3 direction;
        
        RaycastHit hit = new RaycastHit();

        public override void Initialize()
        {
            base.Initialize();
            hit = new RaycastHit();
        }

        public void CreateLinearTrajectory(float speed, float drag = 0)
        {
            trajectoryType = ETrajectoryType.linear;
            this.speed = speed;
            this.drag = drag;
            RaycastHitScan();
        }

        public void CreateGravityTrajectory(Vector3 startPos, float speed, float gravity, float drag = 0)
        {
            trajectoryType = ETrajectoryType.gravity;

            startPosition = startPos;
            RaycastHitScan();
            this.speed = speed;
            this.gravity = gravity;
            this.drag = drag;

            // set origin transform rotation
            
            startVelocity = projectile.Transform.forward * this.speed;

            // create new trajectory list
            trajectoryPoints = new System.Collections.Generic.List<Vector3>();

            for (float t = 0; t < timeResolution; t += timeBetweenPoints)
            {
                Vector3 newPoint = startPosition + t * startVelocity;
                newPoint.y = startPosition.y + startVelocity.y * t + -gravity / 2 * t * t;
                trajectoryPoints.Add(newPoint);

                if (Physics.OverlapSphere(newPoint, collisionCheckRadius, collidableLayers).Length > 0)
                    break;
            }
        }

        void RaycastHitScan()
        {
            if (Physics.Raycast(projectile.Position, projectile.Transform.forward, out hit, 100, collidableLayers))
                predictedPosition = hit.point;

            direction = (predictedPosition - startPosition).normalized;
        }

        public void UpdateTrajectory()
        {
            
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(startPosition, predictedPosition);

            Gizmos.color = Color.green;
        }
    }
}