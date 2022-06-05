
using UnityEngine;

namespace StardropTools
{
    public abstract class OverlapScanner : CoreObject
    {
        [Header("Scanner")]
        [SerializeField] protected ScannerGizmos scannerGizmos;
        [SerializeField] protected LayerMask collidableLayers;
        [SerializeField] protected Vector3 positionOffset;
        [SerializeField] protected bool isOneTime;
        [SerializeField] protected bool debug;
        [Space]
        [SerializeField] protected System.Collections.Generic.List<Collider> listColliders;
        protected Collider[] colliders;

        public int ColliderCount { get => colliders.Exists() ? colliders.Length : 0; }
        public Collider[] Colliders { get => colliders; }
        public Vector3 PositionOffset { get => positionOffset; set => positionOffset = value; }


        #region Events
        public readonly CoreEvent OnEnterDry = new CoreEvent();
        public readonly CoreEvent OnStayDry = new CoreEvent();
        public readonly CoreEvent OnExitDry = new CoreEvent();

        public readonly CoreEvent OnDetected = new CoreEvent();

        public readonly CoreEvent<Collider> OnEnter = new CoreEvent<Collider>();
        public readonly CoreEvent<Collider> OnStay = new CoreEvent<Collider>();
        public readonly CoreEvent<Collider> OnExit = new CoreEvent<Collider>();
        #endregion // events


        public override void Initialize()
        {
            base.Initialize();
            SetCanUpdate(true);
            colliders = new Collider[0];
        }

        public void SetLayerMask(LayerMask collidableLayers)
            => this.collidableLayers = collidableLayers;

        public virtual void Scan()
            => ColliderCheck();

        public virtual Collider[] Scan(LayerMask collidableLayers)
            => colliders;


        // Add & Remove detected colliders in order;
        protected void ColliderCheck()
        {
            if (listColliders == null)
                listColliders = new System.Collections.Generic.List<Collider>();

            if (colliders != null)
            {
                // check if list and array is the same
                // if not, either add or remove from list & invoke events
                if (colliders.Length != listColliders.Count)
                {
                    if (colliders.Length > 0)
                    {
                        // check colliders to add to queue
                        for (int i = 0; i < colliders.Length; i++)
                        {
                            Collider col = colliders[i];

                            // add to queue
                            if (listColliders.Contains(col) == false)
                            {
                                listColliders.Add(GetComponent<Collider>());

                                OnEnter?.Invoke(GetComponent<Collider>());
                                OnEnterDry?.Invoke();

                                if (debug)
                                    Debug.Log("Collider Entered");
                            }

                            // update colliders that are still inside
                            else
                            {
                                OnStay?.Invoke(col);
                                OnStayDry?.Invoke();
                            }
                        }
                    }

                    else
                    {
                        // check list to remove Out Of Bounds colliders
                        for (int i = 0; i < listColliders.Count; i++)
                        {
                            // check if collider in list exists in collider array
                            Collider col = listColliders[i];

                            listColliders.Remove(col);

                            OnExit?.Invoke(col);
                            OnExitDry?.Invoke();

                            if (debug)
                                Debug.Log("Collider Exited");
                        }

                        listColliders.Clear();
                    }

                    OnDetected?.Invoke();
                }
            }
        }

        protected void FixedCollisionCheck()
        {
            if (colliders.Exists())
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    OnStay?.Invoke(colliders[i]);
                }

                OnStayDry.Invoke();
            }
        }

        // To do
        public void SortByDistance(Vector3 referencePosition)
        {

        }


        public virtual T GetComponentInScan<T>(T component)
        {
            if (colliders.Exists())
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    T obj = colliders[i].GetComponent<T>();
                    if (obj != null)
                        return obj;
                }

                Debug.Log("Object not found");
                return default;
            }

            else
            {
                Debug.Log("No colliders detected");
                return default;
            }
        }


        public virtual T[] GetComponentsInScan<T>(T component)
        {
            if (colliders.Exists())
            {
                System.Collections.Generic.List<T> list = new System.Collections.Generic.List<T>();

                for (int i = 0; i < colliders.Length; i++)
                {
                    T obj = colliders[i].GetComponent<T>();
                    if (obj != null)
                        list.Add(obj);
                }

                return list.ToArray();
            }

            else
            {
                Debug.Log("No colliders detected");
                return default;
            }
        }


        protected override void OnDrawGizmos()
        {
            if (scannerGizmos.drawGizmos == false)
                return;
        }
    }

    [System.Serializable]
    public class ScannerGizmos
    {
        public bool drawGizmos;
        public Color color = Color.red;
    }
}


