
using UnityEngine;

[System.Serializable]
public class Pokemon
{
    public string pokemonName;
    public string pokemonDescription;
    public string prefabName;
    public bool isLoaded = false;
    private GameObject instance;

    //construnctor for pokemon class
    public Pokemon(string _name, string _description, string _prafabName)
    {
        pokemonName = _name;
        pokemonDescription = _description;
        prefabName = _prafabName;
    }

    //link the class and the sceneobject
    public void Init(GameObject _instance)
    {
        instance = _instance;
        isLoaded = true;
    }

    //OnEnable() like function
    public void EnableInstance()
    {
        instance.SetActive(true);
    }

    //OnDisable() like function
    public void DisableInstace()
    {
        instance.SetActive(false);
    }
}
