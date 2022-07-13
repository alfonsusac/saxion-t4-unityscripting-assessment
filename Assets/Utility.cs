using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public static class a
    {
    
        public static void FindObject(this MonoBehaviour g, ref Transform t, string n)
        {
            if (t == null) t = g.transform.Find(n);
            if (t == null) Debug.LogError(n + " not found");
        }

        public static T FindComponent<T>(this MonoBehaviour g)
        {
            T c = g.GetComponent<T>();
            if (c == null) Debug.LogError(typeof(T) + " of " + g.name + " is not found!");
            return c;
        }

        public static float GroundedRayCastLenght = 0.2f;
        public static (GameObject, bool) CheckGrounded(this MonoBehaviour g, Vector3 origin, Vector3 direction, bool debug = true)
        {
            RaycastHit hitInfo;
            if(debug) 
                Debug.DrawRay(origin, direction * GroundedRayCastLenght);
            if (Physics.Raycast(origin, direction, out hitInfo, GroundedRayCastLenght))
                return (hitInfo.transform.gameObject, true);
            else
                return (null, false);
        }

        public static float GetMaxAxis(float a, float b)
        {
            return Mathf.Max(Mathf.Abs(a), Mathf.Abs(b));
        }
            
        public static GameObject FindGameObjectinGame(string name)
        {
            return GameObject.Find(name);
        }
    }
}

