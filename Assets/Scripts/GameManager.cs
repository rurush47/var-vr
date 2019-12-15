using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    #endregion

    [SerializeField] private Player player;

    public Vector3[] tracksPositions;

    private int currentTrackIndex = 1;

    private Vector3 GetLeftDiff()
    {
        if (currentTrackIndex < 1)
        {
            return Vector3.zero;
        }

        currentTrackIndex--;

        return tracksPositions[currentTrackIndex] - tracksPositions[currentTrackIndex + 1];
    }

    private Vector3 GetRightDiff()
    {
        if (currentTrackIndex >= tracksPositions.Length - 1)
        {
            return Vector3.zero;
        }

        currentTrackIndex++;

        return tracksPositions[currentTrackIndex] - tracksPositions[currentTrackIndex - 1];
    }

    public void MovePlayerLeft()
    {
        player.Move(GetLeftDiff());
    }

    public void MovePlayerRight()
    {
        player.Move(GetRightDiff());
    }
}
