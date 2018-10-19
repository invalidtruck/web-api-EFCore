using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        [HttpGet]
        public List<Users> Get() => new UsersContext().Users.ToList();

        [HttpGet("{id}")]
        public Users Get(int id) => new UsersContext().Users.FirstOrDefault(e => e.Id == id);



        // POST api/values
        [HttpPost]
        public object Post([FromBody]Users usr)
        {

            try
            {
                if (usr.Email == null || usr.Name == null || usr.Username == null)
                    return new { error = true, message = "Verificar los parametros enviados" };

                var user = new Users
                {
                    Email = usr.Email,
                    Name = usr.Name,
                    Username = usr.Username
                };
                var ctx = new UsersContext();
                ctx.Users.Add(user);
                ctx.SaveChanges();

            }
            catch (Exception)
            {
                return new
                {
                    error = true,
                    message = "ha ocurrido un error intente nunevamente o contacte con el administrador del sistema"
                };
            }
            return new
            {
                message = "Registro guardado",
                register = usr
            };

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public object Put(int id, [FromBody]Users usr)
        {
            var ctx = new UsersContext();
            var user = ctx.Users.FirstOrDefault(f => f.Id == id);
            if (user == null)
                return new
                {
                    error = true,
                    message = "No existe el usuario"
                };
            var newData = new Users
            {
                Id = id,
                Name = usr.Name ?? user.Name,
                Username = usr.Username ?? user.Username,
                Email = usr.Email ?? user.Email
            };
            ctx.Entry(user).CurrentValues.SetValues(newData);
            ctx.SaveChanges();

            return new
            {
                message = "Se ha actualizado el registro"
            };
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public object Delete(int id)
        {
            var ctx = new UsersContext();
            var user = ctx.Users.FirstOrDefault(w => w.Id == id);
            if (user == null)
                return new
                {
                    error = true,
                    message = "No existe el usuario"
                };
            ctx.Users.Remove(user);
            ctx.SaveChanges();
            return new
            {
                message = "se a eliminado el usuario"
            };
        }
    }
}
