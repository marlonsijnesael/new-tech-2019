using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingManager : DefaultTrackableBehaviourPlaceholder
{
    public Dictionary<string, PokemonBase> pokeDict = new Dictionary<string, PokemonBase>();

    public PokemonBase[] Pokemons;
    public GameObject currentPokemon;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;

    public static TestingManager _Instance;

    private void Awake()
    {
        _Instance = this;
    }

    private void Start()
    {
        //TestingTrackable.OnTrackedCard += UpdateAugmentedObjectTrackingFound;

        meshRenderer = currentPokemon.GetComponent<MeshRenderer>();
        meshFilter = currentPokemon.GetComponent<MeshFilter>();
        currentPokemon.SetActive(false);

        for (int i = 0; i < Pokemons.Length; i++)
        {
            pokeDict.Add(Pokemons[i].Name, Pokemons[i]);
        }
    }

    private void Update()
    {
    }
    public void UpdateAugmentedObjectTrackingFound(string _pokemonName, Transform _transform)
    {
        print("working");
        if (pokeDict.ContainsKey(_pokemonName))
        {
            ChangePokemon(pokeDict[_pokemonName]);
            UpdatePokemonLocation(_transform);
        }

        else
        {
        }
    }

    public void UpdateAugmentedObjectTrackingLost()
    {
        currentPokemon.transform.SetParent(null);
    }


    private void UpdatePokemonLocation(Transform _transform)
    {
        currentPokemon.SetActive(true);
        currentPokemon.transform.position = _transform.position;
        currentPokemon.transform.SetParent(_transform);
    }

    private void ChangePokemon(PokemonBase _pokemonBase)
    {
        meshFilter.mesh = _pokemonBase.pokemonModel;
        meshRenderer.materials = _pokemonBase.textures;
    }
}
