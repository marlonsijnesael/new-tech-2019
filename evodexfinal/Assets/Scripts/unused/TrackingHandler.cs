using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingHandler : DefaultTrackableEventHandler { 

    [SerializeField] private GameObject model;
    public string pokemon;
    public GameObject trackableObject;
    public UnityEngine.UI.Text text;

    public delegate void ControllerCallback(string _pokemonName, Transform _transform);
    public static event ControllerCallback OnTrackedCard;

    protected override void Start()
    {
        //trackableObject = Instantiate(model);
        //trackableObject.GetComponent<Renderer>().enabled = false;
    }


    public void Work()
    {

    }

    private void Update()
    {
        //if (Input.touchCount > 0)
        //{
        //    OnTrackingFound();
        //}
        
    }

    protected override void OnTrackingFound()
    {
        //trackableObject.GetComponent<Renderer>().enabled = true;
        //trackableObject.transform.position = transform.position;
        //trackableObject.transform.SetParent(transform);
        OnTrackedCard(pokemon, transform);
    }

    protected override void OnTrackingLost()
    {
        //Debug.Log("not tracking");
        //trackableObject.GetComponent<Renderer>().enabled = false;
        //trackableObject.transform.SetParent(null);
        text.text = "not tracking";
    }
}
