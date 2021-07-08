using Function = System.Action;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Utils {
    /// <summary>
    /// a timer function that is better than the ugly if statements
    /// </summary>
    /// <param name="time">delay time in milliseconds</param>
    /// <param name="function">the function to execute after delay ends</param>
    public static async void Timeout(int time, Function function) {
        await Task.Delay(time);
        function();
    }
}
