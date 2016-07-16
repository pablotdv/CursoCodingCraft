using Exercicio06Dapper.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using System.Configuration;

namespace Exercicio06Dapper.Controllers
{
    public class DadosController : Controller
    {        
        // GET: Dados
        public ActionResult Index()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoMundialContext"].ToString()))
            {
                var dados = conn.Query<Dados>("select top 100 * from ex06.Dados");

                return View(dados);
            }               
        }
        
        public ActionResult PesquisaPaisAno()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult PesquisaPaisAno(string pais, int primeiroAno, int segundoAno)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoMundialContext"].ToString()))
            {
                string sql = "select * from ex06.Dados where CodigoPais = @Pais and Ano between @PrimeiroAno and @SegundoAno";
                CommandDefinition command = new CommandDefinition(sql, new { Pais = pais, PrimeiroAno = primeiroAno, SegundoAno  = segundoAno });

                var dados = conn.Query<Dados>(command);

                return View("index", dados);
            }
        }

        public ActionResult PesquisaEstatistica()
        {
            return View("PesquisaEstatistica", new List<PesquisaEstatistica>());
        }

        [HttpPost]
        public ActionResult PesquisaEstatistica(string pais, int primeiroAno, int segundoAno)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoMundialContext"].ToString()))
            {
                string sql = @" select b.Nome as Indicador, STDEV(valor) DesvioPadrao, min(valor) Minimo, max(valor) Maximo, avg(valor) Media
                            from ex06.Dados a inner
                            join ex06.Indicadores b on a.CodigoIndicador = b.Codigo
                            where a.CodigoPais = @Pais

                                and Ano between @PrimeiroAno and @SegundoAno
                            group by b.Nome
                            order by Indicador";

                CommandDefinition command = new CommandDefinition(sql, new { Pais = pais, PrimeiroAno = primeiroAno, SegundoAno = segundoAno });

                var dados = conn.Query<PesquisaEstatistica>(command);

                return View("PesquisaEstatistica", dados);
            }
        }
    }
}