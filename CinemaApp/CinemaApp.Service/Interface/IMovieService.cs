using CinemaApp.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.Service.Interface
{
    public interface IMovieService
    {
        public List<Movie> GetAllMoviesAsList();
        public Movie GetSpecificMovie(Guid? id);
        public Movie CreateNewMovie(Movie newEntity);
        public Movie CreateNewMovieWithParametars(String MovieTitle, String MovieCover, DateTime ValidFrom, DateTime ValidTo, String TicketPrice);
        public Movie UpdateExistingMovie(Movie updatedMovie);
        public Movie DeleteMovie(Guid? id);
        public Boolean MovieExist(Guid? id);
    }
}
