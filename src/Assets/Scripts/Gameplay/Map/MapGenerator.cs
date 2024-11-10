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

    // M�todo abstrato para gerar a topologia do mapa
    private static IEnumerator GenerateTopology() {
        Debug.Log("Generating Topology...");
        yield return _board.Initialize(_size);

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
