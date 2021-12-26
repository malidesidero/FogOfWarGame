using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out FogOfWarCheck fogOfWarCheck))
        {
            switch (fogOfWarCheck.objectType)
            {
                case FogOfWarCheck.ObjectType.Enemy:

                    fogOfWarCheck.DoVisible();

                    break;
                case FogOfWarCheck.ObjectType.Construction:

                    fogOfWarCheck.DoVisible();

                    break;
            }
        }       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out FogOfWarCheck fogOfWarCheck))
        {
            switch (fogOfWarCheck.objectType)
            {
                case FogOfWarCheck.ObjectType.Enemy:

                    fogOfWarCheck.DoInvisible();

                    break;

                case FogOfWarCheck.ObjectType.Construction:

                    //Still visible

                    break;
            }
        }
    }
}
