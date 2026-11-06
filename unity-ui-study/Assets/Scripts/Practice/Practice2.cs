using System.IO;
using UnityEngine;

public class Practice2 : MonoBehaviour
{
    private string path;
    private string originalPath;
    private string encryptedPath;
    private string decryptedPath;

    private const byte Key = 0xAB;


    private void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "FileStreamPractice");
        originalPath = Path.Combine(path, "original.txt");
        encryptedPath = Path.Combine(path, "encrypted.dat");
        decryptedPath = Path.Combine(path, "decrypted.txt"); //질문 순서 중요한가

        Debug.Log(path);

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            Debug.Log("폴더 생성");
        }

        CreatOriginal();
        Encryption();
        Decryption();
        AllRead();
    }


    private void CreatOriginal()
    {
        File.WriteAllText(originalPath, "암호화연습");

        Debug.Log("원본 생성");
    }


    private void AllRead()
    {
        Debug.Log($"원본: {File.ReadAllText(originalPath)}");
        Debug.Log($"암호화: {File.ReadAllText(encryptedPath)}");//asd
        Debug.Log($"복호화: {File.ReadAllText(decryptedPath)}");
    }


    private void Encryption()
    {
        if (!File.Exists(originalPath))
        {
            Debug.Log("원본 파일 없음");
            return;
        }

        using (FileStream originalInput = new FileStream(originalPath, FileMode.Open))
        using (FileStream encryptedOutput = new FileStream(encryptedPath, FileMode.Create))
        {
            int read;

            while ((read = originalInput.ReadByte()) != -1)
            {
                byte encryptedByte = (byte)(read ^ Key);

                encryptedOutput.WriteByte(encryptedByte);

                Debug.Log($"원본: {read}, 암호화: {encryptedByte}");
            }
        }
    }


    private void Decryption()
    {
        if (!File.Exists(encryptedPath))
        {
            Debug.Log("암호화 파일 없음");
            return;
        }

        using (FileStream encryptedInput = new FileStream(encryptedPath, FileMode.Open))
        using (FileStream decryptedOutput = new FileStream(decryptedPath, FileMode.Create))
        {
            int read;

            while ((read = encryptedInput.ReadByte()) != -1)
            {
                byte decryptedByte = (byte)(read ^ Key);

                decryptedOutput.WriteByte(decryptedByte);

                Debug.Log($"암호화: {read}, 복호화: {decryptedByte}");
            }
        }
    }


}
