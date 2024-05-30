using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private static Tile selected;
    
    [SerializeField] private SpriteRenderer _renderer;
    
    

    public Vector2Int Position;

    public SpriteRenderer Renderer => _renderer;

    public void Select()
    {
        _renderer.color = Color.grey;
    }

    public void Unselect()
    {
        _renderer.color = Color.white;
    }

    private void OnMouseDown()
    {
        if (selected != null)
        {
            if (selected == this)
                return;
            selected.Unselect();
            if (Mathf.Approximately(Vector2Int.Distance(selected.Position, Position), 1))
            {
                GridManager.Instance.SwapTiles(Position, selected.Position);
                selected = null;
            }
            else
            {
                SoundManager.Instance.PlaySound(SoundType.TypeSelect);
                selected = this;
                Select();
            }
        }
        else
        {
            SoundManager.Instance.PlaySound(SoundType.TypeSelect);
            selected = this;
            Select();
        }
    }
}
