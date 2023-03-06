using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public enum TileState
    {
        Unrevealed,
        Bomb,
        Flag,
        Revealed
    }
    private TileState state = TileState.Unrevealed;

    void Awake()
    {
        size = (int)this.GetComponent<RectTransform>().rect.width;
    }

    public void SetState(TileState newState, Sprite sprite)
    {
        state = newState;
        this.GetComponent<Image>().sprite = sprite;
    }

    public TileState GetState()
    {
        return state;
    }

    public int i;
    public int j;
    public static int size;

}
