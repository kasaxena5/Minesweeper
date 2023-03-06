using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinesweeperManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] numberSprites;

    [SerializeField]
    private Sprite bombSprite;

    [SerializeField]
    private Sprite flagSprite;

    [SerializeField]
    private Tile tilePrefab;

    [SerializeField]
    private Transform canvas;

    [SerializeField]
    private int n = 10;

    private Tile[,] grid;

    private void InitializeTiles()
    {
        grid = new Tile[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                grid[i, j] = Instantiate(tilePrefab);
                grid[i, j].i = i;
                grid[i, j].j = j;
                grid[i, j].transform.SetParent(canvas, false);
                grid[i, j].GetComponent<RectTransform>().localPosition = new Vector3(Tile.size * j - (n / 2) * Tile.size, Tile.size * i - (n / 2) * Tile.size, 0);

                int x = i;
                int y = j;
                //grid[i, j].GetComponent<Button>().onClick.AddListener(() => { OnClick(x, y); });
            }
        }
    }
    
    void Awake()
    {
        InitializeTiles();
    }

    void Update()
    {
        
    }
}
