
using UnityEngine;
using StardropTools.Pool;

namespace StardropTools
{
    public class CoreObject : CoreComponent, IPoolable
    {
        [Header("Initialization")]
        [SerializeField] protected CoreObjectData coreData;

        #region Properties
        public bool IsActive { get => coreData.IsActive; set => SetActive(value); }
        public bool CanUpdate { get => coreData.CanUpdate; set => coreData.SetUpdate(value); }
        public bool CanDebug { get => coreData.Debug; }
        public bool DrawGizmos { get => coreData.DrawGizmos; }

        public GameObject GameObject { get => coreData.GameObject; }
        public Transform Transform { get => coreData.Transform; }
        public Transform Parent { get => Transform.parent; set => SetParent(value); }

        public Vector3 Position { get => Transform.position; set => Transform.position = value; }
        public Vector3 LocalPosition { get => Transform.localPosition; set => Transform.localPosition = value; }

        public Quaternion Rotation { get => Transform.rotation; set => Transform.rotation = value; }
        public Quaternion LocalRotation { get => Transform.localRotation; set => Transform.localRotation = value; }

        public Vector3 EulerRotation { get => Transform.eulerAngles; set => Transform.eulerAngles = value; }
        public Vector3 LocalEulerRotation { get => Transform.localEulerAngles; set => Transform.localEulerAngles = value; }

        public Vector3 Scale { get => Transform.localScale; set => Transform.localScale = value; }

        public Vector3 InitialPosition { get; private set; }
        public Vector3 EnabledPosition { get; private set; }
        public Vector3 DisabledPosition { get; private set; }

        public Vector3 SpawnedPosition { get; private set; }

        public float DistanceFromInitialPosition { get => GetDistanceFromInitialPosition(); }
        public float DistanceFromEnabledPosition { get => GetDistanceFromEnabledPosition(); }
        public float DistanceFromDisabledPosition { get => GetDistanceFromDisabledPosition(); }

        // Position
        public float PosX { get => Position.x; set => Position.SetX(value); }
        public float PosY { get => Position.y; set => Position.SetY(value); }
        public float PosZ { get => Position.z; set => Position.SetZ(value); }

        public Vector2 PosXY { get => Position.GetXY(); set => Position.SetXY(value.x, value.y); }
        public Vector2 PosXZ { get => Position.GetXZ(); set => Position.SetXZ(value.x, value.y); }
        public Vector2 PosYZ { get => Position.GetYZ(); set => Position.SetYZ(value.x, value.y); }


        // Rotation
        public float RotX { get => Rotation.x; }
        public float RotY { get => Rotation.y; }
        public float RotZ { get => Rotation.z; }
        public float RotW { get => Rotation.w; }

        public float LocalRotX { get => LocalRotation.x; }
        public float LocalRotY { get => LocalRotation.y; }
        public float LocalRotZ { get => LocalRotation.z; }
        public float LocalRotW { get => LocalRotation.w; }

        public float EulerRotX { get => EulerRotation.x; set => EulerRotation.SetX(value); }
        public float EulerRotY { get => EulerRotation.y; set => EulerRotation.SetY(value); }
        public float EulerRotZ { get => EulerRotation.z; set => EulerRotation.SetZ(value); }

        public float LocalEulerRotX { get => LocalRotation.x; set => EulerRotation.SetX(value); }
        public float LocalEulerRotY { get => LocalRotation.y; set => EulerRotation.SetY(value); }
        public float LocalEulerRotZ { get => LocalRotation.z; set => EulerRotation.SetZ(value); }


        // Scale
        public float ScaleX { get => Scale.x; set => Scale.SetX(value); }
        public float ScaleY { get => Scale.y; set => Scale.SetY(value); }
        public float ScaleZ { get => Scale.z; set => Scale.SetZ(value); }
        #endregion // properties


        #region Print & Debug.log
        /// <summary>
        /// substitute to Debug.Log();
        /// </summary>
        public static void Print(object message) => Debug.Log(message);

        /// <summary>
        /// substitute to Debug.LogWarning();
        /// </summary>
        public static void PrintWarning(object message) => Debug.LogWarning(message);
        #endregion // print

        #region Events
        public readonly CoreEvent OnSetParent = new CoreEvent();
        public readonly CoreEvent OnAddChild = new CoreEvent();
        public readonly CoreEvent OnRemoveChild = new CoreEvent();

        public readonly CoreEvent OnDestroyObject = new CoreEvent();

        public readonly CoreEvent OnActivate = new CoreEvent();
        public readonly CoreEvent OnDeactivate = new CoreEvent();
        #endregion // events


        protected override void Awake()
        {
            DataCheck();

            if (coreData.Initialization == CoreObjectData.InitializationType.awake)
                Initialize();

            if (coreData.LateInitialization == CoreObjectData.InitializationType.awake)
                LateInitialize();

            OnAwake?.Invoke();
        }

        protected override void Start()
        {
            DataCheck();

            if (coreData.Initialization == CoreObjectData.InitializationType.start)
                Initialize();

            if (coreData.LateInitialization == CoreObjectData.InitializationType.start)
                LateInitialize();

            OnStart?.Invoke();
        }

        public override void Initialize()
        {
            if (IsInitialized)
                return;

            InitialPosition = Transform.position;

            DataCheck();
            coreData.Initialize();
            IsInitialized = true;

            OnInitialize?.Invoke();
        }


        public virtual void Initialize(bool canUpdate)
        {
            if (IsInitialized)
            {
                if (CanDebug)
                    Print(name + "is already Initialized");

                return;
            }

            SetCanUpdate(canUpdate);
            Initialize();
        }

        public override void LateInitialize()
        {
            if (IsLateInitialized)
            {
                if (CanDebug)
                    Print(name + "is already LateInitialized");

                return;
            }

            DataCheck();
            coreData.LateInitialize();

            OnLateInitialize?.Invoke();
        }

        public virtual void SubscribeToEvents() { }

        public virtual void UnsubscribeFromEvents() { }

        public virtual void UnsubscribeFromEventsOnDisable() { }

        protected bool UpdateCheck()
        {
            if (IsInitialized == false || CanUpdate == false)
            {
                if (CanDebug && IsInitialized == false)
                    Print(name + " isn't Initialized");

                if (CanDebug && CanUpdate == false)
                    Print(name + " can't Update");

                return false;
            }

            else
                return true;
        }

        public virtual void UpdateObject()
        {
            if (UpdateCheck() == false)
                return;
        }

        public virtual void LateUpdateObject()
        {
            if (UpdateCheck() == false)
                return;
        }

        public virtual void FixedUpdateObject()
        {
            if (UpdateCheck() == false)
                return;
        }

        public virtual void HandleInput()
        {
            if (UpdateCheck() == false)
                return;
        }

        public virtual void ResetObject()
        {
            if (IsInitialized == false)
                return;
        }

        public virtual void SetCanDrawGizmos(bool value) => coreData.DrawGizmos = value;

        protected override void OnEnable()
        {
            EnabledPosition = Transform.position;

            OnEnabled?.Invoke();
        }

        protected override void OnDisable()
        {
            DisabledPosition = Transform.position;

            UnsubscribeFromEvents();
            UnsubscribeFromEventsOnDisable();

            OnDisabled?.Invoke();
        }

        protected virtual void OnDestroy()
        {
            OnDestroyObject?.Invoke();
        }


        protected virtual void OnValidate()
        {
            if (coreData == null)
                coreData = new CoreObjectData();

            if (GameObject == null)
                coreData.SetGameObject(gameObject);
            if (Transform == null)
                coreData.SetTransform(transform);
        }

        protected virtual void OnDrawGizmos()
        {
            if (DrawGizmos == false)
                return;
        }


        protected virtual void DataCheck()
        {
            if (coreData == null)
                coreData = new CoreObjectData(gameObject, transform);
        }


        //
        // Setters ------------------------------------------------------------------------------
        //

        public void SetLayer(int layerIndex) => GameObject.layer = layerIndex;

        public void SetActive(bool value)
        {
            coreData.SetActive(value);

            if (GameObject != null)
                GameObject.SetActive(value);
            else if (CanDebug)
                Print("Object unassigned");

            if (value)
                OnActivate?.Invoke();
            else
                OnDeactivate?.Invoke();
        }

        public void SetCanUpdate(bool value) => coreData.SetUpdate(value);

        public void SetParent(Transform parent)
        {
            if (Transform.parent != parent)
            {
                Transform.parent = parent;
                OnSetParent?.Invoke();
            }

            else if (CanDebug)
                Print(name + " is already child of " + parent);
        }

        public void SetChild(Transform child)
        {
            if (child.parent != Transform)
            {
                child.parent = Transform;
                OnAddChild?.Invoke();
            }

            else if (CanDebug)
                Print(name + "is already parent of " + child);
        }

        public void SetChildIndex(Transform child, int index)
        {
            SetChild(child);
            child.SetSiblingIndex(index);
        }

        public void SetChildIndex(int childIndex, int index)
        {
            Transform child = Transform.GetChild(childIndex);
            child.SetSiblingIndex(index);
        }



        // position
        public void SetPosition(Vector3 position)
            => Position = position;

        public void SetLocalPosition(Vector3 localPosition)
            => LocalPosition = localPosition;

        public void SetPosX(float value)
            => Position = new Vector3(value, PosY, PosZ);

        public void SetPosY(float value)
            => Position = new Vector3(PosX, value, PosZ);

        public void SetPosZ(float value)
            => Position = new Vector3(PosX, PosY, value);

        public void SetLocalPosX(float value)
            => LocalPosition = new Vector3(value, LocalPosition.y, LocalPosition.z);

        public void SetLocalPosY(float value)
            => LocalPosition = new Vector3(LocalPosition.x, value, LocalPosition.z);

        public void SetLocalPosZ(float value)
            => LocalPosition = new Vector3(LocalPosition.x, LocalPosition.y, value);


        // Rotation
        public void SetRotation(Quaternion rotation)
            => Rotation = rotation;

        public void SetLocalRotation(Quaternion localRotation)
            => LocalRotation = localRotation;

        public void SetRotation(Vector3 rotation)
            => EulerRotation = rotation;
        public void SetLocalRotation(Vector3 rotation)
            => LocalEulerRotation = rotation;


        // scale
        public void SetScale(Vector3 scale)
            => Scale = scale;


        public Vector3 DirectionTo(Vector3 target) => target - Position;
        public Vector3 DirectionTo(Transform target) => target.position - Position;

        public Vector3 DirectionFrom(Vector3 target) => Position - target;
        public Vector3 DirectionFrom(Transform target) => Position - target.position;

        public float DistanceFrom(Vector3 target) => DirectionTo(target).magnitude;
        public float DistanceFrom(Transform target) => DirectionTo(target).magnitude;

        public float DistanceSquaredFrom(Vector3 target) => DirectionTo(target).sqrMagnitude;
        public float DistanceSquaredFrom(Transform target) => DirectionTo(target).sqrMagnitude;

        public float GetDistanceFromInitialPosition() => DistanceFrom(InitialPosition);
        public float GetDistanceFromEnabledPosition() => DistanceFrom(EnabledPosition);
        public float GetDistanceFromDisabledPosition() => DistanceFrom(DisabledPosition);


        public Quaternion LookAt(Vector3 direction, bool lockX = true, bool lockY = false, bool lockZ = true)
        {
            if (direction == Vector3.zero)
            {
                //Debug.Log("Look rotation is zero");
                return Quaternion.identity;
            }

            Quaternion lookRot = Quaternion.LookRotation(direction);

            if (lockX) lookRot.x = 0;
            if (lockY) lookRot.y = 0;
            if (lockZ) lookRot.z = 0;

            SetRotation(lookRot);

            return lookRot;
        }

        public Quaternion LookAt(Transform target, bool lockX = false, bool lockY = true, bool lockZ = false)
        {
            Vector3 lookDir = DirectionTo(target.position);
            Quaternion targetRot = LookAt(lookDir, lockX, lockY, lockZ);

            return targetRot;
        }


        public Quaternion SmoothLookAt(Vector3 direction, float lookSpeed, bool lockX = false, bool lockY = true, bool lockZ = false)
        {
            if (direction == Vector3.zero)
                return Quaternion.identity;

            Quaternion lookRot = Quaternion.LookRotation(direction);
            Quaternion targetRot = Quaternion.Slerp(Rotation, lookRot, Time.deltaTime * lookSpeed);

            if (lockX) lookRot.x = 0;
            if (lockY) lookRot.y = 0;
            if (lockZ) lookRot.z = 0;

            SetRotation(targetRot);

            return targetRot;
        }

        public Quaternion SmoothLookAt(Transform target, float lookSpeed, bool lockX = false, bool lockY = true, bool lockZ = false)
        {
            Vector3 lookDir = DirectionTo(target.position);
            Quaternion targetRot = SmoothLookAt(lookDir, lookSpeed, lockX, lockY, lockZ);

            return targetRot;
        }

        public virtual void LifeTime(GameObjectPool pool, float time)
        {
            PooledObject pooled = CoreComponentFinder.GetPooledByInstanceID(GetInstanceID());

            if (pooled == null)
            {
                Debug.LogError("Attack has no pool!");
                return;
            }

            pooled.LifeTime(pool, time);
        }

        public virtual void OnSpawn()
        {
            SpawnedPosition = Position;
        }

        public virtual void OnDespawn()
        {

        }

        public virtual void Despawn(bool resetParent = true)
        {
            PooledObject pooled = CoreComponentFinder.GetPooledByInstanceID(GetInstanceID());

            if (pooled != null)
                pooled.Despawn(resetParent);
        }
    }
}


