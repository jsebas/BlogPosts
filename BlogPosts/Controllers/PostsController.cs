using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ConexionBDD;

namespace BlogPosts.Controllers
{
    public class PostsController : ApiController
    {
        private BDDPostEntities dbPost = new BDDPostEntities();

        //Visualizar Posts
        [HttpGet]
        public IEnumerable<Post> Get()
        {
            using (BDDPostEntities postentidades = new BDDPostEntities())
            {
                return postentidades.Posts.ToList();
            }
        }

        //Visualizar solamente UN Post
        [HttpGet]
        public Post Get(int Id)
        {
            using (BDDPostEntities postentidades = new BDDPostEntities())
            {
                return postentidades.Posts.FirstOrDefault(e => e.IdPost == Id);
            }
        }

        //Crear nuevo Post en la BDD
        [HttpPost]
        public IHttpActionResult CrearPost([FromBody]Post pst)
        {
            if (ModelState.IsValid)
            {
                dbPost.Posts.Add(pst);
                dbPost.SaveChanges();

                return Ok(pst);
            }
            else
            {
                return BadRequest();
            }
        }

        //Modificar Post
        [HttpPut]
        public IHttpActionResult ModificarPost(int Id, [FromBody]Post pst)
        {
            if (ModelState.IsValid)
            {
                var postExistente = dbPost.Posts.Count(c => c.IdPost ==Id) > 0;

                if (postExistente)
                {
                    dbPost.Entry(pst).State = EntityState.Modified;
                    dbPost.SaveChanges();

                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        //Eliminar Post
        [HttpDelete]
        public IHttpActionResult EliminarPost(int Id)
        {
            var pst = dbPost.Posts.Find(Id);

            if (pst != null)
            {
                dbPost.Posts.Remove(pst);
                dbPost.SaveChanges();

                return Ok(pst);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
