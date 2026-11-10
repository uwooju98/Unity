using System.IO;
using UnityEngine;

public class FileStreamExample : MonoBehaviour
{
    void Start()
    {
        string path = Path.Combine(Application.persistentDataPath, "test.bin");

        // 파일에 바이트 쓰기
        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            fs.WriteByte(72); // 'H'
            fs.WriteByte(101); // 'e'
            fs.WriteByte(108); // 'l'
            fs.WriteByte(108); // 'l'
            fs.WriteByte(111); // 'o'

            fs.Seek(5, SeekOrigin.Current);
            
            byte[] data = { 33, 33, 33 }; // '!!!'
            fs.Write(data, 0, data.Length);

            Debug.Log($"파일 크기: {fs.Length} bytes");
            Debug.Log($"현재 위치: {fs.Position}");
        }

        // 파일에서 바이트 읽기
        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            Debug.Log($"읽기 가능: {fs.CanRead}");
            Debug.Log($"쓰기 가능: {fs.CanWrite}");

            // 1바이트씩 읽기
            int b = fs.ReadByte();
            Debug.Log($"첫 번째 바이트: {b} ('{(char)b}')");

            // 바이트 배열로 읽기
            byte[] buffer = new byte[4];
            int bytesRead = fs.Read(buffer, 0, buffer.Length);
            Debug.Log($"읽은 바이트 수: {bytesRead}");
        }
    }
}
