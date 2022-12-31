using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    internal static readonly float EPSILON = 0.00001f;

    public static string AppName { get { return ""; } }
    public static string PackageName { get { return "com.ap."; } }
    public static string FacebookPageURL { get { return "https://www.facebook.com/Vital-Hits-Games-103407214505915/"; } }
#if UNITY_ANDROID
    public static string AppURL { get { return "https://play.google.com/store/apps/details?id=" + PackageName; } }
    public static string StoreURL { get { return "https://play.google.com/store/apps/developer?id=Mini+Dreams"; } }
#elif UNITY_IOS
    public static string AppURL { get { return "https://apps.apple.com/us/app/fillin-good-3d/id1495360846?ls=1&mt=8"; } }
    public static string StoreURL { get { return "https://apps.apple.com/us/developer/chang-linh/id1495360845"; } }
#else
    public static string AppURL { get { return "PC Build"; } }
    public static string StoreURL { get { return "PC Build"; } }
#endif

    public static class Tag
    {
        public static string MainCharacter = "MainCharacter";
        public static string BadGuy = "BadGuy";
        public static string Trap = "Trap";
        public static string Obstacle = "Obstacle";
        public static string Loot = "Loot";
        public static string GrabItem = "GrabItem";
    }

    public static class Layer
    {
        public static string Obstacle = "Obstacle";
        public static string DisabledGround = "DisabledGround";
        public static string PathPoint = "PathPoint";
    }

    public static string SharingContent { get { return "I got [score] score in this awesome " + AppName + " game! Who can beat me?"; } }
    public static string[] RateUsContent
    {
        get
        {
            return new string[]
            {
                "Give us your valuable feedback to improve the game?",
                "Like "+ AppName+"? \nRate it please!",
                "Your precious rating is important to "+ AppName+". \nRate now?",
                "Enjoying the game? \nTell others how cool you play it!"
            };
        }
    }

#if UNITY_ANDROID
#elif UNITY_IOS
#endif
}
