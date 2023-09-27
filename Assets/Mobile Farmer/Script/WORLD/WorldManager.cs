using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class WorldManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private Transform world;
    Chunk[,] grid;
    [Header("Settings")]
    [SerializeField] private int gridSize;
    [SerializeField] private int gridScale;
    [Header("Data")]
    private WorldData worldData;
    private string dataPatch;
    private bool shouldSave;
    private void Awake()
    {
        Chunk.onUnlocked += ChunkUnlockedCallback;
        Chunk.onPriceChanged += ChunkPriceCallback;
    }

    void Start()
    {
       dataPatch = Application.persistentDataPath + "/WorldData.txt";
        LoadWorld();
        Initialize();

        InvokeRepeating("TrySaveWorld", 1, 1);

    }


    private void OnDestroy()
    {
        Chunk.onUnlocked -= ChunkUnlockedCallback;
        Chunk.onPriceChanged -= ChunkPriceCallback;

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void Initialize()
    {
        for (int i = 0; i < world.childCount; i++)
        {
            world.GetChild(i).GetComponent<Chunk>().Initialize(worldData.chunkPrices[i]);
        }

        InitializeGrid();

        UpdateChunkWalls();
        UpgradeGridRenderer();

    }
    private void InitializeGrid()
    {
        grid = new Chunk[gridSize,gridSize];

        for (int i = 0; i < world.childCount; i++)
        {
            Chunk chunk = world.GetChild(i).GetComponent<Chunk>();

            Vector2Int chunkGridPosition= new Vector2Int((int)chunk.transform.position.x/gridScale,
                (int)chunk.transform.position.z/gridScale);

            chunkGridPosition += new Vector2Int(gridSize / 2, gridSize / 2);
            grid[chunkGridPosition.x,chunkGridPosition.y] = chunk;
        }
    }

    private void UpdateChunkWalls()
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                Chunk chunk = grid[x, y];

                if (chunk == null)
                    continue;

                Chunk frontChunk = null;

                if (IsValidGridPosition(x, y + 1))
                    frontChunk = grid[x, y + 1];

                Chunk rightChunk = null;

                if (IsValidGridPosition(x + 1, y))
                    rightChunk = grid[x + 1, y];

                Chunk backChunk = null;

                if (IsValidGridPosition(x, y - 1))
                    backChunk = grid[x, y - 1];

                Chunk leftChunk = null;

                if (IsValidGridPosition(x - 1, y))
                    leftChunk = grid[x - 1, y];

                int configuration = 0;

                if (frontChunk != null && frontChunk.IsUnlocked())
                    configuration = configuration + 1;
                if (rightChunk != null && rightChunk.IsUnlocked())
                    configuration = configuration + 2;
                if (backChunk != null && backChunk.IsUnlocked())
                    configuration = configuration + 4;
                if (leftChunk != null && leftChunk.IsUnlocked())
                    configuration = configuration + 8;

                chunk.UpdateWalls(configuration);

            }
        }
        
    }

    public void UpgradeGridRenderer()
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                Chunk chunk = grid[x, y];

                if (chunk == null)
                    continue;


                if (chunk.IsUnlocked())
                    continue;

                Chunk frontChunk = IsValidGridPosition(x, y + 1) ? grid[x, y + 1] : null;
                Chunk rightChunk = IsValidGridPosition(x + 1, y) ? grid[x, y + 1] : null;
                Chunk backChunk = IsValidGridPosition(x, y - 1) ? grid[x, y + 1] : null;
                Chunk leftChunk = IsValidGridPosition(x - 1, y) ? grid[x, y + 1] : null;

                if (frontChunk != null && frontChunk.IsUnlocked())
                    chunk.DisplayLockedElements();
                else if (rightChunk != null && rightChunk.IsUnlocked())
                    chunk.DisplayLockedElements();
                else if (backChunk != null && backChunk.IsUnlocked())
                    chunk.DisplayLockedElements();
                else if (leftChunk != null && leftChunk.IsUnlocked())
                    chunk.DisplayLockedElements();
            }
        }
    }
    public void TrySaveWorld()
    {
        if (shouldSave)
        {
            SaveWorld();
            shouldSave = false;
        }
    }
    private bool IsValidGridPosition(int x,int y)
    {
        if(x<0||x>=gridSize||y<0||y>=gridSize)
            return false;

        return true;
    }
    public void ChunkPriceCallback()
    {
        shouldSave = true;
    }
    private void ChunkUnlockedCallback()
    {
        SaveWorld();

        UpdateChunkWalls(); 
        UpgradeGridRenderer();
    }

    private void LoadWorld()
    {
        
        string data = "";

        if (!File.Exists(dataPatch))
        {
            FileStream fs = new FileStream(dataPatch, FileMode.Create);
            worldData = new WorldData();

            for (int i = 0; i < world.childCount; i++)            
                worldData.chunkPrices.Add(world.GetChild(i).GetComponent<Chunk>().GetInitialPrices());

            string worldDataString = JsonUtility.ToJson(worldData, true);

            byte[] worldDataBytes = Encoding.UTF8.GetBytes(worldDataString);
            fs.Write(worldDataBytes);
            fs.Close();
        }
        else
        {
            data=File.ReadAllText(dataPatch);
            worldData= JsonUtility.FromJson<WorldData>(data);

            if (worldData.chunkPrices.Count < world.childCount)
                UpdateData();
        }
    }

    private void UpdateData()
    {
        int missingData = world.childCount - worldData.chunkPrices.Count;
        for (int i = 0; i < missingData; i++)
        {
            int chunkIndex = world.childCount - missingData + i;
            int chunkPrice= world.GetChild(chunkIndex).GetComponent<Chunk>().GetInitialPrices();
            worldData.chunkPrices.Add(chunkPrice);
        }
    }

    private void SaveWorld()
    {
        if(worldData.chunkPrices.Count!=world.childCount)
            worldData = new WorldData();

        for (int i = 0; i < world.childCount; i++)
        {
            int chunkCurrentPrices = world.GetChild(i).GetComponent<Chunk>().GetCurrentPrices();


            if (worldData.chunkPrices.Count > i)
                worldData.chunkPrices[i] = chunkCurrentPrices;
            else
                worldData.chunkPrices.Add(chunkCurrentPrices);

        }
        string data = JsonUtility.ToJson(worldData, true);
        File.WriteAllText(dataPatch, data);


    }
}
