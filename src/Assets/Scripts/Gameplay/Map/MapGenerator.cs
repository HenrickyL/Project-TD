using UnityEngine;
using System.Collections;
using System.Drawing;
//sealed 
public static class MapGenerator 
{
    private static GameBoard _board = default;
    private static Vector2Int _size;


    public static IEnumerator GenerateMap(GameBoard board, Vector2Int size)
    {
        _board = board;
        _size = size;

        yield return GenerateTopology();
        yield return PlaceObstacles();
        yield return DecorateEnvironment();
    }

    // Método abstrato para gerar a topologia do mapa
    private static IEnumerator GenerateTopology() {
        Debug.Log("Generating Topology...");
        yield return _board.Initialize(_size);

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
