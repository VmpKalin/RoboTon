using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable LocalVariableHidesMember

public class GridManager : MonoBehaviour
{
    public event System.Action<int, int> NumMovesChanged;
    
    public List<Sprite> Sprites = new();
    public TileItem TilePrefab;
    public int GridDimension = 6;
    public float Distance = 1.0f;
    private TileItem[,] _grid;
    public int StartingMoves = 50;
    private int _numMoves;
    
    
    
    public int NumMoves
    {
        get => _numMoves;

        set
        {
            var oldNumMoves = _numMoves;
            _numMoves = value;
            NumMovesChanged?.Invoke(oldNumMoves, value);
        }
    }

    public void StartNewGame()
    {
        EraseGrid();
        _grid = new TileItem[GridDimension, GridDimension];
        ScoreManager.Instance.CurrentScore = 0;
        NumMoves = StartingMoves;
        InitGrid();
    }

    private void InitGrid()
    {
        RectTransform rectTransform = transform as RectTransform;
        
        var distanceHorizontal = rectTransform.rect.width / GridDimension;
        var distanceVertical = rectTransform.rect.height / GridDimension;
        var distance = Mathf.Min(distanceHorizontal, distanceVertical);
        Vector3 positionOffset = - new Vector3(GridDimension * distance / 2.0f, GridDimension * distance / 2.0f, 0);

        for (int row = 0; row < GridDimension; row++)
            for (int column = 0; column < GridDimension; column++)
            {
                var newTile = Instantiate(TilePrefab.gameObject, transform).GetComponent<TileItem>();
                
                List<Sprite> possibleSprites = new List<Sprite>(Sprites);

                //Choose what sprite to use for this cell
                Sprite left1 = GetSpriteAt(column - 1, row);
                Sprite left2 = GetSpriteAt(column - 2, row);
                if (left2 != null && left1 == left2)
                {
                    possibleSprites.Remove(left1);
                }

                Sprite down1 = GetSpriteAt(column, row - 1);
                Sprite down2 = GetSpriteAt(column, row - 2);
                if (down2 != null && down1 == down2)
                {
                    possibleSprites.Remove(down1);
                }

                newTile.SetUp(this,new Vector2Int(column, row) , possibleSprites[Random.Range(0, possibleSprites.Count)]);
                newTile.transform.localPosition = new Vector3(column * distance, row * distance, 0) + positionOffset;
                var tileRectTransform = newTile.transform as RectTransform;
                tileRectTransform.sizeDelta = new Vector2(distance, distance);
                _grid[column, row] = newTile;
            }
    }

    private void EraseGrid()
    {
        if (_grid != null)
        {
            foreach (var tile in _grid)
            {
                if(tile) Destroy(tile.gameObject);
            }
        }
        _grid = null;
    }

    private Sprite GetSpriteAt(int column, int row)
    {
        var imageAt = GetImageAt(column, row);
        return !imageAt ? null : imageAt.sprite;
    }

    private Image GetImageAt(int column, int row)
    {
        if (column < 0 || column >= GridDimension
         || row < 0 || row >= GridDimension)
            return null;
        return _grid[column, row].Image;
    }

    public void SwapTiles(Vector2Int tile1Position, Vector2Int tile2Position)
    {
        Image renderer1 = _grid[tile1Position.x, tile1Position.y].Image;
        Image renderer2 = _grid[tile2Position.x, tile2Position.y].Image;

        Sprite temp = renderer1.sprite;
        renderer1.sprite = renderer2.sprite;
        renderer2.sprite = temp;

        bool changesOccurs = CheckMatches();
        if(!changesOccurs)
        {
            temp = renderer1.sprite;
            renderer1.sprite = renderer2.sprite;
            renderer2.sprite = temp;
            SoundManager.Instance.PlaySound(SoundType.TypeMove);
        }
        else
        {
            SoundManager.Instance.PlaySound(SoundType.TypePop);
            NumMoves--;
            do
            {
                FillHoles();
            } while (CheckMatches());
            if (NumMoves <= 0)
            {
                NumMoves = 0;
                GameOver();
            }
        }
    }

    private bool CheckMatches()
    {
        HashSet<Image> matchedTiles = new HashSet<Image>();
        for (int row = 0; row < GridDimension; row++)
        {
            for (int column = 0; column < GridDimension; column++)
            {
                Image current = GetImageAt(column, row);

                List<Image> horizontalMatches = FindColumnMatchForTile(column, row, current.sprite);
                if (horizontalMatches.Count >= 2)
                {
                    matchedTiles.UnionWith(horizontalMatches);
                    matchedTiles.Add(current);
                }

                List<Image> verticalMatches = FindRowMatchForTile(column, row, current.sprite);
                if (verticalMatches.Count >= 2)
                {
                    matchedTiles.UnionWith(verticalMatches);
                    matchedTiles.Add(current);
                }
            }
        }

        foreach (Image renderer in matchedTiles)
        {
            renderer.sprite = null;
        }
        ScoreManager.Instance.CurrentScore += matchedTiles.Count;
        return matchedTiles.Count > 0;
    }

    private List<Image> FindColumnMatchForTile(int col, int row, Sprite sprite)
    {
        List<Image> result = new List<Image>();
        for (int i = col + 1; i < GridDimension; i++)
        {
            Image nextColumn = GetImageAt(i, row);
            if (nextColumn.sprite != sprite)
            {
                break;
            }
            result.Add(nextColumn);
        }
        return result;
    }

    private List<Image> FindRowMatchForTile(int col, int row, Sprite sprite)
    {
        List<Image> result = new List<Image>();
        for (int i = row + 1; i < GridDimension; i++)
        {
            Image nextRow = GetImageAt(col, i);
            if (nextRow.sprite != sprite)
            {
                break;
            }
            result.Add(nextRow);
        }
        return result;
    }

    private void FillHoles()
    {
        for (int column = 0; column < GridDimension; column++)
            for (int row = 0; row < GridDimension; row++)
            {
                while (GetImageAt(column, row).sprite == null)
                {
                    Image current = GetImageAt(column, row);
                    Image next = current;
                    for (int filler = row; filler < GridDimension - 1; filler++)
                    {
                        next = GetImageAt(column, filler + 1);
                        current.sprite = next.sprite;
                        current = next;
                    }
                    next.sprite = Sprites[Random.Range(0, Sprites.Count)];
                }
            }
    }

    private void GameOver()
    {
        SoundManager.Instance.PlaySound(SoundType.TypeGameOver);
        bool isNewHighScore = ScoreManager.Instance.CheckAndSetHighScore(ScoreManager.Instance.CurrentScore);
        var infoToShow = new MainMenuWindow.InfoToShow()
        {
            Score = ScoreManager.Instance.CurrentScore,
            Highscore = ScoreManager.Instance.HighScore,
            IsNewHighScore = isNewHighScore,
        };
        MenuRouter.Instance.Router.Show<MainMenuWindow>(infoToShow, callback: EraseGrid);
    }

}
