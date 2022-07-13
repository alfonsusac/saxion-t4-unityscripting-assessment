using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game", menuName = "Game")]
public class Game : ScriptableObject
{
    // Keeping Player's Data
    public int p_score;
    public int p_health;
}
