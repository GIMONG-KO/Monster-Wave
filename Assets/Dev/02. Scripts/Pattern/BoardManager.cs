using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public GameObject tilePrefab; // 타일 프리팹
    public Vector2Int boardSize = new Vector2Int(5, 5); // 보드 사이즈
    public int[,] tileArray; // 보드 배열
    
    public GameObject[] turrets;
    public GameObject currentTurret;
    public Button[] selectButtons;
    
    void Start()
    {
        tileArray = new int[boardSize.x, boardSize.y];
        
        for (int x = 0; x < boardSize.x; x++)
        {
            for (int z = 0; z < boardSize.y; z++)
            {
                GameObject tile = Instantiate(tilePrefab, this.transform);
                
                tile.transform.position = new Vector3(x, 0, z);
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                int x = Mathf.RoundToInt(hit.collider.transform.position.x);
                int z = Mathf.RoundToInt(hit.collider.transform.position.z);

                if (tileArray[x, z] == 0)
                {
                    GameObject turretObj = Instantiate(turrets[0]);
                    turretObj.transform.position = new Vector3(x, 0, z);

                    tileArray[x, z] = 1;
                }
            }
        }
    }

    public void OnChangeTurret(int index)
    {
        currentTurret = turrets[index];
    }
}