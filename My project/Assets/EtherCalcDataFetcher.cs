using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

// Ensure DataVisualizer is in the same namespace or imported correctly
public class EtherCalcDataFetcher : MonoBehaviour
{
    // Replace with your actual EtherCalc URL
    private string etherCalcUrl = "https://ethercalc.net/4p864404nemo.json";

    void Start()
    {
        StartCoroutine(FetchData());
    }

    IEnumerator FetchData()
    {
        // Create a UnityWebRequest to get data from EtherCalc
        UnityWebRequest www = UnityWebRequest.Get(etherCalcUrl);
        yield return www.SendWebRequest();

        // Check for network errors
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error fetching data: " + www.error);
        }
        else
        {
            // Success, process the JSON response
            string jsonResponse = www.downloadHandler.text;
            ParseData(jsonResponse);
        }
    }

    void ParseData(string jsonData)
    {
        Debug.Log("Received Data: " + jsonData);  // Log the received data

        try
        {
            SpreadsheetData data = JsonConvert.DeserializeObject<SpreadsheetData>(jsonData);
            if (data != null)
            {
                DataVisualizer visualizer = FindObjectOfType<DataVisualizer>();
                if (visualizer != null)
                {
                    visualizer.VisualizeData(data);
                }
                else
                {
                    Debug.LogWarning("DataVisualizer component not found in the scene.");
                }
            }
            else
            {
                Debug.LogError("Failed to parse JSON data.");
            }
        }
        catch (JsonException ex)
        {
            Debug.LogError("JSON parsing error: " + ex.Message);
        }
    }
}
// Define data structures here or in separate files
[System.Serializable]
public class SpreadsheetData
{
    public List<Row> rows;
}

[System.Serializable]
public class Row
{
    public List<string> cells;
}

