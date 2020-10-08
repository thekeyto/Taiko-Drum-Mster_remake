using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class G : MonoBehaviour
{
    //settings
    public static Setting setting = new Setting();
    public static Language lang;
    public static int CURLANG = 0;

    //paths
    public static string DATA_PATH = Application.dataPath;
    public const string FILE_PATH = "music sheet";
    public const string CRAFT_PATH = "craft";
    public const string SETTING_PATH = "setting";

    public static string VERSION = Application.version;

    public static System.Random rng = new System.Random();
    public static SHA256 sha256 = SHA256.Create();

    //games
    public const double TIMER_INTERVAL = 20.0f;

    public const int LANES = 4;

    public const double NOTE_SPEED = 2f;
    public enum POSITION { Fixed,Random};

    public static double PERFECT_MARGIN = 0.12f;
    public static double Great_MARGIN = 0.16f;
    public static double GOOD_MARGIN = 0.20f;
    public static double MISS_MARGIN = 0.20f;

    public const double POINT_BASE = 1000f;

    public const int PERFECT_PENALTY = 0;
    public const int GOOD_PENALTY = 0;
    public const int MISS_PENALTY = 10;

    public static double STARTLINE = 3f;
    public static double ACTIVELINE = -2.2f;
    public static readonly double DISTANSE_SA = (STARTLINE - ACTIVELINE) / G.NOTE_SPEED;

    public enum LANGUAGE { CH, EN, JP };
    
    public static class CRAF
    {
        public static double NOTES_SPEED = 4f;
        public static string currentmusic_path = null;
        public readonly static Dictionary<string, AudioType> mtype = new Dictionary<string, AudioType>() {
            { ".mp3",  AudioType.MPEG}, { ".wav",  AudioType.WAV},{ ".ogg",  AudioType.OGGVORBIS}
        };
    }
}
