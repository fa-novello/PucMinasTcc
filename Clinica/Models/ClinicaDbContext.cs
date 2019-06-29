using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.IO;
using System.Web;

namespace Clinica.Models
{
    public class ClinicaDbContext : DbContext
    {
        public ClinicaDbContext() : base("ClinicaDbContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer<ClinicaDbContext>(new UniDBInitializer<ClinicaDbContext>());
        }

        public DbSet<Agenda> Agenda { get; set; }
        public DbSet<AgendaConfiguracao> AgendaConfiguracao { get; set; }
        public DbSet<CID> CID { get; set; }
        public DbSet<Colaborador> Colaborador { get; set; }
        public DbSet<Consulta> Consulta { get; set; }
        public DbSet<Especialidade> Especialidade { get; set; }
        public DbSet<Exame> Exame { get; set; }
        public DbSet<LoginModel> LoginModels { get; set; }
        public DbSet<Medicamento> Medicamento { get; set; }
        public DbSet<Medico> Medico { get; set; }
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Consulta>()
            //       .HasRequired(j => j.Paciente)
            //       .WithMany()
            //       .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Consulta>()
            //       .HasRequired(j => j.Medico)
            //       .WithMany()
            //       .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Agenda>()
            //       .HasOptional(j => j.Paciente);

            //modelBuilder.Entity<Medico>()
            //       .HasRequired(j => j.Especialidade)
            //       .WithMany()
            //       .WillCascadeOnDelete(false);
            
            base.OnModelCreating(modelBuilder);
        }

        //toda vez que altera o código a base é deletada, para facilitar os testes serão criados cadastros falsos
        //private class UniDBInitializer<T> : DropCreateDatabaseAlways<ClinicaDbContext>
        private class UniDBInitializer<T> : DropCreateDatabaseIfModelChanges<ClinicaDbContext>
        {
            protected override void Seed(ClinicaDbContext dbContext)
            {
                string line;
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = baseDir + "/App_Data/CID.SQL";


                //using (StreamReader file = new StreamReader(filePath))
                //{
                //    while ((line = file.ReadLine()) != null)
                //    {
                //        if (!string.IsNullOrEmpty(line))
                //        {
                //            dbContext.Database.ExecuteSqlCommand(line);
                //        }
                //    }
                //}

                dbContext.CID.Add(new CID()
                {
                    Id = 1,
                    Descricao = "Cólera devida a Vibrio cholerae 01, biótipo cholerae",
                    Codigo = "A00.0"
                });

                dbContext.CID.Add(new CID()
                {
                    Id = 2,
                    Descricao = "Cólera devida a Vibrio cholerae 01, biótipo El Tor",
                    Codigo = "A00.1"
                });

                dbContext.CID.Add(new CID()
                {
                    Id = 3,
                    Descricao = "Cólera não especificada",
                    Codigo = "A00.9"
                });

                dbContext.CID.Add(new CID()
                {
                    Id = 4,
                    Descricao = "Febre tifóide",
                    Codigo = "A01.0"
                });


                //--------------------------------------------------

                //filePath = baseDir + "/App_Data/especialidades.SQL";

                //using (StreamReader file = new StreamReader(filePath))
                //{
                //    while ((line = file.ReadLine()) != null)
                //    {
                //        if (!string.IsNullOrEmpty(line))
                //        {
                //            dbContext.Database.ExecuteSqlCommand(line);
                //        }
                //    }
                //}

                dbContext.Especialidade.Add(new Especialidade()
                {
                    Id = 1,
                    Descricao = "Anestesiologia"
                });

                dbContext.Especialidade.Add(new Especialidade()
                {
                    Id = 2,
                    Descricao = "Cardiologia"
                });

                dbContext.Especialidade.Add(new Especialidade()
                {
                    Id = 3,
                    Descricao = "Cirurgia geral"
                });

                dbContext.Especialidade.Add(new Especialidade()
                {
                    Id = 4,
                    Descricao = "Cirurgia pediatrica"
                });

                dbContext.Especialidade.Add(new Especialidade()
                {
                    Id = 5,
                    Descricao = "Cirurgia vascular"
                });

                //--------------------------------------------------

                //filePath = baseDir + "/App_Data/medicamentos.SQL";

                //using (StreamReader file = new StreamReader(filePath))
                //{
                //    while ((line = file.ReadLine()) != null)
                //    {
                //        if (!string.IsNullOrEmpty(line))
                //        {
                //            dbContext.Database.ExecuteSqlCommand(line);
                //        }
                //    }
                //}

                dbContext.Medicamento.Add(new Medicamento() {
                    Id = 1,
                    Fabricante = "ABBOTT",
                    FabricanteNome = "PROLONA",
                    GenericoNome = "Prednisolona 100 ml"
                });

                dbContext.Medicamento.Add(new Medicamento()
                {
                    Id = 2,
                    Fabricante = "NECAMIN",
                    FabricanteNome = "Solucortef",
                    GenericoNome = "Hidrocortisona 100 mg"
                });

                dbContext.Medicamento.Add(new Medicamento()
                {
                    Id = 3,
                    Fabricante = "JANSSEN-CILAG",
                    FabricanteNome = "PROTAMINA",
                    GenericoNome = "Protamina 1000 UI/ml"
                });

                dbContext.Medicamento.Add(new Medicamento()
                {
                    Id = 4,
                    Fabricante = "CRISTALIA",
                    FabricanteNome = "Dolasal",
                    GenericoNome = "Meperidina 50 mg/ml"
                });

                dbContext.Medicamento.Add(new Medicamento()
                {
                    Id = 5,
                    Fabricante = "CRISTALIA",
                    FabricanteNome = "Nubain",
                    GenericoNome = "Nalburfina 10mg"
                });

                //--------------------------------------------------
                //--------------------------------------------------
                //--------------------------------------------------


                dbContext.Paciente.Add(new Paciente()
                {
                    Id = 1,
                    Nome = "Paciente 1",
                    Nascimento = DateTime.Today.AddYears(-20).AddDays(5),
                    CPF = "111.111.111-11",
                    Telefone = "(12)99999-9999",
                    Email = "paciente1@teste.com",
                    Perfil = PerfilEnumerator.Paciente,
                    Senha = "12345",
                    Login = "pac1",

                    PossuiConvenio = false
                });

                dbContext.Paciente.Add(new Paciente()
                {
                    Id = 2,
                    Nome = "Paciente 2",
                    Nascimento = DateTime.Today.AddYears(-23).AddDays(-20),
                    CPF = "222.222.222-22",
                    Telefone = "(12)99999-9999",
                    Email = "paciente2@teste.com",
                    Perfil = PerfilEnumerator.Paciente,
                    Senha = "12345",
                    Login = "pac2",

                    PossuiConvenio = true,
                    EmpresaConvenio = "Unimed",
                    ValidadeConvenio = DateTime.Today.AddMonths(5),
                    NumeroCNS = "111 222 333"
                });

                //--------------------------------------------------

                List<Medico> medico = new List<Medico>();

                medico.Add(new Medico()
                {
                    Id = 3,
                    Nome = "Medico 1",
                    Nascimento = DateTime.Today.AddYears(-37),
                    CPF = "333.333.333-33",
                    Telefone = "(12)99999-9999",
                    Email = "medico1@teste.com",
                    Perfil = PerfilEnumerator.Médico,
                    Senha = "12345",
                    Login = "med1",

                    CRMEmissor = "SP",
                    CRMNumero = "111111",
                    EspecialidadeId = 1
                });

                medico.Add(new Medico()
                {
                    Id = 4,
                    Nome = "Medico 2",
                    Nascimento = DateTime.Today.AddYears(-35),
                    CPF = "444.444.444-44",
                    Telefone = "(12)99999-9999",
                    Email = "medico2@teste.com",
                    Perfil = PerfilEnumerator.Médico,
                    Senha = "12345",
                    Login = "med2",

                    CRMEmissor = "SP",
                    CRMNumero = "22222",
                    EspecialidadeId = 2
                });

                medico.Add(new Medico()
                {
                    Id = 5,
                    Nome = "Medico 3",
                    Nascimento = DateTime.Today.AddYears(-30),
                    CPF = "444.444.444-55",
                    Telefone = "(12)99999-9999",
                    Email = "medico3@teste.com",
                    Perfil = PerfilEnumerator.Médico,
                    Senha = "12345",
                    Login = "med3",

                    CRMEmissor = "SP",
                    CRMNumero = "33333",
                    EspecialidadeId = 4
                });

                foreach (var item in medico)
                {
                    dbContext.Medico.Add(item);
                }

                //--------------------------------------------------

                dbContext.Colaborador.Add(new Colaborador()
                {
                    Id = 6,
                    Nome = "Laboratório 1",
                    Nascimento = DateTime.Today.AddYears(-25).AddDays(5),
                    CPF = "111.222.111-11",
                    Telefone = "(12)99999-9999",
                    Email = "laboratorio1@teste.com",
                    Perfil = PerfilEnumerator.Laboratório,
                    Senha = "12345",
                    Login = "lab1"
                });

                //--------------------------------------------------

                dbContext.Colaborador.Add(new Colaborador()
                {
                    Id = 7,
                    Nome = "Colaborador 1",
                    Nascimento = DateTime.Today.AddYears(-20).AddDays(25),
                    CPF = "111.333.111-11",
                    Telefone = "(12)99999-9999",
                    Email = "colaborador1@teste.com",
                    Perfil = PerfilEnumerator.Secretária,
                    Senha = "12345",
                    Login = "colab1"
                });

                dbContext.Colaborador.Add(new Colaborador()
                {
                    Id = 8,
                    Nome = "Colaborador 2",
                    Nascimento = DateTime.Today.AddYears(-22).AddDays(15),
                    CPF = "111.333.111-11",
                    Telefone = "(12)99999-9999",
                    Email = "colaborador2@teste.com",
                    Perfil = PerfilEnumerator.Secretária,
                    Senha = "12345",
                    Login = "colab2"
                });

                //--------------------------------------------------


                dbContext.Exame.Add(new Exame()
                {
                    Id = 1,
                    Codigo = "0.10",
                    Descricao = "Raio X torax"
                });

                dbContext.Exame.Add(new Exame()
                {
                    Id = 2,
                    Codigo = "1.11",
                    Descricao = "Ultrassom da região abdominal"
                });

                dbContext.Exame.Add(new Exame()
                {
                    Id = 3,
                    Codigo = "2.1",
                    Descricao = "Endoscopia"
                });

                dbContext.Exame.Add(new Exame()
                {
                    Id = 4,
                    Codigo = "2.5",
                    Descricao = "Broncoscopia"
                });

                //--------------------------------------------------

                dbContext.AgendaConfiguracao.Add(new AgendaConfiguracao()
                {
                    MedicoId = 3,
                    Medico = "Medico 1",
                    DiaSemana = DayOfWeek.Monday,
                    LimiteConsultas = 5,
                    TempoConsulta = 20,
                    HorarioInicio = DateTime.Parse("9:00:00")
                });

                dbContext.AgendaConfiguracao.Add(new AgendaConfiguracao()
                {
                    MedicoId = 3,
                    Medico = "Medico 1",
                    DiaSemana = DayOfWeek.Wednesday,
                    LimiteConsultas = 5,
                    TempoConsulta = 20,
                    HorarioInicio = DateTime.Parse("9:15:00")
                });

                dbContext.AgendaConfiguracao.Add(new AgendaConfiguracao()
                {
                    MedicoId = 4,
                    Medico = "Medico 2",
                    DiaSemana = DayOfWeek.Tuesday,
                    LimiteConsultas = 7,
                    TempoConsulta = 15,
                    HorarioInicio = DateTime.Parse("9:30:00")
                });

                dbContext.AgendaConfiguracao.Add(new AgendaConfiguracao()
                {
                    MedicoId = 4,
                    Medico = "Medico 2",
                    DiaSemana = DayOfWeek.Thursday,
                    LimiteConsultas = 7,
                    TempoConsulta = 15,
                    HorarioInicio = DateTime.Parse("10:00:00")
                });

                //--------------------------------------------------

                dbContext.Agenda.Add(new Agenda()
                {
                    MedicoId = 3,
                    Data = DateTime.Now.Date, //DateTime.Parse("15/04/2019"),
                    Horario = DateTime.Parse("13:00:00"),
                    Confirmada = false
                });
                dbContext.Agenda.Add(new Agenda()
                {
                    MedicoId = 3,
                    Data = DateTime.Now.Date, //DateTime.Parse("15/04/2019"),
                    Horario = DateTime.Parse("13:20:00"),
                    Confirmada = false
                });
                dbContext.Agenda.Add(new Agenda()
                {
                    MedicoId = 3,
                    Data = DateTime.Now.Date, //DateTime.Parse("15/04/2019"),
                    Horario = DateTime.Parse("14:00:00"),
                    Confirmada = false
                });
                dbContext.Agenda.Add(new Agenda()
                {
                    MedicoId = 3,
                    Data = DateTime.Now.Date, //DateTime.Parse("15/04/2019"),
                    Horario = DateTime.Parse("16:00:00"),
                    PacienteId = 1,
                    Confirmada = false
                });

                dbContext.Agenda.Add(new Agenda()
                {
                    MedicoId = 3,
                    Data = DateTime.Now.Date, //DateTime.Parse("20/04/2019"),
                    Horario = DateTime.Parse("15:00:00"),
                    Confirmada = false
                });

                dbContext.Agenda.Add(new Agenda()
                {
                    MedicoId = 4,
                    Data = DateTime.Now.Date.AddDays(5), //DateTime.Parse("15/04/2019"),
                    Horario = DateTime.Parse("13:20:00"),
                    Confirmada = false
                });

                dbContext.Agenda.Add(new Agenda()
                {
                    MedicoId = 4,
                    Data = DateTime.Now.Date, //DateTime.Parse("20/04/2019"),
                    Horario = DateTime.Parse("15:20:00"),
                    Confirmada = false
                });

                dbContext.Agenda.Add(new Agenda()
                {
                    MedicoId = 4,
                    Data = DateTime.Now.Date.AddDays(3), //DateTime.Parse("20/04/2019"),
                    Horario = DateTime.Parse("16:20:00"),
                    Confirmada = false
                });

                dbContext.Agenda.Add(new Agenda()
                {
                    MedicoId = 4,
                    PacienteId = 1,
                    Data = DateTime.Now.Date.AddDays(3), //DateTime.Parse("20/04/2019"),
                    Horario = DateTime.Parse("16:45:00"),
                    Confirmada = true,
                    ConsultaCriada = true
                });

                //--------------------------------------------------

                dbContext.Consulta.Add(new Consulta()
                {
                    Id = 1,
                    MedicoId = 4,
                    PacienteId = 1,
                    Data = DateTime.Now.Date.AddDays(3),
                    Horario = DateTime.Parse("16:45:00"),
                    Anotacoes = "anotações 1",
                    Realizada = true
                });

                dbContext.ConsultaExames.Add(new ConsultaExame()
                {
                    ConsultaId = 1,
                    ExameId = 2
                });

                dbContext.ConsultaExames.Add(new ConsultaExame()
                {
                    ConsultaId = 1,
                    ExameId = 3
                });

                dbContext.ConsultaExames.Add(new ConsultaExame()
                {
                    ConsultaId = 1,
                    ExameId = 1,
                    Realizado = true,
                    Laudo = "aaaaa",
                    DataLiberacaoExame = DateTime.Now.Date
                });

                dbContext.ConsultaMedicamentos.Add(new ConsultaMedicamento()
                {
                    ConsultaId = 1,
                    MedicamentoId = 1,
                    Posologia = "Tomar 1 comprimido ao dia após o almoço por 10 dias"
                });

                dbContext.ConsultaMedicamentos.Add(new ConsultaMedicamento()
                {
                    ConsultaId = 1,
                    MedicamentoId = 4,
                    Posologia = "Tomar 1 comprimido após o café da manhã por 5 dias"
                });

                dbContext.Consulta.Add(new Consulta()
                {
                    MedicoId = 4,
                    PacienteId = 1,
                    Data = DateTime.Now.Date, //DateTime.Parse("20/04/2019"),
                    Horario = DateTime.Parse("15:20:00"),
                    Realizada = false
                });

                dbContext.Consulta.Add(new Consulta()
                {
                    MedicoId = 4,
                    PacienteId = 1,
                    Data = DateTime.Now.Date, //DateTime.Parse("20/04/2019"),
                    Horario = DateTime.Parse("14:20:00"),
                    Realizada = false
                });

                dbContext.Consulta.Add(new Consulta()
                {
                    MedicoId = 4,
                    PacienteId = 1,
                    Data = DateTime.Now.Date, //DateTime.Parse("20/04/2019"),
                    Horario = DateTime.Parse("10:20:00"),
                    Realizada = false
                });

                dbContext.Consulta.Add(new Consulta()
                {
                    MedicoId = 4,
                    PacienteId = 1,
                    Data = DateTime.Now.Date.AddDays(1), //DateTime.Parse("20/04/2019"),
                    Horario = DateTime.Parse("10:20:00"),
                    Realizada = false
                });

                dbContext.Consulta.Add(new Consulta()
                {
                    MedicoId = 4,
                    PacienteId = 1,
                    Data = DateTime.Now.Date.AddDays(-100), //DateTime.Parse("20/04/2019"),
                    Horario = DateTime.Parse("10:20:00"),
                    Anotacoes = "teste de anotações da consulta",
                    Realizada = true
                });

                dbContext.Consulta.Add(new Consulta()
                {
                    MedicoId = 4,
                    PacienteId = 1,
                    Data = DateTime.Now.Date.AddDays(-1e0), //DateTime.Parse("20/04/2019"),
                    Horario = DateTime.Parse("10:20:00"),
                    Anotacoes = "Teste de anotações da consulta. /n Linha2.",
                    Realizada = true
                });
                //--------------------------------------------------
                base.Seed(dbContext);
            }
        }

        public System.Data.Entity.DbSet<Clinica.Models.AgendaViewModel> AgendaViewModels { get; set; }

        public System.Data.Entity.DbSet<Clinica.Models.ConsultaExame> ConsultaExames { get; set; }

        public System.Data.Entity.DbSet<Clinica.Models.ConsultaMedicamento> ConsultaMedicamentos { get; set; }

        public System.Data.Entity.DbSet<Clinica.Models.Relatorio> Relatorios { get; set; }

        public System.Data.Entity.DbSet<Clinica.Models.ReceitaRelatorio> ReceitaRelatorios { get; set; }
    }
}