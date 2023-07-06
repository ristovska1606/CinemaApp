using CinemaApp.Domain.DomainModels;
using CinemaApp.Domain.DTO;
using CinemaApp.Repository.Interface;
using CinemaApp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaApp.Service.Implementation
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<Movie> _movieRepository;

        public MovieService(IRepository<Ticket> ticketRepository, IRepository<Movie> movieRepository)
        {
            _ticketRepository = ticketRepository;
            _movieRepository = movieRepository;
        }

        public Movie CreateNewMovie(Movie newEntity)
        {
            newEntity.Id = Guid.NewGuid();
           
            for (int i = 0; i < 20; i++)
            {

                Ticket ticketToInsert = new Ticket(newEntity.TicketPrice, i + 1, newEntity.Id);
                _ticketRepository.Insert(ticketToInsert);
            }
            return _movieRepository.Insert(newEntity);
        }

        public Movie DeleteMovie(Guid? id)
        {
            var movieToDelete = this.GetSpecificMovie(id);
            return _movieRepository.Delete(movieToDelete);
        }

       
        public List<Movie> GetAllMoviesAsList()
        {
            return _movieRepository.GetAll().ToList();
        }

        public Movie GetSpecificMovie(Guid? id)
        {
            return _movieRepository.Get(id);
        }

        public bool MovieExist(Guid? id)
        {
            var ticketExist = this.GetSpecificMovie(id);

            return ticketExist != null;
        }

        public Movie UpdateExistingMovie(Movie updatedMovie)
        {
            return _movieRepository.Update(updatedMovie);
        }
    }
}
