using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace OutSolTools
{
    public class CoreFunctions : MonoBehaviour
    {
        public static bool CheckChance(float chance) /// from 0 to 1 
        {
            if (chance > 1 || chance < 0) {Debug.LogError($"not valid chance: {chance}"); return false;}
            return (UnityEngine.Random.Range(0f, 1f) < chance);
        }
        public static void BubbleSort<T>(T[] arr, IComparer<T> comparer = null)
        {
            if (null == arr)
                throw new ArgumentNullException(nameof(arr));
            if (null == comparer && typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
                comparer = Comparer<T>.Default;
            else
                throw new ArgumentNullException(
                   nameof(comparer),
                 $"Type {typeof(T)} doesn't have reasonable default comparer.");

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length - 1; j++)
                {
                    if (comparer.Compare(arr[j], arr[j + 1]) > 0)
                    {
                        T temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }
        public static T MaxFromArray<T>(T[] array, IComparer<T> comparer = null) 
        {
            if (null == array)
                throw new ArgumentNullException(nameof(array));
            if (null == comparer && typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
                comparer = Comparer<T>.Default;
            else
                throw new ArgumentNullException(
                   nameof(comparer),
                 $"Type {typeof(T)} doesn't have reasonable default comparer.");
            T max = array[0];
            for (int i = 0; i < array.Length; i++) 
            {
                if (comparer.Compare(array[i], max) > 0) 
                {
                    max = array[i];
                }
            }
            return max;
        } 
        public static T MinFromArray<T>(T[] array, IComparer<T> comparer = null) 
        {
            if (null == array)
                throw new ArgumentNullException(nameof(array));
            if (null == comparer && typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
                comparer = Comparer<T>.Default;
            else
                throw new ArgumentNullException(
                   nameof(comparer),
                 $"Type {typeof(T)} doesn't have reasonable default comparer.");
            T min = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (comparer.Compare(array[i], min) < 0)
                {
                    min = array[i];
                }
            }
            return min;
        }
        public static T MaxFromList<T>(List<T> list, IComparer<T> comparer = null)
        {
            if (null == list)
                throw new ArgumentNullException(nameof(list));
            if (null == comparer && typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
                comparer = Comparer<T>.Default;
            else
                throw new ArgumentNullException(
                   nameof(comparer),
                 $"Type {typeof(T)} doesn't have reasonable default comparer.");
            T max = list[0];
            for (int i = 0; i < list.Count; i++)
            {
                if (comparer.Compare(list[i], max) > 0)
                {
                    max = list[i];
                }
            }
            return max;
        }
        public static T MinFromList<T>(List<T> list, IComparer<T> comparer = null)
        {
            if (null == list)
                throw new ArgumentNullException(nameof(list));
            if (null == comparer && typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
                comparer = Comparer<T>.Default;
            else
                throw new ArgumentNullException(
                   nameof(comparer),
                 $"Type {typeof(T)} doesn't have reasonable default comparer.");
            T min = list[0];
            for (int i = 0; i < list.Count; i++)
            {
                if (comparer.Compare(list[i], min) < 0)
                {
                    min = list[i];
                }
            }
            return min;
        }

    }
    public class DebugTools : MonoBehaviour 
    {
        public static void Array<T>(T[] array, string itsName = "[NameOfArray]")
        {
            string debug = ($"Array: {itsName} Count: {array.Length} \n");
            for (int i = 0; i < array.Length; i++) debug += ($"[{i}] {array[i]} \n");
            Debug.Log(debug);
        }
        public static void List<T>(List<T> list, string itsName = "[NameOfList]")
        {
            string debug = ($"List: {itsName} Count: {list.Count} \n");
            for (int i = 0; i < list.Count; i++) debug += ($"[{i}] {list[i]} \n");
            Debug.Log(debug);
        }
    }
    public class FileHandling 
    {
        protected static string defaultDirectory = ($"{Application.dataPath}/CoreFiles");
        public static string GetDefaultDirectory() {return defaultDirectory;}
        public static void WriteText(string filename, string text, string directory = "default") 
        {
            const string fileExtension = ".txt";
            for (int i = 0; i < filename.Length; i++)
            {
                if (!Char.IsLetterOrDigit(filename[i]))
                {
                    Debug.LogError($"inappropriate filename: {filename} ('a-z' 'A-Z' or '/')");
                    return;
                }
            }
            if (directory == "default") directory = defaultDirectory;
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            string filePath = directory + "/" + filename + fileExtension;
            StreamWriter newFile = new StreamWriter(filePath);
            newFile.Write(text);
            newFile.Close();
        }
        public static string ReadText(string filename, string directory = "default") 
        {
            const string fileExtension = ".txt";
            if (directory == "default") directory = defaultDirectory;
            string filepath = directory + "/" + filename + fileExtension;
            if (!File.Exists(filepath)) return $"BadFilePath : {filepath}";
            StreamReader readFile = new StreamReader(filepath);
            string readString = readFile.ReadToEnd(); 
            readFile.Close();
            return readString;
        }
        public static void DeleteFile(string filename, string fileExtension, string directory = "") 
        {
            /// cannot delete files above default directory
            string filepath;
            if (directory == "") filepath = defaultDirectory + "/" + filename + fileExtension;
            else filepath = defaultDirectory + directory + "/" + filename + fileExtension;
            if (!File.Exists(filepath))
            {
                Debug.Log($"CannotDelete BadFilePath : {filepath}");
                return;
            }
            File.Delete(filepath);
            string metapath;
            if (directory == "") metapath = defaultDirectory + "/" + filename + fileExtension + ".meta";
            else metapath = defaultDirectory + directory + "/" + filename + fileExtension + ".meta";
            if (File.Exists(metapath)) File.Delete(metapath);
        }
    }
}
