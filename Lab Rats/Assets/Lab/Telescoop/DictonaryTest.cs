using System.Collections.Generic;
using UnityEngine;

public class DictonaryTest : MonoBehaviour
{
    public List<GameObject> gameObjects = new List<GameObject>();

    private Dictionary<string, GameObject> gameObjectDictionary;

    private void Start()
    {
        BuildDictionary();
    }

    private void BuildDictionary()
    {
        // Create a dictionary to store GameObjects by their names
        gameObjectDictionary = new Dictionary<string, GameObject>();

        foreach (GameObject obj in gameObjects)
        {
            // Assuming the names of the GameObjects represent numbers
            string objectName = obj.name;

            // Add to the dictionary
            if (!gameObjectDictionary.ContainsKey(objectName))
            {
                gameObjectDictionary.Add(objectName, obj);
            }
            else
            {
                // Handle duplicate names if needed
                Debug.LogWarning("Duplicate name found: " + objectName);
            }
        }
    }

    // Function to search for a value whenever you want
    public void SearchValue(string lookForNumber)
    {
        // Check if the number is in the dictionary
        if (gameObjectDictionary.TryGetValue(lookForNumber, out GameObject foundObject))
        {
            Debug.Log("Found the GameObject with the name: " + lookForNumber);
        }
        else
        {
            Debug.Log("GameObject with the name not found: " + lookForNumber);
        }
    }
}
