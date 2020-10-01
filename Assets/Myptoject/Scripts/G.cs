using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G : MonoBehaviour
{
    //settings
    public static AudioSettings setting = null;
    public static SystemLanguage lang;
    public static int CURLANG = 0;

    //paths
    public static string DATA_PATH = Application.dataPath;
    public const string FILE_PATH = "music sheet";
    public const string CRAFT_PATH = "craft";
    public const string SETTING_PATH = "setting";

    public static string VERSION = Application.version;

    public static System.Random rng = new System.Random();
    public static SHA256 sha256 = sha256.Create();

    //games
    public const float TIMER_INTERVAL = 20.0f;

    public const int LANES = 1;

    public const float NOTE_SPEED = 2f;
    public enum POSITION { Fixed,Random};

    public static float PERFECT_MARGIN = 0.08f;
    public static float GOOD_MARGIN = 0.16f;
    public static float MISS_MARGIN = 0.2f;

    public const float POINT_BASE = 1000f;

    public const int PERFECT_PENALTY = 0;
    public const int GOOD_PENALTY = 0;
    public const int MISS_PENALTY = 10;

    public static float STARTLINE = 3f;
    public static float ACTIVELINE = -2.2f;
    public static readonly float DISTANSE_SA = (STARTLINE - ACTIVELINE) / G.NOTE_SPEED;

    public enum LANGUAGE { CH,EN,JP};

    public static class CRAF
    {
        public static float NOTES_SPEED = 300f;
        public static string currentmusic_path = null;
        public readonly static Dictionary<string, AudioType> mtype = new Dictionary<string, AudioType>() {
            { ".mp3",  AudioType.MPEG}, { ".wav",  AudioType.WAV},{ ".ogg",  AudioType.OGGVORBIS}
        };
    }
}
