using UnityEngine;
using System.Collections;
//sealed 
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
    }

    // Método abstrato para gerar a topologia do mapa
    private static IEnumerator GenerateTopology() {
        Debug.Log("Generating Topology...");
       _board.Initialize(_size, _tileContentFactory);

        yield return null;
    }

    // Método abstrato para posicionar obstáculos no mapa
    private static IEnumerator PlaceObstacles() {
        Debug.Log("Generating PlaceObstacles...");

        yield return null;
    }

    // Método abstrato para adicionar decorações no ambiente
    private static IEnumerator DecorateEnvironment() {
        Debug.Log("Generating DecorateEnvironment...");
        yield return null;
    }
}
