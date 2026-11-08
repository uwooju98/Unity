using System.Collections.Generic;

public class PlayersTable : DataTable
{
    public Dictionary<string, PlayerData> players = new Dictionary<string, PlayerData>();

    public PlayerData Get(string id)
    {
        if (players.TryGetValue(id, out PlayerData data))
            return data;

        return null;
    }

    public override void Load(string filename)
    {
        string path = $"DataTables/{filename}";

        List<PlayerData> datas = CsvLoader.ReadCsv<PlayerData>(path);

        if (datas == null)
            return;

        foreach (var data in datas)
        {
            if (players.ContainsKey(data.Id))
                continue;

            players.Add(data.Id, data);
        }
    }
}

