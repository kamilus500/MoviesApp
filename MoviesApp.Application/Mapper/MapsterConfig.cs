using Mapster;
using MoviesApp.Application.Movies;
using MoviesApp.Domain.Entities;

namespace MoviesApp.Application.Mapper
{
    public static class MapsterConfig
    {
        public static void Configure()
        {
            TypeAdapterConfig<CreateUpdateMovieDto, Movie>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Director, src => src.Director)
                .Map(dest => dest.Rate, src => src.Rate)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Year, src => src.Year);
        }
    }
}
