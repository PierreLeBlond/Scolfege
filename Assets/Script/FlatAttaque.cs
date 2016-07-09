using UnityEngine;
using System.Collections;

public class FlatAttaque : MonoBehaviour {

	public Flat flatPrefab;
	public Collider2D limit;
	public Info info;

	public SpriteRenderer warning;

	private Flat[] _flats;

	public int safe = 0;

	// Use this for initialization
	void Start () {
		_flats = new Flat[13];
		safe = Random.Range(0, 7);
		info.setNote(safe);
		for(int i = 2; i < 15; ++i)
		{
			if(i%7 != safe)
			{
				_flats[i-2] = Instantiate (flatPrefab) as Flat;
				_flats[i-2].transform.parent = transform;
				_flats[i-2].transform.localPosition = new Vector3(0f, (float)(i)*0.5f - 4f, 0f);
			}
		}

		StartCoroutine(run());
	}

	public IEnumerator run(){
		for(int i = 0; i < 4;i++)
		{
			warning.enabled = true;
			yield return new WaitForSeconds(0.5f);
			warning.enabled = false;
			yield return new WaitForSeconds(0.5f);
		}

		while(!GetComponent<Collider2D>().IsTouching(limit))
		{
			transform.localPosition = transform.localPosition - new Vector3(0.2f, 0f, 0f);
			yield return null;
		}
		Destroy(gameObject);
	}
}
