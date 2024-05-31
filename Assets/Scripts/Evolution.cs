using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class Evolve : MonoBehaviour
{
    public List<Sprite> backgrounds = new List<Sprite>(); // Liste des sprites de fond
    private int currentIndex = 0; // Indice actuel dans la liste de sprites de fond

    public SpriteRenderer backgroundRenderer; // Référence au sprite renderer du fond
    public Tilemap groundTilemap; // Référence au Tilemap du sol

    public TileBase[] groundTiles; // Tableau des Tiles de sol
    private int currentTileIndex = 0; // Indice actuel dans le tableau de Tiles de sol

    public List<Sprite> playerBases = new List<Sprite>(); // Liste des sprites de base du joueur
    public SpriteRenderer player1BaseRenderer; // Référence au sprite renderer de la base du joueur
    public SpriteRenderer player2BaseRenderer; // Référence au sprite renderer de la base du joueur


    public void ChangeBackgroundAndGround()
    {
        // Vérifier si le sprite renderer du fond est défini
        if (backgroundRenderer != null && currentIndex < backgrounds.Count)
        {
            // Changer le sprite du fond
            backgroundRenderer.sprite = backgrounds[currentIndex];

            // Changer le sol avec le Tilemap
            ChangeGround();

            // Changer le sprite de la base du joueur
            ChangePlayerBase();

            // Passer à l'indice suivant pour la prochaine fois
            currentIndex = (currentIndex + 1) % backgrounds.Count;
        }
        else
        {
            Debug.LogWarning("Le sprite renderer du fond ou la liste de sprites de fond n'est pas correctement définie.");
        }
    }

    // Fonction pour changer le sol avec le Tilemap
    private void ChangeGround()
    {
        // Si le Tilemap et les Tiles ne sont pas définis, on quitte la fonction
        if (groundTilemap == null || groundTiles == null || groundTiles.Length == 0)
        {
            Debug.LogError("Tilemap ou Tiles non définis !");
            return;
        }

        // Pour chaque cellule dans le Tilemap, on change le Tile
        BoundsInt bounds = groundTilemap.cellBounds;
        TileBase[] allTiles = groundTilemap.GetTilesBlock(bounds);

        foreach (var position in bounds.allPositionsWithin)
        {
            if (groundTilemap.HasTile(position))
            {
                groundTilemap.SetTile(position, GetCurrentTile());
            }
        }

        // Passer au prochain Tile dans le tableau
        currentTileIndex = (currentTileIndex + 1) % groundTiles.Length;
    }

    // Fonction pour obtenir le Tile courant dans le tableau
    private TileBase GetCurrentTile()
    {
        return groundTiles[currentTileIndex];
    }

    // Fonction pour changer le sprite de la base du joueur
    private void ChangePlayerBase()
    {
        if (player1BaseRenderer != null && playerBases.Count > 0)
        {
            player1BaseRenderer.sprite = playerBases[currentIndex];
        }
        else
        {
            Debug.LogWarning("Le sprite renderer de la base du joueur ou la liste de sprites de base du joueur n'est pas correctement définie.");
        }
    }
}