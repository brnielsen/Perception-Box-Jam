using System;
using System.Collections.Generic;
using UnityEngine;

public class PercievedObjectHOlder : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private GameObject ImageObjectPrefab;

    [SerializeField] private Vector3 _imageObjectOffset;
    [SerializeField] private float _rotationOffset = 10f;

    private List<GameObject> _imageObjectList = new List<GameObject>();


    void Start()
    {
        PickUpObject.OnPickUp += PickupObject_OnPickUp;
    }

    private void PickupObject_OnPickUp(object sender, PickUpObject.OnPickUpEventArgs e)
    {
        GameObject imageObject = Instantiate(ImageObjectPrefab, transform);
        imageObject.transform.localPosition = Vector3.zero;
        imageObject.transform.rotation = Quaternion.identity;
        _imageObjectList.Add(imageObject);
        int index = _imageObjectList.IndexOf(imageObject);

        ImageObjectWorldSpace imageObjectWorldSpace = imageObject.GetComponent<ImageObjectWorldSpace>();
        imageObjectWorldSpace.SetSprite(e.sprite);

        if (index > 0)
        {
            Transform previousImageObject = _imageObjectList[index - 1].transform;
            imageObject.transform.eulerAngles = new Vector3(0, previousImageObject.eulerAngles.y + _rotationOffset,0 );
        }

    }
}
