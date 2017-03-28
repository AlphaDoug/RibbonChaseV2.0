using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FlightKit
{
    public class GamePickUpsAmount
    {
        public static bool[] isCurrentLevelPickUpsCollected;
        public static List<GameObject> currentLevelPickUps;
        public static int currentPickUpsAmount;
        public static int currentCollectingPickUpIndex;
        public static int pickUpsAmount;
        public static AeroplaneAiControl.AIDifficuty aiDifficuty;
        public static Transform aiCurrentTarget;
        public static string time;
    }
}

