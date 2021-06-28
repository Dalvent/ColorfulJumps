using Code.Core;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class BaseLocationGenerator : MonoBehaviour
{
    protected Tilemap PlatformTilemap { get; private set; }
    protected Tilemap SpikeTilemap { get; private set; }
    protected TileMapGeneratorSource Source { get; private set; }
    protected int MaxHeight { get; private set; }
    
    public void Init(TileMapGeneratorSource source, Tilemap platformTilemap, Tilemap spikeTilemap, int maxHeight)
    {
        Source = source;
        PlatformTilemap = platformTilemap;
        SpikeTilemap = spikeTilemap;
        MaxHeight = maxHeight;
    }

    public abstract void Generate();
}