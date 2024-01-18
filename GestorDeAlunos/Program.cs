using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using GestorDeAlunos;

// in GestorDeAlunos //
// In = Identification number or in portuguese In = Número de identificação//

namespace GestorDeAlunos
{
    internal class Aluno
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string In { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Course { get; set; }

    }
}

internal class ApplicationContext : DbContext
{
    public DbSet<Aluno> Alunos { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("ConexionString");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluno>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        using (var context = new ApplicationContext())
        {
            //Operetion Create
            var NovoAluno = new Aluno
            {
                Name = "Yuri Martin",
                Age = 20,
                In = "205.554.308-65",
                Email = "YuriMartins@crud.com",
                Phone = "(81)98965-3557",
                Course = "Direito"
            };
            //Operetion Read
            var alunos = context.Alunos.ToList();
            Console.WriteLine("Lista de alunos");
            foreach (var aluno in alunos) 
            {
                Console.WriteLine($"{aluno.Id},\n{aluno.Name},\n{aluno.Age},\n{aluno.In},\n{aluno.Email},\n{aluno.Phone},\n{aluno.Course},\n");
            }
            //Operetion Update 
            var AtualizarAluno = context.Alunos.Find(1);
            if (AtualizarAluno != null)
            {
                AtualizarAluno.Age = 21;
                AtualizarAluno.Course = "Desenvolvedor de SoftWare";
            }
            //Operetion Delete
            var ExcluirAluno = context.Alunos.Find(1);
            if (ExcluirAluno != null)
            {
                context.Alunos.Remove(ExcluirAluno);
                context.SaveChanges();
            }
        }
    }
}