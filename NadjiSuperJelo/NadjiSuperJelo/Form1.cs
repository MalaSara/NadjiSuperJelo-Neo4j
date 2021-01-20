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
            client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "autentifikacija");


            try
            {
                client.Connect();

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }
    }
}
