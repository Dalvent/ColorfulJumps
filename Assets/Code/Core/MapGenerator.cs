using System.Collections;
using System.Collections.Generic;
using Code.Core;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private TileMapGeneratorSource _source;
    [SerializeField] private Tilemap _platformTilemap;
    [SerializeField] private Tilemap _spikeTilemap;
    [SerializeField] private int _maxHeight;

    [SerializeField] private List<BaseLocationGenerator> _locationGenerators;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var locationGenerator in _locationGenerators)
        {
            locationGenerator.Init(_source, _platformTilemap, _spikeTilemap, _maxHeight);
            locationGenerator.Generate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
