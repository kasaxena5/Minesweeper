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

    private bool[,] bombs;
    private bool areBombsInitialized = false;

    private int[] dx = { 1, -1, 0, 0, 1, -1, 1, -1 };
    private int[] dy = { 0, 0, 1, -1, 1, -1, -1, 1 };

    private void InitializeTiles()
    {
        grid = new Tile[n, n];
        bombs = new bool[n, n];
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
                grid[i, j].GetComponent<Button>().onClick.AddListener(() => { OnClick(x, y); });
            }
        }
    }

    private void InitializeBombs(int x, int y)
    {
        if (areBombsInitialized)
            return;

        for(int i = 0; i < n; i++)
        {
            for(int j = 0; j < n; j++)
            {
                bool flag = false;
                for(int k = 0; k < 8; k++)
                {
                    flag |= (i + dx[k] == x && j + dy[k] == y);
                }
                if (flag)
                    continue;
            
                if (Random.Range(0f, 1f) < 0.2f)
                {
                    bombs[i, j] = true;
                }
            }
        }
        areBombsInitialized = true;
    }
    
    void Awake()
    {
        InitializeTiles();
    }

    void OnClick(int i, int j)
    {
        if(Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right Click");
        }
        InitializeBombs(i, j);

        if(bombs[i, j])
        {
            grid[i, j].SetState(Tile.TileState.Bomb, bombSprite);
            return;
        }
        
        
        Queue<Tile> q = new Queue<Tile>();
        q.Enqueue(grid[i, j]);

        while (q.Count != 0)
        {
            Tile t = q.Dequeue();
            int cnt = 0;
            for (int k = 0; k < 8; k++)
            {
                int x = t.i + dx[k];
                int y = t.j + dy[k];
                if (x >= 0 && x < n && y >= 0 && y < n)
                {
                    if (bombs[x, y])
                        cnt++;
                }
            }

            grid[t.i, t.j].SetState(Tile.TileState.Revealed, numberSprites[cnt]);
            
            if(cnt == 0)
            {
                for (int k = 0; k < 8; k++)
                {
                    int x = t.i + dx[k];
                    int y = t.j + dy[k];
                    if (x >= 0 && x < n && y >= 0 && y < n)
                    {
                        if (!bombs[x, y] && grid[x, y].GetState() != Tile.TileState.Revealed)
                        {
                            grid[x, y].SetState(Tile.TileState.Revealed, numberSprites[cnt]);
                            q.Enqueue(grid[x, y]);
                        }
                    }
                }
            }
            
        }
        
    }
}
