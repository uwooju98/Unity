using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class Practice1 : MonoBehaviour
{
    private string path;
    string[] files;
    private Dictionary<string, string> info = new Dictionary<string, string>();


    private void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "SaveData");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            Debug.Log("폴더 생성");
        }
        else
        {
            CreatSaveData();
            FileRead();
            PrintAll();
            FileCopy();
            FileDelete();
            PrintAll();
        }

    }


    private void CreatSaveData()
    {
        using (StreamWriter saveData1 = File.CreateText(Path.Combine(path, "saveData1.txt")))
            saveData1.WriteLine("Name=철수");
        using (StreamWriter saveData2 = File.CreateText(Path.Combine(path, "saveData2.txt")))
            saveData2.WriteLine("Name=영희");
        using (StreamWriter saveData3 = File.CreateText(Path.Combine(path, "saveData3.txt")))
            saveData3.WriteLine("Name=민수");
        Debug.Log("파일 생성");
    }


    private void FileRead()
    {
        files = Directory.GetFiles(path);
        Debug.Log("파일 로드");
    }

    private void PrintAll()
    {
        for (int i = 0; i < files.Length; i++)
        {
            Debug.Log($"{Path.GetFileNameWithoutExtension(files[i])}");
            Debug.Log($"{Path.GetExtension(files[i])}");
        }
    }


    private void FileCopy()
    {
        string allText;
        string pathSave1 = Path.Combine(Application.persistentDataPath, "SaveData", "saveData1.txt");

        using (StreamReader reader = File.OpenText(pathSave1))
        {
            allText = reader.ReadToEnd();
        }
 
        using (StreamWriter saveData_backup = File.CreateText(Path.Combine(path, "save1_backup.txt")))
            saveData_backup.WriteLine(allText);
        Debug.Log("파일 복사");
    }


    private void FileDelete()
    {
        string save3Path = Path.Combine(Application.persistentDataPath, path, "saveData3.txt");
        if (File.Exists(save3Path))
        {
            Debug.Log("파일이 존재합니다.");
            File.Delete(save3Path);
            Debug.Log("파일이 삭제되었습니다.");
        }
    }
}
