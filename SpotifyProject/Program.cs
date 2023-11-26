using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        MusicApp musicApp = new MusicApp();
        musicApp.Run();
    }

}

class MusicApp
{
    private List<Artist> artists;
    private List<Playlist> playlists;

    public MusicApp()
    {
        // Inizializzazione degli artisti e delle loro canzoni
        // Inizializzazione degli artisti e delle loro canzoni
        artists = new List<Artist>
{
    new Artist("JD", "Rock", new List<Album>
    {
        new Album("Album1", "Alternative", new List<Song>
        {
            new Song("Song1", "Rock"),
            new Song("Song2", "Rock")
        }),
        new Album("Album2", "Rock", new List<Song>
        {
            new Song("Song3", "Alternative"),
            new Song("Song4", "Alternative")
        })
    }),
    new Artist("JS", "Pop", new List<Album>
    {
        new Album("Album3", "Pop", new List<Song>
        {
            new Song("Song5", "Pop"),
            new Song("Song6", "Pop")
        })
    })
};

        // Inizializzazione delle playlist dell'utente
        playlists = new List<Playlist>
{
    new Playlist("Playlist1", new List<Song>
    {
        new Song("Song7", "Rock"),
        new Song("Song8", "Alternative")
    }),
    new Playlist("Playlist2", new List<Song>
    {
        new Song("Song9", "Pop"),
        new Song("Song10", "Pop")
    })
};


    }
    private List<T> GetTop5<T>(List<T> elements) where T : class
    {
        // Ordina gli elementi in base al conteggio delle riproduzioni in ordine decrescente
        List<T> sortedElements = elements.OrderByDescending(e => GetPlayCount(e)).ToList();

        // Prendi i primi 5 elementi
        return sortedElements.Take(5).ToList();
    }

    private int GetPlayCount<T>(T element) where T : class
    {
        // Ottieni il conteggio delle riproduzioni in base al tipo di elemento
        if (element is Song)
        {
            return (element as Song).PlayCount;
        }
        // Aggiungi altri casi per gli altri tipi (Album, Artist, ecc.) se necessario
        else
        {
            return 0; // Default a 0 se il tipo non è gestito
        }
    }

    public void Run()
    {
        Console.WriteLine("Benvenuto su Spotify-like Console App!");

        while (true)
        {
            Console.WriteLine("\nPer iniziare un brano, premi 'M'. Per uscire, premi 'Q'.");
            char choice = Console.ReadKey().KeyChar;

            if (choice == 'Q' || choice == 'q')
            {
                Console.WriteLine("\nGrazie per aver utilizzato la nostra app. Arrivederci!");
                break;
            }

            if (choice == 'M' || choice == 'm')
            {
                DisplayMusicMenu();
            }
            else
            {
                Console.WriteLine("\nScelta non valida. Riprova.");
            }
        }
    }

    private Album GetAlbumByIndex(int index)
    {
        int currentIndex = 1;

        foreach (var artist in artists)
        {
            foreach (var album in artist.Albums)
            {
                if (currentIndex == index)
                {
                    return album;
                }
                currentIndex++;
            }
        }

        return null; // Restituisci null se l'album non è stato trovato
    }

    private void AddSongToPlaylist()
    {
        // Visualizza elenco delle playlist
        Console.WriteLine("\nLe tue Playlists:");

        for (int i = 0; i < playlists.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {playlists[i].Title}");
        }

        Console.WriteLine("\nSeleziona una playlist a cui aggiungere la canzone:");
        int playlistChoice;

        if (int.TryParse(Console.ReadLine(), out playlistChoice) && playlistChoice > 0 && playlistChoice <= playlists.Count)
        {
            Playlist selectedPlaylist = playlists[playlistChoice - 1];

            // Visualizza elenco delle canzoni disponibili
            Console.WriteLine("\nElenco delle canzoni disponibili:");

            // Puoi mostrare le canzoni da tutti gli artisti o solo da un artista specifico, a tua discrezione.
            // Qui mostro le canzoni da tutti gli artisti.
            int songIndex = 1;
            foreach (var artist in artists)
            {
                foreach (var album in artist.Albums)
                {
                    foreach (var song in album.Songs)
                    {
                        Console.WriteLine($"{songIndex}. {song.Title} - {artist.Name} ({album.Title})");
                        songIndex++;
                    }
                }
            }

            Console.WriteLine("\nSeleziona una canzone da aggiungere alla playlist:");
            int songChoice;

            if (int.TryParse(Console.ReadLine(), out songChoice) && songChoice > 0 && songChoice <= songIndex)
            {
                Song selectedSong = GetSongByIndex(songChoice);

                // Aggiungi la canzone alla playlist
                selectedPlaylist.Songs.Add(selectedSong);

                Console.WriteLine($"\n'{selectedSong.Title}' è stata aggiunta a '{selectedPlaylist.Title}'.");
            }
            else
            {
                Console.WriteLine("\nSelezione non valida. Riprova.");
            }
        }
        else
        {
            Console.WriteLine("\nSelezione non valida. Riprova.");
        }
    }

    private Song GetSongByIndex(int index)
    {
        int currentIndex = 1;

        foreach (var artist in artists)
        {
            foreach (var album in artist.Albums)
            {
                foreach (var song in album.Songs)
                {
                    if (currentIndex == index)
                    {
                        return song;
                    }
                    currentIndex++;
                }
            }
        }

        return null; // Restituisci null se la canzone non è stata trovata
    }

    private void DisplayMusicMenu()
    {
        Console.WriteLine("\nScegli un'opzione:");
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("1. Artists");
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("2. Albums");
        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("3. Playlists");
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("4. Radio");
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("5. Search");

        Console.ResetColor();

        char musicChoice = Console.ReadKey().KeyChar;

        switch (musicChoice)
        {
            case '1':
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Black;
                DisplayArtists();
                Console.ResetColor();
                break;


            case '2':
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                int selectedAlbumIndex = DisplayAllAlbums();
                if (selectedAlbumIndex != -1)
                {
                    Album selectedAlbum = GetAlbumByIndex(selectedAlbumIndex);
                    DisplaySongs(selectedAlbum);

                    // Implementa la riproduzione
                    PlaySongs(selectedAlbum.Songs);
                }
                Console.ResetColor();
                break;

            case '3':
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                DisplayUserPlaylists();
                Console.ResetColor();
                break;

            case '4':
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                // Implementa la logica per la radio
                Console.WriteLine("\nHai scelto Radio. Implementa la logica qui.");
                Console.ResetColor();
                break;

            case '5':
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                // Implementa la logica per la ricerca
                Console.WriteLine("\nHai scelto Search. Implementa la logica qui.");
                break;

            default:
                Console.WriteLine("\nScelta non valida. Riprova.");
                Console.ResetColor();
                break;
        }
    }

    private void DisplayArtists()
    {
        Console.WriteLine("\nLista degli Artisti:");

        for (int i = 0; i < artists.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {artists[i].Name} ({artists[i].Genre})");
        }

        Console.WriteLine("\nSeleziona un artista:");
        int artistChoice;

        if (int.TryParse(Console.ReadLine(), out artistChoice) && artistChoice > 0 && artistChoice <= artists.Count)
        {
            Artist selectedArtist = artists[artistChoice - 1];
            DisplayArtistAlbums(selectedArtist);

            // Implementa la riproduzione
            PlaySongs(selectedArtist.Albums.SelectMany(album => album.Songs).ToList());

            // Ottieni e visualizza i Top 5 brani dell'artista
            List<Song> topSongs = GetTop5(selectedArtist.Albums.SelectMany(album => album.Songs).ToList());
            Console.WriteLine("\nTop 5 brani più ascoltati dell'artista:");
            DisplayTopSongs(topSongs);
        }
        else
        {
            Console.WriteLine("\nSelezione non valida. Riprova.");
        }
    }


    private void DisplayArtistAlbums(Artist artist)
    {
        Console.WriteLine($"\nAlbums dell'artista '{artist.Name}' ({artist.Genre}):");

        for (int i = 0; i < artist.Albums.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {artist.Albums[i].Title}");
        }

        Console.WriteLine("\nSeleziona un album:");
        int albumChoice;

        if (int.TryParse(Console.ReadLine(), out albumChoice) && albumChoice > 0 && albumChoice <= artist.Albums.Count)
        {
            Album selectedAlbum = artist.Albums[albumChoice - 1];
            DisplaySongs(selectedAlbum);

            // Implementazione della riproduzione
            PlaySongs(selectedAlbum.Songs);
        }
        else
        {
            Console.WriteLine("\nSelezione non valida. Riprova.");
        }
    }

    private int DisplayAllAlbums()
    {
        Console.WriteLine("\nTutti gli Albums disponibili:");

        int index = 1;

        foreach (var artist in artists)
        {
            foreach (var album in artist.Albums)
            {
                Console.WriteLine($"{index}. {album.Title} by {artist.Name} ({album.Genre})");
                index++;
            }
        }

        Console.WriteLine("\nSeleziona un album:");
        if (int.TryParse(Console.ReadLine(), out int albumChoice) && albumChoice > 0 && albumChoice <= index)
        {
            Album selectedAlbum = GetAlbumByIndex(albumChoice);
            if (selectedAlbum != null)
            {
                return albumChoice;
            }
            else
            {
                Console.WriteLine("\nAlbum non trovato. Riprova.");
                return -1; // Indica che l'album non è stato trovato
            }
        }
        else
        {
            Console.WriteLine("\nSelezione non valida. Riprova.");
            return -1; // Indica che la selezione non è valida
        }
    }

    private void DisplayUserPlaylists()
    {
        Console.WriteLine("\nLe tue Playlists:");

        for (int i = 0; i < playlists.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {playlists[i].Title}:");

            // Visualizza le canzoni nella playlist
            for (int j = 0; j < playlists[i].Songs.Count; j++)
            {
                Console.WriteLine($"   {j + 1}. {playlists[i].Songs[j].Title} ({playlists[i].Songs[j].Genre})");
            }
        }

        Console.WriteLine("\nSeleziona una playlist:");
        int playlistChoice;

        if (int.TryParse(Console.ReadLine(), out playlistChoice) && playlistChoice > 0 && playlistChoice <= playlists.Count)
        {
            Playlist selectedPlaylist = playlists[playlistChoice - 1];
            DisplayPlaylistSongs(selectedPlaylist);

            // Implementazione della riproduzione delle canzoni nella playlist
            PlaySongs(selectedPlaylist.Songs);
        }
        else
        {
            Console.WriteLine("\nSelezione non valida. Riprova.");
        }
    }



    private void DisplayPlaylistSongs(Playlist playlist)
    {
        Console.WriteLine($"\nCanzoni della playlist '{playlist.Title}':");

        for (int i = 0; i < playlist.Songs.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {playlist.Songs[i].Title}");
        }
    }

    private void DisplaySongs(Album album)
    {
        Console.WriteLine($"\nCanzoni dell'album '{album.Title}':");

        for (int i = 0; i < album.Songs.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {album.Songs[i].Title}");
        }
    }
    private void DisplayTopSongs(List<Song> topSongs)
    {
        for (int i = 0; i < topSongs.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {topSongs[i].Title} ({topSongs[i].Genre}) - Riproduzioni: {topSongs[i].PlayCount}");
        }
    }


    private void PlaySongs(List<Song> songs)
    {
        MediaPlayer player = new MediaPlayer();
        player.Start(songs);

        Console.WriteLine("\nPremi 'P' per riprodurre, 'S' per fermare, 'N' per la prossima canzone, 'B' per la canzone precedente, 'E' per uscire.");

        while (true)
        {
            char key = Console.ReadKey().KeyChar;
            Console.WriteLine();  // Vai a capo dopo la lettura del tasto

            switch (key)
            {
                case 'P':
                case 'p':
                    player.Start(songs);  // Riavvia la riproduzione
                    break;

                case 'S':
                case 's':
                    player.Stop();
                    break;

                case 'N':
                case 'n':
                    player.Next(songs);
                    break;

                case 'B':
                case 'b':
                    player.Previous(songs);
                    break;

                case 'E':
                case 'e':
                    return;  // Esci dal loop di riproduzione

                default:
                    Console.WriteLine("Tasto non valido. Riprova.");
                    break;
            }

        }
    }
}

class Artist
{
    public string Name { get; set; }
    public string Genre { get; set; } // Nuovo campo per il genere
    public List<Album> Albums { get; set; }

    public Artist(string name, string genre, List<Album> albums)
    {
        Name = name;
        Genre = genre;
        Albums = albums;
    }
}

class Album
{
    public string Title { get; set; }
    public string Genre { get; set; } // Nuovo campo per il genere
    public List<Song> Songs { get; set; }

    public Album(string title, string genre, List<Song> songs)
    {
        Title = title;
        Genre = genre;
        Songs = songs;
    }
}

class Song
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public int PlayCount { get; private set; } // Nuovo campo per il conteggio delle riproduzioni

    public Song(string title, string genre)
    {
        Title = title;
        Genre = genre;
        PlayCount = 0; // Inizializza il conteggio delle riproduzioni a 0
    }

    public void IncrementPlayCount()
    {
        PlayCount++;
    }
}



class Playlist
{
    public string Title { get; set; }
    public List<Song> Songs { get; set; }

    public Playlist(string title, List<Song> songs)
    {
        Title = title;
        Songs = songs;
    }
}




class MediaPlayer
{
    private int currentIndex;

    public void Stop()
    {
        Console.WriteLine("Riproduzione fermata.");
    }

    public void Next(List<Song> songs)
    {
        if (currentIndex < songs.Count - 1)
        {
            currentIndex++;
            Console.WriteLine($"Brano successivo:\nPosizione {currentIndex + 1}\n{songs[currentIndex].Title}");
        }
        else
        {
            Console.WriteLine("Sei alla fine della playlist. Nessun brano successivo disponibile.");
        }
    }

    public void Previous(List<Song> songs)
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            Console.WriteLine($"Brano precedente:\nPosizione {currentIndex + 1}\n{songs[currentIndex].Title}");
        }
        else
        {
            Console.WriteLine("Sei all'inizio della playlist. Nessun brano precedente disponibile.");
        }
    }
    public void Start(List<Song> songs)
    {
        songs[currentIndex].IncrementPlayCount(); // Incrementa il conteggio delle riproduzioni
        Console.WriteLine($"Riproduzione in corso:\nPosizione {currentIndex + 1}\n{songs[currentIndex].Title}");
    }

}

