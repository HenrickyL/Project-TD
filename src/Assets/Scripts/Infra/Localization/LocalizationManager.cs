using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour, ILocalizationManager
{
    private Dictionary<string, string> localizedText;
    private string currentLanguage = "pt";  // Idioma padrão

    // Carrega o texto localizado de uma categoria específica (Menus, Dialogues, etc.)
    public void LoadLocalizedText(string category)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, currentLanguage, category + ".json");

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
            localizedText = new Dictionary<string, string>();

            foreach (var item in loadedData.items)
            {
                localizedText.Add(item.key, item.value);
            }
        }
        else
        {
            Debug.LogError("Arquivo de localização não encontrado para a categoria: " + category);
        }
    }

    // Retorna o valor traduzido associado à chave
    public string GetLocalizedValue(string key)
    {
        if (localizedText.ContainsKey(key))
        {
            return localizedText[key];
        }
        return key; // Retorna a chave original se a tradução não for encontrada
    }

    // Permite alterar o idioma atual
    public void SetLanguage(string languageCode)
    {
        currentLanguage = languageCode;
    }
}

// Estrutura de dados usada para manipular o JSON de localização
[System.Serializable]
public class LocalizationData
{
    public LocalizationItem[] items;
}

[System.Serializable]
public class LocalizationItem
{
    public string key;
    public string value;
}
