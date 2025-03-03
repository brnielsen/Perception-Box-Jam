using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Adroit.Utilities
{
    public static class Helpers
    {

        /// <summary>
        ///UI Elements Instatiated at runtime can have issues with Scaling.
        ///This function helps avoid that.
        /// </summary>

        public static GameObject InstantiateUIElement(GameObject go, Transform parentObject)
        {
            GameObject gameObject = GameObject.Instantiate(go);
            gameObject.transform.SetParent(parentObject, false);
            return gameObject;
        }

        private static Camera _camera;
        public static Camera Camera
        {
            get
            {
                if (_camera == null) _camera = Camera.main;
                return _camera;
            }
        }

        /// <summary>
        /// Thank you Taro Dev :). This returns the world position of a Rect Transform.  
        /// </summary>
        /// <param name="Vector2 of Canvas Element in World Space Coordinates"></param>
        /// <returns></returns>
        public static Vector2 GetWorldPositionOfCanvasElement(RectTransform element)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(element, element.position, Camera, out var result);
            return result;
        }

        private static PointerEventData _eventDataCurrentPosition;
        private static List<RaycastResult> _results;

        /// <summary>
        /// Thank you Taro Dev :). This checks if the cursor is over a UI element. 
        /// </summary>
        /// <returns></returns>
        public static bool IsOverUI()
        {
            if (CheckIfNewInputSystem() == true)
            {
                _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Pointer.current.position.ReadValue() };
            }
            else
            {
                _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
            }
            _results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(_eventDataCurrentPosition, _results);
            return _results.Count > 0;
        }

        /// <summary>
        /// Thank you Taro Dev :).  This checks to see if a WaitForSeconds of this specific amount of time has been cached before.
        /// This helps avoid making the garbage collector work everytime you need a WaitForSeconds. 
        /// </summary>
        private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();
        public static WaitForSeconds GetWait(float time)
        {
            if (WaitDictionary.TryGetValue(time, out var wait)) return wait;

            WaitDictionary[time] = new WaitForSeconds(time);
            return WaitDictionary[time];
        }

        public static List<T> ShuffledList<T>(this IList<T> list)
        {
            System.Random random = new System.Random();

            var shuffled = list.OrderBy(_ => random.Next()).ToList();

            return shuffled;
        }

        public static bool CheckIfNewInputSystem()
        {
            try
            {
                System.Type.GetType("UnityEngine.InputSystem.InputAction");
                return true;
            }
            catch (System.TypeLoadException)
            {
                return false;
            }
        }

        public static string GetFirstName(string fullName)
        {           

            string[] nameParts = fullName.Split(' ');

            if(nameParts.Length > 0 )
            {
                string firstName = nameParts[0];
                return firstName;
            }
            else
            {
                return null;
            }

        }

        public static string GetLastName(string fullName)
        {
            string[] nameParts = fullName.Split(' ');

            if (nameParts.Length >= 2)
            {
                // The last name is the last element in the array
                string lastName = nameParts[nameParts.Length - 1];
                return lastName;
            }
            else
            {
                Debug.Log("Invalid full name format.");
                return null;
            }
        }

        public static T GetComponentInChildrenButNotSelf<T>(GameObject parent) where T : Component
        {
            // Get all components of type T in children, including the current GameObject
            T[] components = parent.GetComponentsInChildren<T>();

            // Loop through the components and find the first one that is not on the current GameObject
            foreach (T component in components)
            {
                if (component.gameObject != parent)
                {
                    return component;
                }
            }

            // Return null if no component is found in children
            return null;
        }
    }
}




