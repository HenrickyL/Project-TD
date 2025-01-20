using UnityEngine;
using System.Collections;
using Perikan.Gameplay.Factory;
using Perikan.Gameplay.Map;

namespace Perikan.Gameplay.Generator { 
    public static class MapGenerator 
    {
        private static GameBoard _board = default;
        private static Vector2Int _size;
        private static GameTileContentFactory _tileContentFactory;


        public static IEnumerator GenerateMap(GameBoard board, GameTileContentFactory contentFactory,  Vector2Int size)
        {
            _board = board;
            _size = size;
            _board.SetEnable(true);
            _tileContentFactory = contentFactory;

            yield return GenerateTopology();
            yield return PlaceObstacles();
            yield return DecorateEnvironment();
        }

        public static void Generate(GameBoard board, GameTileContentFactory contentFactory, Vector2Int size)
        {
            _board = board;
            _size = size;
            _tileContentFactory = contentFactory;

            _board.SetEnable(true);
            _board.Initialize(_size, _tileContentFactory);
            _board.ShowGrid = true;
        }

        // M�todo abstrato para gerar a topologia do mapa
        private static IEnumerator GenerateTopology() {
            Debug.Log("Generating Topology...");
            _board.Initialize(_size, _tileContentFactory);
            _board.ShowGrid = true;
            yield return null;
        }

        // M�todo abstrato para posicionar obst�culos no mapa
        private static IEnumerator PlaceObstacles() {
            Debug.Log("Generating PlaceObstacles...");

            yield return null;
        }

        // M�todo abstrato para adicionar decora��es no ambiente
        private static IEnumerator DecorateEnvironment() {
            Debug.Log("Generating DecorateEnvironment...");
            yield return null;
        }
    }
}