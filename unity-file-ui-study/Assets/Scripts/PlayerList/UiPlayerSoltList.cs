
using System;
using System.Collections.Generic;
using UnityEngine;

public class UiPlayerSoltList : MonoBehaviour
{
    public enum SortingOptions
    {
        DateAsc,
        DateDesc,
        NameAsc,
        NameDesc
    }

    public UiPlayerButton playerButtonPrefab;
    public Transform contentRoot;
    public UiPlayerInfo playerInfo;

    private readonly List<SavePlrData> players = new List<SavePlrData>();
    private readonly Dictionary<Guid, UiPlayerButton> playerButtons = new Dictionary<Guid, UiPlayerButton>();

    private SavePlrData selectedPlayer;
    private SortingOptions sorting = SortingOptions.DateAsc;
    private FilteringOptions filtering = FilteringOptions.All;


    public SortingOptions Sorting
    {
        get => sorting;
        set
        {
            sorting = value;
            SortPlayers();
        }
    }

    public FilteringOptions Filtering
    {
        get => filtering;
        set
        {
            filtering = value;
            UpdateFiltering();
        }
    }


    public enum FilteringOptions
    {
        All,
        Warrior,
        Archer,
        Rogue,
        Paladin
    }

    private void Start()
    {
        playerInfo.SetEmpty();
        LoadPlayers();
    }

    private void OnEnable()
    {
        Variables.OnLanguageChanged += UpdateLanguage;
    }

    private void OnDisable()
    {
        Variables.OnLanguageChanged -= UpdateLanguage;
    }

    private void UpdateLanguage()
    {
        foreach (SavePlrData player in players)
        {
            if (playerButtons.TryGetValue(player.InstanceId, out UiPlayerButton uiButton))
            {
                uiButton.SetPlayer(player);
            }
        }

        if (selectedPlayer != null)
        {
            playerInfo.SetPlayer(selectedPlayer);
        }

        SortPlayers();
    }


    public void AddRandomPlayer()
    {
        SavePlrData player = SavePlrData.GetRandomPlayer();

        if (player == null || player.PlrData == null)
        {
            Debug.LogError("플레이어 생성 실패");
            return;
        }

        players.Add(player);
        CreatePlayerButton(player);
        SortPlayers();

        PlrSaveLoadManager.Save(players);
    }

    private void CreatePlayerButton(SavePlrData player)
    {
        UiPlayerButton uiButton = Instantiate(playerButtonPrefab, contentRoot);

        uiButton.SetPlayer(player);
        uiButton.OnClicked += OnPlayerSelected;

        playerButtons.Add(player.InstanceId, uiButton);
    }

    private void OnPlayerSelected(SavePlrData player)
    {
        selectedPlayer = player;
        playerInfo.SetPlayer(selectedPlayer);

        Debug.Log($"선택: {selectedPlayer.PlrData.StringName}");
    }

    public void RemoveSelectedPlayer()
    {
        if (selectedPlayer == null)
        {
            return;
        }

        Guid id = selectedPlayer.InstanceId;

        players.Remove(selectedPlayer);

        if (playerButtons.TryGetValue(id, out UiPlayerButton uiButton))
        {
            uiButton.OnClicked -= OnPlayerSelected;
            playerButtons.Remove(id);
            Destroy(uiButton.gameObject);
        }

        selectedPlayer = null;
        playerInfo.SetEmpty();

        PlrSaveLoadManager.Save(players);
    }

    private void SortPlayers()
    {
        switch (sorting)
        {
            case SortingOptions.DateAsc:
                players.Sort((a, b) => a.CreationTime.CompareTo(b.CreationTime));
                break;

            case SortingOptions.DateDesc:
                players.Sort((a, b) => b.CreationTime.CompareTo(a.CreationTime));
                break;

            case SortingOptions.NameAsc:
                players.Sort((a, b) => string.Compare(a.PlrData.StringName, b.PlrData.StringName, StringComparison.CurrentCulture));
                break;

            case SortingOptions.NameDesc:
                players.Sort((a, b) => string.Compare(b.PlrData.StringName, a.PlrData.StringName, StringComparison.CurrentCulture));
                break;
        }

        foreach (SavePlrData player in players)
        {
            if (playerButtons.TryGetValue(player.InstanceId, out UiPlayerButton uiButton))
            {
                uiButton.transform.SetAsLastSibling();
            }
        }

        UpdateFiltering();
    }

    private bool PassesFilter(SavePlrData player)
    {
        switch (filtering)
        {
            case FilteringOptions.All:
                return true;

            case FilteringOptions.Warrior:
                return player.PlrData.Id == "CHAR_1";

            case FilteringOptions.Archer:
                return player.PlrData.Id == "CHAR_2";

            case FilteringOptions.Rogue:
                return player.PlrData.Id == "CHAR_3";

            case FilteringOptions.Paladin:
                return player.PlrData.Id == "CHAR_4";
        }

        return false;
    }

    private void UpdateFiltering()
    {
        foreach (SavePlrData player in players)
        {
            if (playerButtons.TryGetValue(player.InstanceId, out UiPlayerButton uiButton))
            {
                uiButton.gameObject.SetActive(PassesFilter(player));
            }
        }

        if (selectedPlayer != null && !PassesFilter(selectedPlayer))
        {
            selectedPlayer = null;
            playerInfo.SetEmpty();
        }
    }


    private void LoadPlayers()
    {
        List<SavePlrData> loadedPlayers = PlrSaveLoadManager.Load();

        foreach (SavePlrData player in loadedPlayers)
        {
            if (player == null || player.PlrData == null)
            {
                continue;
            }

            players.Add(player);
            CreatePlayerButton(player);
        }

        SortPlayers();
    }
}
