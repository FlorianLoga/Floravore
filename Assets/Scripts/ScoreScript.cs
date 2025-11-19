using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ScoreEntry
{
    public string playerName;
    public float timeSurvived;
}

[System.Serializable]
public class ScoreList
{
    public List<ScoreEntry> scores = new List<ScoreEntry>();
}
public class ScoreScript : MonoBehaviour
{
    

}
