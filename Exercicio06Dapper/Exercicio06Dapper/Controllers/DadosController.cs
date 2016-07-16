using Exercicio06Dapper.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using System.Configuration;
using System.Web.UI;
using Google.DataTable.Net.Wrapper;

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
                CommandDefinition command = new CommandDefinition(sql, new { Pais = pais, PrimeiroAno = primeiroAno, SegundoAno = segundoAno });

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

        public ActionResult Graficos()
        {
            return View("Graficos");
        }

        [OutputCache(Location = OutputCacheLocation.None)]
        public string GraficoPaisIndicadorAno(string indicador, int primeiroAno, int segundoAno)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoMundialContext"].ToString()))
            {
                string sql = @" select Ano, Valor, CodigoPais
                                from ex06.Dados
                                where CodigoIndicador = @Indicador
                                    and Ano between @PrimeiroAno and @SegundoAno
                                order by Ano";

                CommandDefinition command = new CommandDefinition(sql, new { Indicador = indicador, PrimeiroAno = primeiroAno, SegundoAno = segundoAno });

                var dados = conn.Query<GraficoAno>(command);

                DataTable dt = new DataTable();

                //Act -----------------
                dt.AddColumn(new Column(ColumnType.String, "Ano"));
                dt.AddColumn(new Column(ColumnType.Number, "Brasil"));
                dt.AddColumn(new Column(ColumnType.Number, "Suécia"));

                var anos = dados.Select(a => a.Ano).Distinct();

                foreach (var ano in anos)
                {                    
                    var brasil = dados.Where(a => a.Ano == ano && a.CodigoPais == "BRA").Select(a => a.Valor).FirstOrDefault();
                    var suecia = dados.Where(a => a.Ano == ano && a.CodigoPais == "SWE").Select(a => a.Valor).FirstOrDefault();

                    Cell c0 = new Cell(ano.ToString(), null);
                    Cell c1 = new Cell(0, null);
                    Cell c2 = new Cell(0, null);

                    c2.Value = brasil;

                    c1.Value = suecia;


                    var row1 = dt.NewRow();
                    row1.AddCellRange(new[] { c0, c2, c1 });
                    dt.AddRow(row1);
                }

                var json = dt.GetJson();

                return json;
            }





        }

    }
}