using UnityEngine;
using System.Collections;
//sealed 
public static class MapGenerator 
{
    public static int mapWidth = 10;
    public static int mapHeight = 10;

    public static void Generate()
    {
        Debug.Log("Generating map...");
    }

    public static IEnumerator GenerateMap()
    {
        yield return GenerateTopology();
        yield return PlaceObstacles();
        yield return DecorateEnvironment();
    }

    // Método abstrato para gerar a topologia do mapa
    private static IEnumerator GenerateTopology() {
        Debug.Log("Generating Topology...");
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
