using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Core
{
    public class StarLineLocationGenerator : BaseLocationGenerator
    {
        [Header("Core")]
        [SerializeField] private int _minLineCount;
        [SerializeField] private int _maxLineCount;

        [Header("Platform generation")]
        [Min(1)]
        [SerializeField] private int _minPlatformLength;
        [Min(0)]
        [SerializeField] private int _maxPlatformLength;
        [Min(0)]
        [SerializeField] private int _minNextPlatformOffsetX;
        [Min(0)]
        [SerializeField] private int _maxNextPlatformOffsetX;
        [Min(0)]
        [SerializeField] private int _minNextPlatformBottomOffsetY;
        [Min(0)]
        [SerializeField] private int _maxNextPlatformBottomOffsetY;
        [Min(0)]
        [SerializeField] private int _minNextPlatformTopOffsetY;
        [Min(0)]
        [SerializeField] private int _maxNextPlatformTopOffsetY;
        [Range(0, 1)]
        [SerializeField] private float _nextPlatformUpperProbability;
        
        [Header("Platform spikes")]
        [Range(0, 1)]
        [SerializeField] private float _leftSpikeSpawnProbability;
        [Range(0, 1)]
        [SerializeField] private float _rightSpikeSpawnProbability;
        [Range(0, 1)]
        [SerializeField] private float _bottomSpikeSpawnProbability;
        [Range(0, 1)]
        [SerializeField] private float _topSpikeSpawnProbability;
        [Range(0, 1)]
        [SerializeField] private float _maxTopFilledSpikePersent;
        [Range(0, 1)]
        [SerializeField] private float _maxBottomFilledSpikePersent;


        private List<PlatformInfo> _generatedPlatformsInfo = new List<PlatformInfo>();

        public override void Generate()
        {
            GenerateLines(GenerateLineCount());
            GenerateSpikes();
        }

        private void GenerateSpikes()
        {
            foreach (var generatedPlatform in _generatedPlatformsInfo)
            {
                if (GenerateBoolByProbability(_leftSpikeSpawnProbability))
                    SpikeTilemap.SetTile((Vector3Int) (generatedPlatform.StartPosition + Vector2Int.left), Source.LeftSpike);
                
                if (GenerateBoolByProbability(_rightSpikeSpawnProbability))
                    SpikeTilemap.SetTile((Vector3Int) (generatedPlatform.StartPosition + Vector2Int.right * generatedPlatform.Lenght), Source.RightSpike);

                int maxTopSpikeCount = (int)Mathf.Floor(_maxTopFilledSpikePersent * generatedPlatform.Lenght);
                int generatedTopSpikesCount = 0;
                for (int i = 0; i < generatedPlatform.Lenght; i++)
                {
                    if (!GenerateBoolByProbability(_topSpikeSpawnProbability)) 
                        continue;
                    
                    var spikePosition = new Vector2Int(generatedPlatform.StartPosition.x + i, generatedPlatform.StartPosition.y + 1);
                    SpikeTilemap.SetTile((Vector3Int) spikePosition, Source.TopSpike);
                        
                    generatedTopSpikesCount++;
                    if(generatedTopSpikesCount >= maxTopSpikeCount)
                        break;
                }
                
                int maxBottomSpikeCount = (int)Mathf.Floor(_maxBottomFilledSpikePersent * generatedPlatform.Lenght);
                int generatedBottomSpikesCount = 0;
                for (int i = 0; i < generatedPlatform.Lenght; i++)
                {
                    if (!GenerateBoolByProbability(_bottomSpikeSpawnProbability)) 
                        continue;
                    
                    var spikePosition = new Vector2Int(generatedPlatform.StartPosition.x + i, generatedPlatform.StartPosition.y - 1);
                    SpikeTilemap.SetTile((Vector3Int) spikePosition, Source.BottomSpike);
                        
                    generatedBottomSpikesCount++;
                    if(generatedBottomSpikesCount >= maxBottomSpikeCount)
                        break;
                }
            }
        }
        
        private void GenerateLines(int lineCount)
        {
            var lastEndPosition = Vector2Int.zero;
            for (int i = 0; i < lineCount; i++)
            {
                var currentStartPosition = new Vector2Int(GenerateNextLineStartPositionX(lastEndPosition.x), GenerateNextLineStartPositionY(lastEndPosition.y));
                var lineLength = GenerateLineLenght();
                _generatedPlatformsInfo.Add(new PlatformInfo() 
                { 
                    StartPosition = currentStartPosition,
                    Lenght = lineLength
                });
                
                GenerateLine(currentStartPosition, lineLength);
                lastEndPosition = currentStartPosition + new Vector2Int(lineLength - 1, 0);
            }
        }

        private int GenerateLineLenght()
        {
            return Random.Range(_minPlatformLength, _maxPlatformLength);
        }


        private void GenerateLine(Vector2Int startPosition, int length)
        {
            if (length == 0)
                return;

            if (length == 1)
            {
                PlatformTilemap.SetTile((Vector3Int)startPosition, Source.FullBorderedPlatform);
                return;
            }

            PlatformTilemap.SetTile((Vector3Int)startPosition, Source.RightTopBottomBorderedPlatform);
            PlatformTilemap.SetTile( (Vector3Int)(startPosition + new Vector2Int(length - 1, 0)), Source.LeftTopBottomBorderedPlatform);

            for (var x = startPosition.x + 1; x < startPosition.x + length - 1; x++)
            {
                PlatformTilemap.SetTile(new Vector3Int(x, startPosition.y, 0), Source.TopBottomBorderedPlatform);
            }
        }

        private int GenerateLineCount()
        {
            return Random.Range(_minLineCount, _maxLineCount);
        }

        private int GenerateNextLineStartPositionX(int lastLinePositionX)
        {
            return Random.Range(lastLinePositionX + _minNextPlatformOffsetX, lastLinePositionX + _maxNextPlatformOffsetX);
        }

        private int GenerateNextLineStartPositionY(int lastLinePositionY)
        {
            var offsetVector = GenerateBoolByProbability(_nextPlatformUpperProbability)
                ? Random.Range(_minNextPlatformTopOffsetY, _maxNextPlatformTopOffsetY)
                : -1 * Random.Range(_minNextPlatformBottomOffsetY, _maxNextPlatformBottomOffsetY);

            var resultPositionY = lastLinePositionY + offsetVector;

            resultPositionY = resultPositionY < 0 ? -resultPositionY : resultPositionY; 
            resultPositionY = resultPositionY > MaxHeight ? MaxHeight * 2 - resultPositionY : resultPositionY;

            if (lastLinePositionY == resultPositionY)
                return GenerateNextLineStartPositionY(lastLinePositionY);
            
            return resultPositionY;
        }

        private bool GenerateBoolByProbability(float probabilityPercent)
        {
            return probabilityPercent > Random.Range(0.0f, 1.0f);
        }
    }
}