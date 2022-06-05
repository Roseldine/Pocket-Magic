
using UnityEngine;

namespace StardropTools
{
    [System.Serializable]
    public struct Team
    {
        [SerializeField] string teamName;
        [SerializeField] Color teamColor;
        [Range(0, 255)][SerializeField] int teamIndex;

        public string Name { get => teamName; }
        public Color Color { get => teamColor; }
        public int Index { get => teamIndex; }

        public static Team NullTeam { get => new Team("Null", Color.clear, -1); }
        public static Team BlueTeam { get => new Team("Blue", Color.blue, 0); }
        public static Team RedTeam { get => new Team("Red", Color.red, 1); }

        public Team(string name, Color color, int index)
        {
            teamName = name;
            teamColor = color;
            teamIndex = index;
        }
    }
}