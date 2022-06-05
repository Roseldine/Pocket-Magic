
using System.IO;
using UnityEngine;

namespace StardropTools.Tween
{
    [CreateAssetMenu(menuName = "Stardrop / Tween / Tween Curves")]
    public class TweenCurvesSO : SingletonSO<TweenCurvesSO>
    {
        [SerializeField] string filePath;
        [SerializeField] bool generateFile;
        [Space]
        [SerializeField] TweenAnimCurve[] curves;

        public AnimationCurve GetCurve(int id) => curves[id].curve;

        void GenerateFile()
        {
            StreamWriter writer = new StreamWriter(filePath, true);

            for (int i = 0; i < curves.Length; i++)
            {
                var curve = curves[i];

                string code = "public static AnimationCurve " + curve.name + "{ get { return new AnimationCurve (";
                for (int j = 0; j < curve.keys.Length; j++)
                {
                    Keyframe currentFrame = curve.keys[j];
                    code += "new Keyframe (" + currentFrame.time + "f, " + currentFrame.value + "f, " + currentFrame.inTangent + "f, " + currentFrame.outTangent + "f)";
                    if (j < curve.keys.Length - 1) code += ", ";
                }
                code += "); } }";

                writer.WriteLine(code);
                Debug.Log(code);
            }

            writer.Close();

            //public static AnimationCurve EaseIn
            //{
            //    get
            //    {
            //        if (easeIn == null)
            //            easeIn = new AnimationCurve(new Keyframe(0, 0, 0, 0), new Keyframe(1, 1, 1, 0));
            //        return easeIn;
            //    }
            //}
        }

        private void OnValidate()
        {
            if (generateFile)
                GenerateFile();
            generateFile = false;
        }
    }

    [System.Serializable]
    public class TweenAnimCurve
    {
        public string name;
        public AnimationCurve curve;
        public Keyframe[] keys { get => curve.keys; }
    }
}