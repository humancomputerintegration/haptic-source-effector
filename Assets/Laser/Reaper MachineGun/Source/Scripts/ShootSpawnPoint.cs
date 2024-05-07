// 
// ShootSpawnPoint.cs
//  
// Author:
//       Jose A. Milan <jose@jamilan.net>
// 
// Copyright (c) 2013 Jose A. Milan - 2013
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using UnityEngine;
using System.Collections;

// Mark the owner as a Shoot Spawn Point
public class ShootSpawnPoint : MonoBehaviour
{
	// How fast we can shoot again
	public float ShootDelay = 1f;
	
	private CartridgeSpawnPoint _cartridgeSpawnPoint;
	public GameObject shootPrefab;
	private bool _doShoot;
	private float _lastShootTime;
	
	// Setup the required variables
	// public void Setup(CartridgeSpawnPoint cartridgeSpawnPoint, GameObject shootPrefab)
	// {
	// 	_cartridgeSpawnPoint = cartridgeSpawnPoint;
	// 	_shootPrefab = shootPrefab;
	// }
	
	// Call this to indicate we need to shoot
	public void DoShoot()
	{
		_doShoot = true;
	}
	
	void Start()
	{
		_doShoot = false;
	}
	
	void LateUpdate()
	{
		//_doShoot = true;
		if (shootPrefab == null)
		{
			//Debug.LogError("Please call Setup on initialization");
			return;
		}
		_lastShootTime += Time.deltaTime;
		// Check if we can shoot again using ShootDelay as cooldown
		if (_doShoot)// && _lastShootTime > ShootDelay)
		{
			_doShoot = false;
			//_lastShootTime -= ShootDelay;
			var go = Instantiate(shootPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
			// if (go != null && _cartridgeSpawnPoint != null)
			// {
			// 	_cartridgeSpawnPoint.Spawn();
			// }
		}
	}
}
