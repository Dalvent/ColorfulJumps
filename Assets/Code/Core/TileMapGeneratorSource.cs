using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Code.Core
{
    [Serializable]
    public class TileMapGeneratorSource
    {
        // Platforms.
        [SerializeField] private TileBase _platform;
        [SerializeField] private TileBase _fullBorderedPlatform;
        [SerializeField] private TileBase _rightLeftTopBorderedPlatform;
        [SerializeField] private TileBase _rightLeftBottomBorderedPlatform;
        [SerializeField] private TileBase _rightTopBottomBorderedPlatform;
        [SerializeField] private TileBase _rightLeftBorderedPlatform;
        [SerializeField] private TileBase _rightTopBorderedPlatform;
        [SerializeField] private TileBase _rightBottomBorderedPlatform;
        [SerializeField] private TileBase _rightBorderedPlatform;
        [SerializeField] private TileBase _leftTopBottomBorderedPlatform;
        [SerializeField] private TileBase _leftTopBorderedPlatform;
        [SerializeField] private TileBase _leftBottomBorderedPlatform;
        [SerializeField] private TileBase _leftBorderedPlatform;
        [SerializeField] private TileBase _topBottomBorderedPlatform;
        [SerializeField] private TileBase _topBorderedPlatform;
        [SerializeField] private TileBase _bottomBorderedPlatform;

        // Spikes.
        [SerializeField] private TileBase _topSpike;
        [SerializeField] private TileBase _bottomSpike;
        [SerializeField] private TileBase _leftSpike;
        [SerializeField] private TileBase _rightSpike;
        [SerializeField] private TileBase _starSpike;
        
        public TileBase Platform => _platform;

        public TileBase FullBorderedPlatform => _fullBorderedPlatform;

        public TileBase RightLeftTopBorderedPlatform => _rightLeftTopBorderedPlatform;

        public TileBase RightLeftBottomBorderedPlatform => _rightLeftBottomBorderedPlatform;

        public TileBase RightTopBottomBorderedPlatform => _rightTopBottomBorderedPlatform;

        public TileBase RightLeftBorderedPlatform => _rightLeftBorderedPlatform;

        public TileBase RightTopBorderedPlatform => _rightTopBorderedPlatform;

        public TileBase RightBottomBorderedPlatform => _rightBottomBorderedPlatform;

        public TileBase RightBorderedPlatform => _rightBorderedPlatform;

        public TileBase LeftTopBottomBorderedPlatform => _leftTopBottomBorderedPlatform;

        public TileBase LeftTopBorderedPlatform => _leftTopBorderedPlatform;

        public TileBase LeftBottomBorderedPlatform => _leftBottomBorderedPlatform;

        public TileBase LeftBorderedPlatform => _leftBorderedPlatform;

        public TileBase TopBottomBorderedPlatform => _topBottomBorderedPlatform;

        public TileBase TopBorderedPlatform => _topBorderedPlatform;

        public TileBase BottomBorderedPlatform => _bottomBorderedPlatform;

        public TileBase TopSpike => _topSpike;

        public TileBase BottomSpike => _bottomSpike;

        public TileBase LeftSpike => _leftSpike;

        public TileBase RightSpike => _rightSpike;

        public TileBase StarSpike => _starSpike;
    }
}