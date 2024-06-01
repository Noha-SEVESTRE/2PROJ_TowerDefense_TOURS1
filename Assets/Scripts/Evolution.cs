using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class Evolution : MonoBehaviour
{
    public List<Sprite> backgrounds = new List<Sprite>(); // Liste des sprites de fond
    public List<Color> playerColors = new List<Color>(); // Liste des couleurs des joueurs

    public SpriteRenderer backgroundRenderer; // Référence au sprite renderer du fond
    public Tilemap groundTilemap; // Référence au Tilemap du sol

    public TileBase[] groundTiles; // Tableau des Tiles de sol

    public List<Sprite> playerBases = new List<Sprite>(); // Liste des sprites de base du joueur
    public SpriteRenderer player1BaseRenderer; // Référence au sprite renderer de la base du joueur
    public SpriteRenderer player2BaseRenderer; // Référence au sprite renderer de la base du joueur

    public int Player1Level = 1; // Niveau du joueur 1
    public int Player2Level = 1; // Niveau du joueur 2

    private void Start()
    {
        // Assurez-vous que vos listes et vos références sont correctement initialisées ici
    }

    public void Player1Evolve()
    {
        if (Player1Level < 7)
        {
            Player1Level += 1;
            UpdateGraphics();
        }
    }

    public void Player2Evolve()
    {
        if (Player2Level < 7)
        {
            Player2Level += 1;
            UpdateGraphics();
        }
    }

    private void UpdateGraphics()
    {
        int maxLevel = Math.Max(Player1Level, Player2Level);
        ChangeBackgroundAndGround(maxLevel - 1);
        ChangePlayerBase(maxLevel, Player1Level);
    }

    private void ChangeBackgroundAndGround(int maxLevel)
    {
        if (maxLevel > 6)
        {
            Debug.LogWarning("Les joueurs ont atteint le niveau maximum.");
            return;
        }

        if (backgroundRenderer != null && maxLevel < backgrounds.Count)
        {
            backgroundRenderer.sprite = backgrounds[maxLevel];
            ChangeGround(maxLevel);
        }
        else
        {
            Debug.LogWarning("Le sprite renderer du fond ou la liste de sprites de fond n'est pas correctement définie.");
        }
    }

    private void ChangeGround(int maxLevel)
    {
        if (groundTilemap == null || groundTiles == null || groundTiles.Length == 0)
        {
            Debug.LogError("Tilemap ou Tiles non définis !");
            return;
        }

        BoundsInt bounds = groundTilemap.cellBounds;

        foreach (var position in bounds.allPositionsWithin)
        {
            if (groundTilemap.HasTile(position))
            {
                groundTilemap.SetTile(position, GetCurrentTile(maxLevel));
            }
        }
    }

    private TileBase GetCurrentTile(int maxLevel)
    {
        return groundTiles[maxLevel];
    }

    private void ChangePlayerBase(int maxLevel, int playerLevel)
    {
        if (player1BaseRenderer != null && player2BaseRenderer != null && playerBases.Count > 0)
        {
            player1BaseRenderer.sprite = playerBases[playerLevel - 1];
            player2BaseRenderer.sprite = playerBases[Player2Level - 1];
        }
        else
        {
            Debug.LogWarning("Le sprite renderer de la base du joueur ou la liste de sprites de base du joueur n'est pas correctement définie.");
        }
    }


    public Color DeterminePlayerColor(int maxLevel, int playerLevel)
    {
        int colorIndex = Mathf.Clamp(playerLevel - 1, 0, playerColors.Count - 1);
        return playerColors[colorIndex];
    }
}
