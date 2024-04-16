using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    World,
    Cutscene,
    Battle,
    Menu,
}
public class Game : MonoBehaviour
{
    public GameState state { get; private set; }
    public static PlayerController Player { get; private set; }


}