using Mikroszimuláció.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
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

            for (int year = 2005; year <= 2024; year++)
            {
                // Végigmegyünk az összes személyen
                for (int i = 0; i < Population.Count; i++)
                {
                    // Ide jön a szimulációs lépés
                }

                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();
                Console.WriteLine(
                    string.Format("Év:{0} Fiúk:{1} Lányok:{2}", year, nbrOfMales, nbrOfFemales));
            }
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



        private void SimStep(int year, Person person)
        {
            //Ha halott akkor kihagyjuk, ugrunk a ciklus következő lépésére
            if (!person.IsAlive) return;

            // Letároljuk az életkort, hogy ne kelljen mindenhol újraszámolni
            int age = (int)(year - person.BirthYear);

            // Halál kezelése
            // Halálozási valószínűség kikeresése
            double pDeath = (from x in DeathProbabilities
                             where x.Gender == person.Gender && x.Age == age
                             select x.DeathProb).FirstOrDefault();
            // Meghal a személy?
            if (rnd.NextDouble() <= pDeath)
                person.IsAlive = false;

            //Születés kezelése - csak az élő nők szülnek
            if (person.IsAlive && person.Gender == Gender.Female)
            {
                //Szülési valószínűség kikeresése
                double pBirth = (from x in BirthProbabilities
                                 where x.Age == age && x.NbrOfChildren == person.NbrOfChildren
                                 select x.Prob).FirstOrDefault();
                //Születik gyermek?
                if (rnd.NextDouble() <= pBirth)
                {
                    Person újszülött = new Person();
                    újszülött.BirthYear = year;
                    újszülött.NbrOfChildren = 0;
                    újszülött.Gender = (Gender)(rnd.Next(1, 3));
                    Population.Add(újszülött);
                }
            }
        }





    }
}
