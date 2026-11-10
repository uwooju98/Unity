using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Practice2 : MonoBehaviour
{
    private const byte Key = 0xAB;

    private string folder;
    private string srcPath;
    private string encPath;
    private string decPath;


    void Start()
    {
        folder = Path.Combine(Application.persistentDataPath, "Secret");
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        srcPath = Path.Combine(folder, "original.txt");
        encPath = Path.Combine(folder, "encrypted.dat");
        decPath = Path.Combine(folder, "decrypted.txt");
    }

    void Update()
    {
        
    }

    private void WriteOriginal()
    {
        File.WriteAllText(srcPath, "Hello Unity World!!");
    }

    private void Encrypt()
    {
        using(var input = new FileStream(srcPath, FileMode.Open))
        using(var output = new FileStream(encPath, FileMode.Create))
        {
            int b;
            while ((b = input.ReadByte()) != -1)
            {
                output.WriteByte((byte)(b ^ Key));
            }
        }
    }
    // Resources
    private void Decrypt()
    {
        using(Stream input = new FileStream(encPath, FileMode.Open))
        using(Stream output = new FileStream(decPath, FileMode.Create))
        {
            int b;
            while ((b = input.ReadByte()) != -1)
            {
                output.WriteByte((byte)(b ^ Key));
            }
        }

        string result = File.ReadAllText(decPath);
        bool equal = result == File.ReadAllText(srcPath);
    }
}
