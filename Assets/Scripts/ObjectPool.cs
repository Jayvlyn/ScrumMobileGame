using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> objects;
    public GameObject objectPrefab;
	public int activeCount;

	private void Start()
	{
		DeactivateAll();
	}

	public void DeactivateAll()
	{
		foreach (GameObject obj in objects)
		{
			obj.SetActive(false);
		}
		activeCount = 0;
	}

	public GameObject ActivateObject()
	{
		GameObject foundGo = null;
		foreach (GameObject obj in objects)
		{
			if(!obj.activeSelf)
			{
				foundGo = obj;
				obj.SetActive(true);
				break;
			}
		}
		if (foundGo == null)
		{
			foundGo = Instantiate(objectPrefab, this.transform);
			objects.Add(foundGo);
		}
		activeCount++;
		return foundGo;
	}

	public void DeactivateObject(GameObject go)
	{
		if(objects.Contains(go))
		{
			go.SetActive(false);
			activeCount--;
		}
	}

	public void SetObjectPosition(GameObject go, Vector3 pos)
	{
		go.transform.position = pos;
	}

	public bool NoneActive()
	{
		return activeCount == 0;
	}
}
