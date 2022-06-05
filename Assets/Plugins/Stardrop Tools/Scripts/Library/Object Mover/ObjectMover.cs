using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    public class ObjectMover : CoreObject
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            ObjectMoverManager.Instance.AddMover(this);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            ObjectMoverManager.Instance.RemoveMover(this);
        }

        public void Move(Vector3 direction)
        {
            Transform.Translate(direction * Time.deltaTime);
        }
    }
}
