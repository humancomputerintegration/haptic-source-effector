// 
// demo.cs
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

public class demo : MonoBehaviour
{
	public WeaponCtrl Reaper;
	
	public Texture EnableFocusImg;
	public Texture DisableFocusImg;
	public Texture ShootImg;
	public Texture ReloadImg;
	
	// Update is called once per frame
	void OnGUI()
	{
		var aux1 = new Rect(500, 555, 120, 75);
		var aux2 = new Rect(630, 555, 120, 75);
		var aux3 = new Rect(470, 585, 250, 50);
		
		if (!Reaper.IsFocusEnabled() && GUI.Button(aux1, EnableFocusImg, GUIStyle.none))
		{
			Reaper.EnableFocus();
		}
		if (Reaper.IsFocusEnabled() && GUI.Button(aux1, DisableFocusImg, GUIStyle.none))
		{
			Reaper.DisableFocus();
		}
		if (Reaper.CurrentAmunition > 0 && GUI.Button(aux2, ShootImg, GUIStyle.none))
		{
			Reaper.DoShoot();
		}
		if (Reaper.CurrentAmunition <= 0 && GUI.Button(aux2, ReloadImg, GUIStyle.none))
		{
			Reaper.Reload();
		}
		GUI.Label(aux3, string.Format("Current Amunition {0}/{1}", Reaper.CurrentAmunition, Reaper.ReloadAmount));
	}
}
