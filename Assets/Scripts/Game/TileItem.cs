using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileItem : MonoBehaviour, IPointerClickHandler
{
    private static TileItem selected;
    
    [SerializeField] private Image _image;
    [SerializeField] private Color _normalImageColor = Color.white;
    [SerializeField] private Color _selectedImageColor = Color.grey;

    public Vector2Int Position { get; private set; }
    private GridManager _manager;

    public Image Image => _image;

    public void Select()
    {
        _image.color = _selectedImageColor;
    }

    public void Unselect()
    {
        _image.color = _normalImageColor;
    }

    public void SetUp(GridManager manager, Vector2Int gridPosition, Sprite startSprite)
    {
        _manager = manager;
        Image.sprite = startSprite;
        Position = gridPosition;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (selected != null)
        {
            if (selected == this)
                return;
            selected.Unselect();
            if (Mathf.Approximately(Vector2Int.Distance(selected.Position, Position), 1))
            {
                _manager.SwapTiles(Position, selected.Position);
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
