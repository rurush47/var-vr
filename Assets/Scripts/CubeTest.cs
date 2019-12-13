using UnityEngine;

public class CubeTest : MonoBehaviour
{
    private Material material;
    private GameManager gameManager;
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        gameManager = GameManager.Instance;
    }

    public void SetColor(string color)
    {
        Color uColor = Color.red;
        switch (color)
        {
            case "blue":
                uColor = Color.blue;
                break;
            case "yellow":
                uColor = Color.yellow;
                break;
        }

        material.color = uColor;
    }
}