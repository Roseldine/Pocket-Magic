
namespace StardropTools.UI
{
    public class UISizeCopyCluster : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] UnityEngine.Transform parent;
        [UnityEngine.SerializeField] UISizeCopy[] sizeCopies;
        [UnityEngine.SerializeField] bool getCopies;
        [UnityEngine.SerializeField] bool copy;

        public void Copy()
        {
            if (sizeCopies != null && sizeCopies.Length > 0)
                foreach (UISizeCopy copy in sizeCopies)
                    copy.Copy();
        }

        public void GetCopies()
        {
            if (parent != null)
                sizeCopies = Utilities.GetItems<UISizeCopy>(parent);
        }

        private void OnValidate()
        {
            if (getCopies)
            {
                GetCopies();
                getCopies = false;
            }

            if (copy)
                Copy();
            copy = false;
        }
    }
}