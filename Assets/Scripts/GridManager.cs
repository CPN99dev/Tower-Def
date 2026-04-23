using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Settings")]
    public LayerMask towerLayer;
    public LayerMask obstacleLayer;
    public float cellSize = 1f;

    [Header("Building Data")]
    public GameObject currentTowerPrefab;
    public GameObject ghostObj;
    private GhostPreview ghostPreviewScript;
    private int currentCost;

    public static GridManager Instance;
    public bool buildingMode;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        buildingMode = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) CancelBuilding();

        if (currentTowerPrefab == null || !buildingMode) return;

        HandleGhostPreview();

        if (Input.GetMouseButtonDown(0) && buildingMode)
        {
            TryBuildTower();
        }
    }

    public void SelectTowerToBuild(TowerData data)
    {
        if (ghostObj != null) Destroy(ghostObj);

        currentTowerPrefab = data.prefab;
        ghostObj = null;
        currentCost = data.cost;
        buildingMode = true;
    }

    public void CancelBuilding()
    {
        buildingMode = false;
        currentTowerPrefab = null;
        if (ghostObj != null) Destroy(ghostObj);
    }

    void HandleGhostPreview()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float snappedX = Mathf.Round(mousePos.x / cellSize) * cellSize;
        float snappedY = Mathf.Round(mousePos.y / cellSize) * cellSize;
        Vector3 targetPos = new Vector3(snappedX, snappedY, 0f);

        if (ghostObj == null)
        {
            ghostObj = Instantiate(currentTowerPrefab); 

            if (ghostObj.TryGetComponent<Collider2D>(out Collider2D col))
            {
                Destroy(col); 
            }

            ghostPreviewScript = ghostObj.AddComponent<GhostPreview>();
            ghostPreviewScript.spriteRenderer = ghostObj.GetComponent<SpriteRenderer>();
        }

        if (ghostPreviewScript != null)
        {
            bool canBuild = CheckBuildValidity(targetPos);
            ghostPreviewScript.SetPosition(targetPos, canBuild);
        }
    }

    bool CheckBuildValidity(Vector2 pos)
    {
        float sizeMultiplier = 1.9f;
        Vector2 boxSize = new Vector2(cellSize * sizeMultiplier, cellSize * sizeMultiplier);

        Collider2D hit = Physics2D.OverlapBox(pos, boxSize, 0f, obstacleLayer);
        return hit == null;
    }

    void TryBuildTower()
    {
        if (ghostObj == null) return;

        Vector3 buildPos = ghostObj.transform.position;

        if (CheckBuildValidity(buildPos))
        {
                GameManager.Instance.SpendMoney(currentCost);
                Instantiate(currentTowerPrefab, buildPos, Quaternion.identity);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        float snappedX = Mathf.Round(pos.x / cellSize) * cellSize;
        float snappedY = Mathf.Round(pos.y / cellSize) * cellSize;
        Gizmos.DrawWireCube(new Vector3(snappedX, snappedY, 0), new Vector3(cellSize * 0.9f, cellSize * 0.9f, 0.1f));
    }
}