using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGen : MonoBehaviour
{
    // Start is called before the first frame update
    private Grid levelGrid;
    public GameObject[] npcs;
    public GameObject spawnGrid;
    public GameObject regionGrid;
    public GameObject bossRoomGrid;
    public GameObject enemy;
    private float offsetX = 0;
    public Tile wall;
    public Tile background;
    public Tile deco;

    void Start()
    {
        levelGrid = GetComponent<Grid>();
        //gen
        GameObject go = Instantiate(spawnGrid, transform);
        //npc
        Vector3 pos = transform.position + new Vector3(10, 5, 0);   
        GameObject npc = Instantiate(npcs[0], transform);
        npc.transform.position = pos;
        //world
        go.transform.Find("Tilemap Spawn").GetComponent<Tilemap>().CompressBounds();
        offsetX += go.transform.Find("Tilemap Spawn").GetComponent<Tilemap>().size.x * go.GetComponent<Grid>().cellSize.x;
        int buildings = Random.Range(1, 6);
        for(int i = 0; i < buildings; i++)
        {
            GenBuilding();
        }
        //instantiate boss room
        Vector3 posBossRoom = transform.position + new Vector3(offsetX, 1, 0);
        GameObject goBossRoom = Instantiate(bossRoomGrid, transform);
        goBossRoom.transform.position = posBossRoom;
    }

    void GenBuilding()
    {
        GameObject go = Instantiate(regionGrid, transform);
        Tilemap solid = go.transform.Find("Tilemap").GetComponent<Tilemap>();
        Tilemap background = go.transform.Find("Tilemap Background").GetComponent<Tilemap>();
        Tilemap decoration = go.transform.Find("Tilemap Decoration").GetComponent<Tilemap>();
        Vector3 pos = go.transform.position;
        int roomHeight = 4;
        pos.x += offsetX;
        go.transform.position = pos;
        int width = Random.Range(6, 20);
        //2 cells will be walls;
        int floors = Random.Range(1, 5);
        int height = roomHeight * floors + 1;
        //generate background
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                Vector3Int p = new Vector3Int(i, j, 0);
                background.SetTile(p, this.background);
                if(i == 3)
                {
                    if(j == 0)
                    {
                        continue;
                    }
                    if(j == 2 || j % 7 == 0)
                    {
                        Vector3 pp = regionGrid.GetComponent<Grid>().CellToWorld(p);
                        pp.x += offsetX;
                        GameObject obj = Instantiate(enemy);
                        obj.transform.position = pp;
                    }
                }
            }
        }
        //gen solid
        //gen first floor
        for(int i = 0; i < width; i++)
        {
            Vector3Int p = new Vector3Int(i, 0, 0);
            solid.SetTile(p, wall);
            p = new Vector3Int(i, height - 1, 0);
            solid.SetTile(p, wall);
        }
        for(int j = 3; j < height; j++)
        {
            Vector3Int p = new Vector3Int(0, j, 0);
            solid.SetTile(p, wall);
            p = new Vector3Int(width - 1, j, 0);
            solid.SetTile(p, wall);
        }
        //gen rooms
        for(int floor = 1; floor <= floors; floor++)
        {
            int xDeco = Random.Range(3 + 1, width - 2 + 1);
            int yDeco = Random.Range((floor - 1) * roomHeight + 2, floor * roomHeight - 1 + 1);
            Debug.Log(((floor - 1) * roomHeight + 2) + "<=" + yDeco + "<=" + (floor * roomHeight - 1));
            Vector3Int pDeco = new Vector3Int(xDeco, yDeco, 0);
            decoration.SetTile(pDeco, deco);
            for (int i = 3; i < width - 1; i++)
            {
                Vector3Int p = new Vector3Int(i, floor * roomHeight, 0);
                solid.SetTile(p, wall);
            }
        }
        //
        offsetX += solid.size.x;
    }
}
