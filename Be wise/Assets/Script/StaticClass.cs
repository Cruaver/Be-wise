﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Static class used to saves the different values between Scenes.
*/

public static class StaticClass {
	public static string[] Questions{ get; set; }
	public static string[] Answers{ get; set; }
	public static bool[] Correct{ get; set; }
	public static int Score{ get; set; }
}
