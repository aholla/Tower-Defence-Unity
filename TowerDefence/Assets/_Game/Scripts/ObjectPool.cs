﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {

	[SerializeField]
	private GameObject prefab;
	
	[SerializeField]
	private int numOfObjects = 10;

	[SerializeField]
	private GameObject optionalParent;

	private List<GameObject> pool;

	//===================================================
	// PUBLIC METHODS
	//===================================================

	/// <summary>
	/// Initializes the pool with the specified number of GameObject derived from the prefab.
	/// </summary>
	public void Init() {
		pool = new List<GameObject>( numOfObjects );
		for( int i = 0; i < numOfObjects; i++ ) {
			AddGameObject();
		}
	}

	/// <summary>
	/// Gets a GameObject from the pool or creates a new one if it is full/empty.
	/// </summary>
	/// <returns></returns>
	public GameObject GetGameObject() {
		for( int i = 0; i < pool.Count; i++ ) {
			GameObject ob = pool[ i ];
			if( !ob.activeSelf ) {
				ob.SetActive( true );
				return ob;
			}
		}

		// if here, then there is not enought to spawn so add another one.
		GameObject additionalGO = AddGameObject();
		additionalGO.SetActive( true );
		return additionalGO;
	}

	/// <summary>
	/// Releases the object back to the pool but it is so simple it is not really needed.
	/// </summary>
	/// <param name="go">The go.</param>
	public void ReleaseObject( GameObject go ) {
		go.SetActive( false );
	}	

	/// <summary>
	/// Releases all the gameObjects - disables them.
	/// </summary>
	public void ReleaseAll() {
		for( int i = 0; i < pool.Count; i++ ) {
			GameObject ob = pool[ i ];
			ob.SetActive( false );
		}
	}

	//===================================================
	// PRIVATE METHODS
	//===================================================

	/// <summary>
	/// Adds a GameObject to pool.
	/// </summary>
	/// <returns></returns>
	private GameObject AddGameObject() {
		//GameObject go = Instantiate( _prefab, Vector3.zero, Quaternion.identity ) as GameObject;
		GameObject go = Instantiate( prefab ) as GameObject;
		if( optionalParent == null ) {
			go.transform.SetParent( this.transform );
		} else {
			go.transform.SetParent( optionalParent.transform, true );
		}
		go.SetActive( false );
		pool.Add( go );
		return go;
	}
}
