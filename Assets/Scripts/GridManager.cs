using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable LocalVariableHidesMember

public class GridManager : MonoBehaviour
{
    public event System.Action<int, int> NumMovesChanged;
    
    public List<Sprite> Sprites = new();
    public GameObject TilePrefab;
    public int GridDimension = 6;
    public float Distance = 1.0f;
    private Tile[,] _grid;
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

    public static GridManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void StartNewGame()
    {
        EraseGrid();
        _grid = new Tile[GridDimension, GridDimension];
        ScoreManager.Instance.CurrentScore = 0;
        NumMoves = StartingMoves;
        InitGrid();
    }

    private void InitGrid()
    {
        Vector3 positionOffset = transform.position - new Vector3(GridDimension * Distance / 2.0f, GridDimension * Distance / 2.0f, 0);

        for (int row = 0; row < GridDimension; row++)
            for (int column = 0; column < GridDimension; column++)
            {
                var instantiateTile = Instantiate(TilePrefab.gameObject, transform);
                Tile newTile = instantiateTile.GetComponent<Tile>();

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

                SpriteRenderer renderer = newTile.GetComponentInChildren<SpriteRenderer>();
                renderer.sprite = possibleSprites[Random.Range(0, possibleSprites.Count)];

                newTile.Position = new Vector2Int(column, row);
                newTile.transform.position = new Vector3(column * Distance, row * Distance, 0) + positionOffset;
                
                _grid[column, row] = newTile;
            }
    }

    private void EraseGrid()
    {
        if (_grid != null)
        {
            foreach (var tile in _grid)
            {
                if(tile) Destroy(tile);
            }
        }
        _grid = null;
    }

    private Sprite GetSpriteAt(int column, int row)
    {
        var spriteRendererAt = GetSpriteRendererAt(column, row);
        return !spriteRendererAt ? null : spriteRendererAt.sprite;
    }

    private SpriteRenderer GetSpriteRendererAt(int column, int row)
    {
        if (column < 0 || column >= GridDimension
         || row < 0 || row >= GridDimension)
            return null;
        return _grid[column, row].Renderer;
    }

    public void SwapTiles(Vector2Int tile1Position, Vector2Int tile2Position)
    {
        SpriteRenderer renderer1 = _grid[tile1Position.x, tile1Position.y].Renderer;
        SpriteRenderer renderer2 = _grid[tile2Position.x, tile2Position.y].Renderer;

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
        HashSet<SpriteRenderer> matchedTiles = new HashSet<SpriteRenderer>();
        for (int row = 0; row < GridDimension; row++)
        {
            for (int column = 0; column < GridDimension; column++)
            {
                SpriteRenderer current = GetSpriteRendererAt(column, row);

                List<SpriteRenderer> horizontalMatches = FindColumnMatchForTile(column, row, current.sprite);
                if (horizontalMatches.Count >= 2)
                {
                    matchedTiles.UnionWith(horizontalMatches);
                    matchedTiles.Add(current);
                }

                List<SpriteRenderer> verticalMatches = FindRowMatchForTile(column, row, current.sprite);
                if (verticalMatches.Count >= 2)
                {
                    matchedTiles.UnionWith(verticalMatches);
                    matchedTiles.Add(current);
                }
            }
        }

        foreach (SpriteRenderer renderer in matchedTiles)
        {
            renderer.sprite = null;
        }
        ScoreManager.Instance.CurrentScore += matchedTiles.Count;
        return matchedTiles.Count > 0;
    }

    private List<SpriteRenderer> FindColumnMatchForTile(int col, int row, Sprite sprite)
    {
        List<SpriteRenderer> result = new List<SpriteRenderer>();
        for (int i = col + 1; i < GridDimension; i++)
        {
            SpriteRenderer nextColumn = GetSpriteRendererAt(i, row);
            if (nextColumn.sprite != sprite)
            {
                break;
            }
            result.Add(nextColumn);
        }
        return result;
    }

    private List<SpriteRenderer> FindRowMatchForTile(int col, int row, Sprite sprite)
    {
        List<SpriteRenderer> result = new List<SpriteRenderer>();
        for (int i = row + 1; i < GridDimension; i++)
        {
            SpriteRenderer nextRow = GetSpriteRendererAt(col, i);
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
                while (GetSpriteRendererAt(column, row).sprite == null)
                {
                    SpriteRenderer current = GetSpriteRendererAt(column, row);
                    SpriteRenderer next = current;
                    for (int filler = row; filler < GridDimension - 1; filler++)
                    {
                        next = GetSpriteRendererAt(column, filler + 1);
                        current.sprite = next.sprite;
                        current = next;
                    }
                    next.sprite = Sprites[Random.Range(0, Sprites.Count)];
                }
            }
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER");
        
        SoundManager.Instance.PlaySound(SoundType.TypeGameOver);
        bool isNewHighScore = ScoreManager.Instance.CheckAndSetHighScore(ScoreManager.Instance.CurrentScore);
        var infoToShow = new GameOverWindow.InfoToShow()
        {
            score = ScoreManager.Instance.CurrentScore,
            highscore = ScoreManager.Instance.HighScore,
            isNewHighScore = isNewHighScore,
        };
        MenuRouter.Instance.Router.Show<GameOverWindow>(infoToShow, callback: EraseGrid);
    }
}
