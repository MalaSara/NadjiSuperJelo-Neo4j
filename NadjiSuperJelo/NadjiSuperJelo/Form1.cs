using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NadjiSuperJelo.Model;
using Neo4jClient;
using Neo4jClient.Cypher;


namespace NadjiSuperJelo
{
    public partial class Form1 : Form
    {
        private GraphClient client;

        //relationships:RATED, SASTOJAK

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // sara - autentifikacija
            // kate - edukacija
            GraphClient client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "nadjisuperjelo");


            try
            {
                client.Connect();

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }
        #region Autor
        private void btnDodajAutore_Click(object sender, EventArgs e)
        {
            //for (int i = 1; i <= 5; i++)
            //{
            Autor autor = new Autor();

            autor.idAutor = /*i.ToString();*/"1";
            autor.jelaCijiJeAutor = new List<Jelo>();
            autor.srednjaOcenaAutora = 0;
            autor.ime = "Sara";
            autor.prezime = "Milovanovic";
            autor.brojJela = 0;
            autor.brojOcena = 0;
            //}

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("idAutor", autor.idAutor);
            //queryDict.Add("jelaCijiJeAutor", autor.jelaCijiJeAutor);
            queryDict.Add("srednjaOcenaAutora", autor.srednjaOcenaAutora);
            queryDict.Add("ime", autor.ime);
            queryDict.Add("prezime", autor.prezime);
            queryDict.Add("brojJela", autor.brojJela);

            var query = new Neo4jClient.Cypher.CypherQuery("CREATE (n:Autor {idAutor:'" + autor.idAutor +
                "', srednjaOcenaAutora:" + autor.srednjaOcenaAutora + ", ime:'" + autor.ime + "', prezime:'" +
                autor.prezime + "', brojJela:" + autor.brojJela + "', brojOcena:" + autor.brojOcena + "}) return n",
                queryDict, CypherResultMode.Set);

         


            // !!!! ovde javlja null exception
            List<Autor> autori = ((IRawGraphClient)client).ExecuteGetCypherResults<Autor>(query).ToList();

            foreach (Autor a in autori)
                MessageBox.Show(a.ime);

            this.Close();
        }

        private void btnObrisiAutora_Click(object sender, EventArgs e)
        {
            string unos = nudIdAutora.Value.ToString();

            if (unos != "0")
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("idAutor", unos);

                var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where " +
                    "(n:Autor) and exists(n.idAutor) and n.idAutor =~ {idAutor} detach delete n",
                     queryDict, CypherResultMode.Projection);

                List<Autor> autori = ((IRawGraphClient)client).ExecuteGetCypherResults<Autor>(query).ToList();

                foreach (Autor a in autori)
                {
                    MessageBox.Show(a.idAutor + "\t" + a.ime);
                }
            }
            else
                MessageBox.Show("Niste uneli ispravan id!");
        }

        private void btnIzmeniAutora_Click(object sender, EventArgs e)
        {
            string id = nudIdAutoraZaOcenjivanje.Value.ToString();
            int ocena = (int)numOcena.Value;

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("idAutor", id);

            // izvlacim trenutni broj ocena
            var queryBrojOcena = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Autor) and has(n.idAutor) and " +
                "n.idAutor =~ {idAutor} return n.brojOcena", queryDict, CypherResultMode.Projection);
            int brOcena = Convert.ToInt32(((IRawGraphClient)client).ExecuteGetCypherResults<Autor>(queryBrojOcena));//.ToString());
            //System.Convert.ToInt32(queryBrojOcena);
            brOcena++;

            // izvlacim trenutnu srednju ocenu
            var querySrednjaOcena = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Autor) and has(n.idAutor) and " +
                "n.idAutor =~ {idAutor} return n.srednjaOcenaAutora", queryDict, CypherResultMode.Projection);
            double srednjaOcena = Convert.ToDouble(((IRawGraphClient)client).ExecuteGetCypherResults<Autor>(queryBrojOcena));//.ToString());
            srednjaOcena = (srednjaOcena + ocena) / brOcena;


            queryDict.Add("srednjaOcenaAutora", srednjaOcena);
            queryDict.Add("brojOcena", brOcena);
            // update
            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Autor) and has(n.idAutor) and n.idAutor =~ {idAutor} " +
                "set n.srednjaOcenaAutora = {srednjaOcenaAutora}, n.brojOcena = {brojOcena} return n",
                new Dictionary<string, object>(), CypherResultMode.Set);

            List<Autor> autori = ((IRawGraphClient)client).ExecuteGetCypherResults<Autor>(query).ToList();

            foreach (Autor a in autori)
            {
                MessageBox.Show("Srednja ocena: " + a.srednjaOcenaAutora + "\n" + "Broj ocena: " + a.brojOcena);
            }
        }

        private void btnPronadjiAutoraPoImenu_Click(object sender, EventArgs e)
        {

            string imeAutora = ".*" + txtGetAutori.Text + ".*";

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("ime", imeAutora);

            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Autor) and exists(n.ime) and " +
                "n.ime =~ {ime} return n", queryDict, CypherResultMode.Set);

            List<Autor> autori = ((IRawGraphClient)client).ExecuteGetCypherResults<Autor>(query).ToList();

            foreach (Autor a in autori)
            {
                //DateTime bday = a.getBirthday();
                MessageBox.Show(a.ime);
            }
        }
        #endregion

        #region Jelo
        private void DodajJela_Click(object sender, EventArgs e)
        {
            Jelo jelo = new Jelo();

          /*  jelo.idJelo = "1";
            jelo.autor = "Sara Milovanovic";
            jelo.nazivJela = "Ćufte u paradajz sosu";
            jelo.nacinPripreme = "prženje";
            jelo.Sastojci = new List<Sastojak>();
            jelo.Sastojci.Add("Mleveno meso");
            jelo.Sastojci.Add("Jaja");
            jelo.ocenaJela = "9";
            jelo.brojOcenaJela = 3;*/


            //}

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("idJelo", jelo.idJelo);
            queryDict.Add("autor", jelo.autor);
            queryDict.Add("nazivJela", jelo.nazivJela);
            queryDict.Add("nacinPripreme", jelo.nacinPripreme);
            queryDict.Add("Sastojci", jelo.Sastojci);
            queryDict.Add("ocenaJela", jelo.ocenaJela);
            queryDict.Add("brojOcenaJela", jelo.brojOcenaJela);

            var query = new Neo4jClient.Cypher.CypherQuery("CREATE (n: Jelo {idJelo:'" + jelo.idJelo +
                "', autor:" + jelo.autor + ", nazivJela:'" + jelo.nazivJela + "', nacinPripreme:'" +
                jelo.nacinPripreme + "', Sastojci:" + jelo.Sastojci + "', ocenaJela:" + jelo.ocenaJela + "', brojOcenaJela:" + jelo.brojOcenaJela + "}) return n",
                queryDict, CypherResultMode.Set);

            List<Jelo> jela = ((IRawGraphClient)client).ExecuteGetCypherResults<Jelo>(query).ToList();

            foreach (Jelo j in jela)
                MessageBox.Show(j.nazivJela + " " + j.autor);

            this.Close();
        }

        private void ObrisiJela_Click(object sender, EventArgs e)
        {

            string unos = tboxIdJela.Value.ToString();

            if (unos != "0")
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("idJelo", unos);

                var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where " +
                    "(n:Jelo) and exists(n.idJelo) and n.idJelo =~ {idJelo} detach delete n",
                     queryDict, CypherResultMode.Projection);

                List<Jelo> jela = ((IRawGraphClient)client).ExecuteGetCypherResults<Jelo>(query).ToList();

                foreach (Jelo jelo  in jela)
                {
                    MessageBox.Show(jelo.nazivJela);
                }
            }
            else
                MessageBox.Show("Niste uneli ispravan id!");
        }

        private void IzmeniJelo_Click(object sender, EventArgs e)
        {

            string idjelo = tboxIdJela.Value.ToString();
            int ocenajelo = (int)tboxOcena.Value;

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("idJelo", idjelo);

            

            var queryBrojOcena = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Jelo) and has(n.idJelo) and " +
                "n.idJelo =~ {idJelo} return n.brojOcenaJela", queryDict, CypherResultMode.Projection);
            int brOcena = Convert.ToInt32(((IRawGraphClient)client).ExecuteGetCypherResults<Jelo>(queryBrojOcena));//.ToString());
            brOcena++;

            // izvlacim trenutnu srednju ocenu
            var querySrednjaOcena = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Jelo) and has(n.idJelo) and " +
                "n.idJelo =~ {idJelo} return n.ocenaJela", queryDict, CypherResultMode.Projection);
            double srednjaOcena = Convert.ToDouble(((IRawGraphClient)client).ExecuteGetCypherResults<Autor>(queryBrojOcena));//.ToString());
            srednjaOcena = (srednjaOcena + ocenajelo) / brOcena;


            queryDict.Add("ocenaJela", srednjaOcena);
            queryDict.Add("brojOcenaJela", brOcena);
            // update
            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Jelo) and has(n.idJelo) and n.idJelo =~ {idJelo} " +
                "set n.ocenaJela = {ocenaJela}, n.brojOcenaJela = {brojOcenaJela} return n",
                new Dictionary<string, object>(), CypherResultMode.Set);

            List<Jelo> jela = ((IRawGraphClient)client).ExecuteGetCypherResults<Jelo>(query).ToList();

            foreach (Jelo jelo in jela)
            {
                MessageBox.Show("Ocena: " + jelo.ocenaJela );
            }
        }

        private void PronadjiJeloPoSastojku_Click_1(object sender, EventArgs e)
        {
            string sastojak = ".*" + tboxNadjiJelo.Text + ".*";

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("Sastojak", sastojak);

            var query = new Neo4jClient.Cypher.CypherQuery("start n = node(*) match(n) < -[r: SASTOJAK] - (a)where exists(n.naziv) and n.naziv = ~ { sastojak } return a"
               , queryDict, CypherResultMode.Set);

            List<Sastojak> sastojci = ((IRawGraphClient)client).ExecuteGetCypherResults<Sastojak>(query).ToList();

            foreach (Sastojak sas in sastojci)
            {
                //DateTime bday = a.getBirthday();
                MessageBox.Show(sas.naziv);
            }
        }
        #endregion

        #region Sastojak


        private void btnDodajSastojke_Click(object sender, EventArgs e)
        {

            Sastojak sastojak = new Sastojak();
            sastojak.idSastojak = "1";
            sastojak.naziv = "Brasno";
            sastojak.rateHranljivosti = 7;

            Dictionary<string, object> queryDict1 = new Dictionary<string, object>();
            queryDict1.Add("idSastojak", sastojak.idSastojak);
            queryDict1.Add("naziv", sastojak.naziv);
            queryDict1.Add("rateHranljivosti", sastojak.rateHranljivosti);


            var query = new Neo4jClient.Cypher.CypherQuery("CREATE (n:Sastojak {idSastojak:'" + sastojak.idSastojak +
                    "', naziv:" + sastojak.naziv + ", rateHranljivosti:'" + sastojak.rateHranljivosti +  "}) return n",
                    queryDict1, CypherResultMode.Set);

            var query1 = new Neo4jClient.Cypher.CypherQuery("MATCH (n:Jelo), (p:Sastojak) " +
                                                            "WHERE n.idJelo = '1' AND p.idSastojak = '1'" +
                                                            "CREATE (n)-[r:SADRZI]->(p)", new Dictionary<string, object>(), CypherResultMode.Set);

            var query2 = new Neo4jClient.Cypher.CypherQuery("MATCH (n:Jelo), (p:Sastojak) " +
                                                            "WHERE n.idJelo = '1' AND p.idSastojak = '1'" +
                                                            "CREATE (p)-[r:JE_SADRZAN]->(n)", new Dictionary<string, object>(), CypherResultMode.Set);

        }

        private void btnObrisiSastojak_Click(object sender, EventArgs e)
        {
            string idS = upDownObrisiSastojak.Value.ToString();

            if (idS != "0")
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("idSastojak", idS);

                var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where " +
                    "(n:Sastojak) and exists(n.idSastojak) and n.idSastojak =~ {idSastojak} detach delete n",
                     queryDict, CypherResultMode.Projection);
            }
            else
                MessageBox.Show("Wrong ID!");
        }

        private void btnIzmeniSastojak_Click(object sender, EventArgs e)
        {
            string idSastojak = upDownIDPromena.Value.ToString();
            string naziv = tbNazivSastojka.Text;

            Dictionary<string, object> queryDict1 = new Dictionary<string, object>();
            queryDict1.Add("idSastojak", idSastojak);
            queryDict1.Add("naziv", naziv);

            var query= new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Sastojak) and has(n.idSastojak) and n.idSastojak =~ {idSastojak} " +
                "set n.naziv = {naziv} return n", queryDict1, CypherResultMode.Projection);
        }

        private void btnPretraziSastojak_Click(object sender, EventArgs e)
        {
            string naziv = ".*" + tbNazivSastojka + ".*";
            Dictionary<string, object> queryDict1 = new Dictionary<string, object>();
            queryDict1.Add("naziv", naziv);

            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Sastojak) and exists(n.naziv) and n.naziv =~ {naziv} return n",
                                                      queryDict1, CypherResultMode.Set);

            List<Sastojak> sastojci = ((IRawGraphClient)client).ExecuteGetCypherResults<Sastojak>(query).ToList();


            foreach (Sastojak s in sastojci)
            {
                MessageBox.Show(s.naziv);
            }
        }

        #endregion

        #region Kategorija

        private void btnDodajKategoriju_Click(object sender, EventArgs e)
        {
            Kategorija kategorija = new Kategorija();
            kategorija.idKategorija = "1";
            kategorija.naziv = "Predjelo";

            Dictionary<string, object> queryDict1 = new Dictionary<string, object>();
            queryDict1.Add("idKategorija", kategorija.idKategorija);
            queryDict1.Add("naziv", kategorija.naziv);


            var query = new Neo4jClient.Cypher.CypherQuery("CREATE (n:Kategorija {idKategorija:'" + kategorija.idKategorija +
                    "', naziv:" + kategorija.naziv + "}) return n",
                    queryDict1, CypherResultMode.Set);

            var query1 = new Neo4jClient.Cypher.CypherQuery("MATCH (n:Jelo), (p:Kategorija) " +
                                                            "WHERE n.idJelo = '1' AND p.idKategorija = '1'" +
                                                            "CREATE (n)-[r:SPADA_U]->(p)", new Dictionary<string, object>(), CypherResultMode.Set);

            var query2 = new Neo4jClient.Cypher.CypherQuery("MATCH (n:Jelo), (p:Kategorija) " +
                                                            "WHERE n.idJelo = '1' AND p.idKategorija = '1'" +
                                                            "CREATE (p)-[r:PRIPADA]->(n)", new Dictionary<string, object>(), CypherResultMode.Set);
        }

        private void btnBrisiKategoriju_Click(object sender, EventArgs e)
        {
            string idK = upDownIdBrisiKat.Value.ToString();

            if (idK != "0")
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("idKategorija", idK);

                var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where " +
                    "(n:Kategorija) and exists(n.idKategorija) and n.idKategorija =~ {idKategorija} detach delete n",
                     queryDict, CypherResultMode.Projection);
            }
            else
                MessageBox.Show("Wrong ID!");
        }

        private void btnIzmeniKategoriju_Click(object sender, EventArgs e)
        {
            string idKategorija = upDownKategorija.Value.ToString();
            string naziv = tbNazivKategorije.Text;

            Dictionary<string, object> queryDict1 = new Dictionary<string, object>();
            queryDict1.Add("idKategorija", idKategorija);
            queryDict1.Add("naziv", naziv);

            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Kategorija) and has(n.idKategorija) and n.idKategorija =~ {idKategorija} " +
                "set n.naziv = {naziv} return n", queryDict1, CypherResultMode.Projection);
        }

        private void btnPretraziKat_Click(object sender, EventArgs e)
        {
            string naziv = ".*" + tbNazivKategorije + ".*";
            Dictionary<string, object> queryDict1 = new Dictionary<string, object>();
            queryDict1.Add("naziv", naziv);

            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Kategorija) and exists(n.naziv) and n.naziv =~ {naziv} return n",
                                                      queryDict1, CypherResultMode.Set);

            List<Kategorija> kategorije = ((IRawGraphClient)client).ExecuteGetCypherResults<Kategorija>(query).ToList();


            foreach (Kategorija k in kategorije)
            {
                MessageBox.Show(k.naziv);
            }
        }

        #endregion
    }
}
