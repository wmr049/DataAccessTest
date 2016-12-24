using EFCodeFirstSample.DataContexts;
using EFCodeFirstSample.Model;
using System;
using System.Linq;

namespace EFCodeFirstSample
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Objetos
            var informatica = new Categoria { Id = 1, Titulo = "Informática" };
            var artesMarciais = new Categoria { Id = 2, Titulo = "Artes Marciais" };
            var ciencias = new Categoria { Id = 3, Titulo = "Ciências" };

            var andre = new Autor { Id = 1, Nome = "André Baltieri" };
            var bruce = new Autor { Id = 2, Nome = "Bruce Wayne" };
            var peter = new Autor { Id = 3, Nome = "Peter Parker" };
            var tony = new Autor { Id = 4, Nome = "Tony Stark" };

            var devapi = new Livro
            {
                Id = 1,
                Titulo = "Desenvolvendo APIs com WebApi",
                CategoriaId = 1
            };
            var ninjitsu = new Livro
            {
                Id = 2,
                Titulo = "Os segredos do Ninjitsu",
                CategoriaId = 2
            };
            var aranhas = new Livro
            {
                Id = 3,
                Titulo = "O segredo das aranhas",
                CategoriaId = 3
            };
            var robotica = new Livro
            {
                Id = 4,
                Titulo = "Robótica avançada",
                CategoriaId = 3
            };

            var andrebaltieri = new User
            {
                Email = "andrebaltieri@hotmail.com",
                IsActive = true,
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "andrebaltieri",
                Password = "andrebaltieri"
            };

            var batman = new User
            {
                Email = "bruce.wayne@hotmail.com",
                IsActive = true,
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "andrebaltieri",
                Password = "batman"
            };

            #endregion

            #region Context
            using (AppDataContext db = new AppDataContext())
            {
                db.Categorias.Add(informatica);
                db.Categorias.Add(artesMarciais);
                db.Categorias.Add(ciencias);

                db.Autores.Add(andre);
                db.Autores.Add(bruce);
                db.Autores.Add(peter);
                db.Autores.Add(tony);

                db.Livros.Add(devapi);
                db.Livros.Add(ninjitsu);
                db.Livros.Add(aranhas);
                db.Livros.Add(robotica);

                devapi.Autores.Add(andre);
                devapi.Autores.Add(tony);
                devapi.Autores.Add(bruce);

                ninjitsu.Autores.Add(bruce);
                devapi.Autores.Add(tony);

                aranhas.Autores.Add(peter);
                devapi.Autores.Add(tony);

                robotica.Autores.Add(tony);

                db.SaveChanges();
            }
            #endregion

            #region Categorias
            using (AppDataContext db = new AppDataContext())
            {
                Console.WriteLine("Categorias");
                foreach (Categoria categoria in db.Categorias)
                {
                    Console.WriteLine(
                        String.Format("{0} - {1}",
                            categoria.Id,
                            categoria.Titulo));
                }
            }
            #endregion

            Console.WriteLine(Environment.NewLine);

            #region Autores
            using (AppDataContext db = new AppDataContext())
            {
                Console.WriteLine("Autores");
                foreach (Autor autor in db.Autores)
                {
                    Console.WriteLine(
                        String.Format("{0} - {1}",
                            autor.Id,
                            autor.Nome));
                }
            }
            #endregion

            Console.WriteLine(Environment.NewLine);

            #region Livros
            using (AppDataContext db = new AppDataContext())
            {
                Console.WriteLine("Livros");
                foreach (Livro livro in db.Livros)
                {
                    Console.WriteLine(
                        String.Format("{0} - {1}",
                            livro.Id,
                            livro.Titulo));
                }
            }
            #endregion

            Console.WriteLine(Environment.NewLine);

            #region Tudo
            using (AppDataContext db = new AppDataContext())
            {
                Console.WriteLine("Tudo");
                foreach (Categoria categoria in
                    db.Categorias.Include("Livros")
                                 .Include("Livros.Autores")
                                 .ToList())
                {
                    Console.WriteLine("Categoria: " + categoria.Titulo);
                    foreach (Livro livro in categoria.Livros)
                    {
                        Console.WriteLine("\tLivro: " + livro.Titulo);
                        foreach (Autor autor in livro.Autores)
                        {
                            Console.WriteLine("\t\tAutor: " + autor.Nome);
                        }
                    }
                    Console.WriteLine(Environment.NewLine);
                }
            }
            #endregion

            #region Usuarios
            using (AppDataContext db = new AppDataContext())
            {
                db.Users.Add(andrebaltieri);
                db.Users.Add(batman);
                db.SaveChanges();

                foreach (User user in db.Users)
                {
                    Console.WriteLine(String.Format("{0} - {1}", user.Id, user.Email));
                }

                Console.WriteLine(
                    String.Format("{0} - {1}",
                    db.Users.Find(batman.Id).Id,
                    db.Users.Find(batman.Id).Email));
            }
            #endregion

            Console.ReadKey();
        }
    }
}
