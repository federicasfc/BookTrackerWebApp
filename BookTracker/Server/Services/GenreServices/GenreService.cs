namespace BookTracker.Server.Services.GenreServices

{
    public class GenreService : IGenreService
    {

        /* GetSongsByArtist -- refactor for GetBooksByGenre
         
     public async Task<ArtistSongsListItem> GetSongsByArtistAsync(string artistName)
        {
            var artistEntity = await _dbContext.Artists
               .Include(a => a.Songs)
               .FirstOrDefaultAsync(e => e.Name == artistName);

            return artistEntity is null ? null : new ArtistSongsListItem
            {
                Songs = artistEntity.Songs.Select(entity => new SongListItem
                {
                    SongId = entity.SongId,
                    Name = entity.Name

                }).ToList()
            };
        }
         */
    }
}
