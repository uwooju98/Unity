using System;
using Newtonsoft.Json;
using UnityEngine;
public class SavePlrData
{
    public Guid InstanceId { get; set; }

    [JsonConverter(typeof(PlayerDataConverter))]
    public PlrData PlrData { get; set; }

    public DateTime CreationTime { get; set; }

    public SavePlrData()
    {
        InstanceId = Guid.NewGuid();
        CreationTime = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{InstanceId} {PlrData.Id} : (${CreationTime})";
    }

    public static SavePlrData GetRandomPlayer()
    {
        var newPlayer = new SavePlrData();
        newPlayer.PlrData = DataTableManager.plrTable.GetRandom();
        return newPlayer;
    }
}
