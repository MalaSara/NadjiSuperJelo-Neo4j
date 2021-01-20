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

        //relationships:RATED, ACTS_IN, FRIEND, DIRECTED

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // sara - autentifikacija
            // kate - edukacija
            client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "edukacija");


            try
            {
                client.Connect();

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }

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
            /*List<Autor> autori = ((IRawGraphClient)client).ExecuteGetCypherResults<Autor>(query).ToList();

            foreach (Autor a in autori)
                MessageBox.Show(a.ime);*/

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
    }
}
