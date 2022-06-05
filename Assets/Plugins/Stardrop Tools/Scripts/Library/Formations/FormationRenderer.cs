
using UnityEngine;

namespace StardropTools.Formations
{
    [RequireComponent(typeof(Formation))]
    public class FormationRenderer : MonoBehaviour
    {
        public enum RendererShape { sphere, cube }
        public enum TransformPosition { none, worldPosition, localPosition }

        [SerializeField] RendererShape shape;
        [SerializeField] TransformPosition position;
        [SerializeField] Color color = Color.cyan;
        [SerializeField] Vector3 size = new Vector3(.25f, 1, .25f);
        [SerializeField] float radius = .25f;

        Transform self;
        Formation formation;

        private void OnDrawGizmos()
        {
            if (formation == null)
                return;

            if (formation.Points != null && formation.Points.Length > 0)
            {
                Gizmos.color = color;

                for (int i = 0; i < formation.Points.Length; i++)
                {
                    var pos = formation.Points[i];

                    if (position == TransformPosition.worldPosition)
                        pos += self.position;
                    if (position == TransformPosition.localPosition)
                        pos += self.localPosition;

                    if (shape == RendererShape.sphere)
                        Gizmos.DrawSphere(pos, radius);
                    if (shape == RendererShape.cube)
                        Gizmos.DrawCube(pos + new Vector3(0, size.y * 0.5f, 0), size);
                }
            }
        }

        private void OnValidate()
        {
            if (self == null)
                self = transform;
            if (formation == null)
                formation = GetComponent<Formation>();
        }
    }
}

