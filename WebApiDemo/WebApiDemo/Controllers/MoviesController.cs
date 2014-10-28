using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http;
using System.Web.Http.OData;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    [RoutePrefix("api/Movies")]
    public class MoviesController : ApiController
    {
        private readonly MoviesRepository _repository = new MoviesRepository();

        [EnableQuery]
        public IHttpActionResult Get()
        {
            return Ok(_repository.Get());
        }

        // GET /api/Movies/5
        [Route("{id:int:min(1)}")]
        public IHttpActionResult Get(int id)
        {
            var movie = _repository.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        // GET /api/Movies/Inception
        [Route("{title:alpha}")]
        public IHttpActionResult GetByTitle(string title)
        {
            var result = _repository.Get(m => m.Title.StartsWith(title, StringComparison.OrdinalIgnoreCase));
            if (result == null || !result.Any())
            {
                return NotFound();
            }

            return Ok(result);
        }
    }

    class MoviesRepository
    {
        private readonly List<Movie> _data = new List<Movie>();

        public MoviesRepository()
        {
            //Populate the list with some movies
            _data.Add(new Movie { Id = 1, Title = "Dracula Untold", Length = TimeSpan.FromMinutes(92), Genre = "Action, Adventure, Fantasy, Horror"});
            _data.Add(new Movie { Id = 2, Title = "The Equalizer", Length = TimeSpan.FromMinutes(131), Genre = "Action, Thriller" });
            _data.Add(new Movie { Id = 3, Title = "The Drop", Length = TimeSpan.FromMinutes(106), Genre = "Crime, Drama, Thriller" });
            _data.Add(new Movie { Id = 4, Title = "If I Stay", Length = TimeSpan.FromMinutes(107), Genre = "Drama, Romance" });
            _data.Add(new Movie { Id = 5, Title = "The November Man", Length = TimeSpan.FromMinutes(120), Genre = "Action, Thriller" });
            _data.Add(new Movie { Id = 6, Title = "Guardians of the Galaxy", Length = TimeSpan.FromMinutes(122), Genre = "Action, Sci-fi" });
        }

        public IQueryable<Movie> Get()
        {
            return _data.AsQueryable();
        }

        public IQueryable<Movie> Get(Expression<Func<Movie, bool>> whereExpression)
        {
            return _data.AsQueryable().Where(whereExpression);
        }

        public Movie GetById(int id)
        {
            return _data.FirstOrDefault(m => m.Id == id);
        }
    }
}
