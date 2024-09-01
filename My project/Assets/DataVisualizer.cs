using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataVisualizer : MonoBehaviour
{
    public GameObject barPrefab;

    public void VisualizeData(SpreadsheetData data)
    {
        // Ensure barPrefab is assigned in the Unity Inspector
        if (barPrefab == null)
        {
            Debug.LogError("Bar prefab is not assigned.");
            return;
        }

        for (int i = 0; i < data.rows.Count; i++)
        {
            float value;
            if (float.TryParse(data.rows[i].cells[1], out value)) // Assuming the value is in the second column
            {
                GameObject bar = Instantiate(barPrefab, new Vector3(i * 2.0f, value / 2.0f, 0), Quaternion.identity);
                bar.transform.localScale = new Vector3(1, value, 1);
            }
            else
            {
                Debug.LogError("Failed to parse value: " + data.rows[i].cells[1]);
            }
        }
    }
}

