using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneGenerator : MonoBehaviour
{
    #region Properties

    [SerializeField] private List<GameObject> _dangerZones;
    [SerializeField] private List<GameObject> _triggerZones;
    [SerializeField] private List<GameObject> _toActivateZones;
    [SerializeField] private GameObject _endZone;

    [SerializeField] private List<Color> _triggerColors;

    private int _zonesCount = 3;
    private Vector3 _placePoint;
    private List<GameObject> _chosenZones = new List<GameObject>();
    private List<GameObject> _shuffledZones = new List<GameObject>();
    private List<GameObject> _generatedZones = new List<GameObject>();
    private List<GameObject> _chosenTriggerZones = new List<GameObject>();
    private List<GameObject> _chosenToActivateZones = new List<GameObject>();

    #endregion Private methods

    #region Private methods

    void Start()
    {

        _placePoint = transform.position;

        _chosenZones.Add(RandomZone(_dangerZones));
        _chosenZones.Add(RandomZone(_triggerZones));
        _chosenZones.Add(RandomZone(_toActivateZones));
        _shuffledZones = ListExtension.Shuffle(_chosenZones);

        GenerateZones();
        SortZoneTypes();

        _chosenTriggerZones = ListExtension.Shuffle(_chosenTriggerZones);
        _chosenToActivateZones = ListExtension.Shuffle(_chosenToActivateZones);
        MatchTriggerActivate();
    }

    void SortZoneTypes()
    {
        foreach (GameObject zone in _generatedZones)
        {
            Zone.Type zoneType = zone.GetComponent<Zone>().GetZoneType();

            if (zoneType == Zone.Type.Trigger)
            {
                _chosenTriggerZones.Add(zone);
            } 
            else if (zoneType == Zone.Type.ToActivate)
            {
                _chosenToActivateZones.Add(zone);
            }
        }
    }

    void GenerateZones()
    {
        foreach (GameObject zone in _shuffledZones)
        {
            GameObject zoneGenerated = Instantiate(zone, _placePoint, Quaternion.identity, transform);
            _placePoint = zoneGenerated.GetComponent<Zone>().EndPoint.position;
            _generatedZones.Add(zoneGenerated);
        }
    }

    void MatchTriggerActivate()
    {
        //for(int i = 0; i < _chosenTriggerZones.Count - 1; i++)
        //{
            ToActivateZone toActivateZone = _chosenToActivateZones[0].GetComponent<ToActivateZone>();
            
            toActivateZone.LinkedTrigger = _chosenTriggerZones[0];
            toActivateZone.Colour = _triggerColors[0];
            _chosenTriggerZones[0].GetComponent<TriggerZone>().Colour = _triggerColors[0];
        //}
    }

    void AddEndZone()
    {
        GameObject endZone = Instantiate(_endZone, _placePoint, Quaternion.identity, transform);
    }

    GameObject RandomZone(List<GameObject> zones)
    {
        int index = Random.Range(0, zones.Count - 1);
        return zones[index];
    }

    #endregion Private Methods
}

