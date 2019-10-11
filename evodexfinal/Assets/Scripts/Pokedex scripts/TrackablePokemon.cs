using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackablePokemon : MonoBehaviour, ITrackableEventHandler
{
    private Pokemon pokemon;

    [SerializeField] private string pokemonName;
    [SerializeField] private UnityEngine.UI.Text text;
    [SerializeField] private Transform transformTarget;
    
    #region PROTECTED_MEMBER_VARIABLES
    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;
    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS
    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        pokemon = new Pokemon("empty", "pokemon", "object");
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS
    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;
        text.text = newStatus.ToString();
        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName +
                  " " + mTrackableBehaviour.CurrentStatus +
                  " -- " + mTrackableBehaviour.CurrentStatusInfo);

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    protected virtual void OnTrackingFound()
    {
        if (!pokemon.isLoaded)
        {
            pokemon = PokemonManager._Instance.SpawnObject(pokemonName, transform);
            pokemon.EnableInstance();
            return;
        }

        PokemonManager._Instance.RequestPokemon(pokemon);
        pokemon.EnableInstance();
    }

    protected virtual void OnTrackingLost()
    {
        if (pokemon.isLoaded)
        {
            pokemon.DisableInstace();
        }
    }
}
#endregion // PROTECTED_METHODS
