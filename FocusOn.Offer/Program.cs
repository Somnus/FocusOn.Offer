using System;

namespace FocusOn.Offer
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = { -1, 1, 1, 1, 1, 1, 1, 1, 1 };
            //int[] repeat = GetRepeatedArray(array);

            //int result1 = SequentialSearch1(array, 4);
            //int result2 = SequentialSearch2(array, 4);
            int result3 = ImterpolatedSearch1(array, 10);
            int result4 = ImterpolatedSearch2(array, 10, 0, array.Length - 1);

            Console.WriteLine("Hello World!");
        }

        #region 数组中重复的数字

        #region 方法1：将已重复和未重复的数字存到不同的数组中，返回已重复数字数组
        /// <summary>
        /// 获取数组中重复的数字
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        static int[] GetRepeatedArray(int[] array)
        {
            int length = array.Length;
            int[] temp = new int[length], repeat = new int[length];
            int tempCount = 0, repeatCount = 0;
            for (int i = 0; i < length; i++)
            {
                bool existInTemp = NumberIsExist(temp, array[i]);
                if (existInTemp)
                {
                    bool existInRepeat = NumberIsExist(repeat, array[i]);
                    if (!existInRepeat)
                        repeat[repeatCount++] = array[i];
                }
                else
                    temp[tempCount++] = array[i];
            }
            int[] result = new int[tempCount];
            for (int i = 0; i < tempCount; i++)
                result[i] = temp[i];
            return result;
        }

        /// <summary>
        /// 判断数字是否存在于数组中
        /// </summary>
        /// <param name="array"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        static bool NumberIsExist(int[] array, int number)
        {
            for (int i = 0; i < array.Length; i++)
                if (array[i] == number)
                    return true;
            return false;
        }
        #endregion

        #endregion

        #region 顺序查找算法:时间复杂度O(n)

        /// <summary>
        /// 顺序查找
        /// </summary>
        /// <param name="array"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int SequentialSearch1(int[] array, int key)
        {
            int i;
            for (i = 0; i < array.Length; i++)
            {
                if (array[i] == key)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// 带哨兵的顺序查找，减少循环中是否达到边界判断
        /// </summary>
        /// <param name="array"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int SequentialSearch2(int[] array, int key)
        {
            if (array.Length <= 0)
                return -1;

            if (array[0] == key)
                return 0;

            int i = array.Length - 1;
            array[0] = key;
            while (array[i] != key)
            {
                i--;
            }
            i = i == 0 ? -1 : i;
            return i;
        }

        #endregion

        #region 有序表二分法查找算法：时间复杂度O(logn)
        /// <summary>
        /// 非递归二分法顺序查找
        /// </summary>
        /// <param name="array"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int BinarySearch1(int[] array, int key)
        {
            int begin, mid, end;
            begin = 0;
            end = array.Length - 1;
            while (begin <= end)
            {
                mid = (begin + end) / 2;
                if (key > array[mid])
                    begin = mid + 1;
                else if (key < array[mid])
                    end = mid - 1;
                else
                    return mid;
            }
            return -1;
        }

        /// <summary>
        /// 递归二分法顺序查找
        /// </summary>
        /// <param name="array"></param>
        /// <param name="key"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int BinarySearch2(int[] array, int key, int begin, int end)
        {
            if (begin > end)
                return -1;

            int mid = (begin + end) / 2;

            if (key == array[mid])
                return mid;
            if (key < array[mid])
                return BinarySearch2(array, key, begin, mid - 1);
            else
                return BinarySearch2(array, key, mid + 1, end);
        }
        #endregion

        #region 有序表插值法查找算法
        /// <summary>
        /// 非递归插值法顺序查找
        /// </summary>
        /// <param name="array"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int ImterpolatedSearch1(int[] array, int key)
        {
            int begin, mid, end;
            begin = 0;
            end = array.Length - 1;
            while ((array[begin] <= key && array[end] >= key) && (array[begin] != array[end]))
            {
                mid = begin + ((key - array[begin]) / (array[end] - array[begin])) * (begin + end);
                if (key > array[mid])
                    begin = mid + 1;
                else if (key < array[mid])
                    end = mid - 1;
                else
                    return mid;
            }
            if (key == array[begin])
                return begin;
            return -1;
        }

        /// <summary>
        /// 递归插值法顺序查找
        /// </summary>
        /// <param name="array"></param>
        /// <param name="key"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int ImterpolatedSearch2(int[] array, int key, int begin, int end)
        {
            if (array[begin] > key || array[end] < key || (array[begin] == array[end] && array[begin] != key))
                return -1;
            if (array[begin] == array[end] && array[begin] == key)
                return begin;

            int mid = begin + ((key - array[begin]) / (array[end] - array[begin])) * (begin + end);
            if (key == array[mid])
                return mid;
            if (key < array[mid])
                return ImterpolatedSearch2(array, key, begin, mid - 1);
            else
                return ImterpolatedSearch2(array, key, mid + 1, end);
        }
        #endregion
    }
}
