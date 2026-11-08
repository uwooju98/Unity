using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public static class CsvLoader
{
    public static List<T> ReadCsv<T>(string path)
    {
        TextAsset table = Resources.Load<TextAsset>(path);

        using (var reader = new StringReader(table.text))
        using (var csvReader = new CsvReader(reader,CultureInfo.InvariantCulture))
        {
            return csvReader.GetRecords<T>().ToList();
        }
    }
}
