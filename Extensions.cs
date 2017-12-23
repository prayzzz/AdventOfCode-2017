namespace AdventOfCode2017
{
    public static class Extensions
    {
        public static void ShiftRight<T>(this T[] arr)
        {
            var len = arr.Length;
            var tmp = arr[len - 1]; // save last element value
            for (var i = len - 1; i > 0; i--) // assign value of the previous element
            {
                arr[i] = arr[i - 1];
            }
            arr[0] = tmp; // last to first.
        }

        public static void ShiftRight<T>(this T[] arr, int times)
        {
            for (var i = 0; i < times; i++)
            {
                ShiftRight(arr);
            }
        }
    }
}