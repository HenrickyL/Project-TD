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

    // M�todo abstrato para gerar a topologia do mapa
    private static IEnumerator GenerateTopology() {
        Debug.Log("Generating Topology...");
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
