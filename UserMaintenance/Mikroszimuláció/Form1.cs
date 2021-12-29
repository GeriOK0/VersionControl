using Mikroszimuláció.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Mikroszimuláció
{

    
    public partial class Form1 : Form
    {


        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();

        Random rnd = new Random(23);

        public Form1()
        {
            InitializeComponent();

            Population = GetPopulation(@"C:\Temp\nép.csv");
            BirthProbabilities = GetBirthProbabilities(@"C:\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Temp\halál.csv");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public List<Person> GetPopulation(string csvpath)
        {
            List<Person> list = new List<Person>();

            StreamReader reader = new StreamReader(csvpath);

            string line;

            while ((line = reader.ReadLine()) != null)
            {

                string[] arr = line.Split(',');

                Person it = new Person();
                it.BirthYear = Convert.ToInt32(arr[0].Trim());
                it.Gender = (Gender)Enum.Parse(typeof(Gender), arr[1]);
                it.NbrOfChildren = Convert.ToInt32(arr[2].Trim());

                list.Add(it);
            }
            
            return list;
        }

        public List<BirthProbability> GetBirthProbabilities(string csvpath)
        {
            List<BirthProbability> list = new List<BirthProbability>();

            StreamReader reader = new StreamReader(csvpath);

            string line;

            while ((line = reader.ReadLine()) != null)
            {

                string[] arr = line.Split(',');

                BirthProbability it = new BirthProbability();
                it.Age = Convert.ToInt32(arr[0].Trim());                
                it.NbrOfChildren = Convert.ToInt32(arr[1].Trim());
                it.Prob = Convert.ToDouble(arr[2].Trim());

                list.Add(it);
            }

            return list;
        }

        public List<DeathProbability> GetDeathProbabilities(string csvpath)
        {
            List<DeathProbability> list = new List<DeathProbability>();

            StreamReader reader = new StreamReader(csvpath);

            string line;

            while ((line = reader.ReadLine()) != null)
            {

                string[] arr = line.Split(',');

                DeathProbability it = new DeathProbability();
                it.Gender = (Gender)Enum.Parse(typeof(Gender), arr[0]);
                it.Age = Convert.ToInt32(arr[1].Trim());
                it.DeathProb = Convert.ToDouble(arr[2].Trim());

                list.Add(it);
            }

            return list;
        }
    }
}
