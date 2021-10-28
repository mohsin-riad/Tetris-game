using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    public Piece activePiece { get; private set; }
    public TetrominoData[] tetrominoes;
    // public Vector3Int spawnPosition = new Vector3Int(-1, 8, 0);
    public Vector2Int boardSize = new Vector2Int(10, 20);
    public Vector3Int spawnPosition;
    public RectInt Bounds{
        get{
             Vector2Int position = new Vector2Int(-this.boardSize.x/2, -this.boardSize.y/2);
             return new RectInt(position, this.boardSize); 
        }
    }
    private void Awake(){
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();
        for(int i=0; i < this.tetrominoes.Length ;i++){
            this.tetrominoes[i].Initialize();
        }
    }
    private void Start(){
        SpawnPiece();
    }
    public void SpawnPiece()
    {
        TetrominoData data = this.tetrominoes[Random.Range(0, this.tetrominoes.Length)];

        this.activePiece.Initialize(this, this.spawnPosition, data);
        Set(this.activePiece);
    }
    public void Set(Piece piece){
        for(int i=0; i < piece.cells.Length ;i++){
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }
    public void Clear(Piece piece){
        for(int i=0; i < piece.cells.Length ;i++){
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }
    public bool IsValidPos(Piece piece, Vector3Int position){
        RectInt bounds = this.Bounds;
        for(int i=0; i < piece.cells.Length ;i++){
            Vector3Int tilePosition = piece.cells[i] + position;

            if(!bounds.Contains((Vector2Int)tilePosition)){
                return false;
            }
            if(this.tilemap.HasTile(tilePosition)){
                return false;
            }
        }
        return true;
    }
    public void ClearLines(){
        RectInt bounds = this.Bounds;
        int row = bounds.yMin;
        while(row < bounds.yMax){
            if(IsLineFull(row)){
                LineClear(row);
            }
            else {
                row++;
            }
        }
    }
    private bool IsLineFull(int row){
        RectInt bounds = this.Bounds;
        for(int col = bounds.xMin ;col < bounds.xMax; col++){
            Vector3Int position = new Vector3Int(col, row, 0);
            if(!this.tilemap.HasTile(position)){
                return false;
            }
        }   
        return true;
    }
    private void LineClear(int row){
        RectInt bounds = this.Bounds;
        for(int col = bounds.xMin ;col < bounds.xMax; col++){
            Vector3Int position = new Vector3Int(col, row, 0);
            this.tilemap.SetTile(position, null);
        }   
    }
}

