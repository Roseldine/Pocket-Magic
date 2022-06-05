
namespace StardropTools.Tween
{
    public interface ITweenComponent
    {
        public enum GlobalOrLocal { global, local }

        public void Initialize();

        public void PlayTween();
        public void PauseTween();
        public void StopTween();
    }
}