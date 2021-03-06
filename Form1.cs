using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DosyaOrganizasyonu
{
    public partial class Form1 : Form
    {
        public static Random random = new Random();
        public static int lenghtofArray;
        public static int[] array;
        public static int lenghtofTable;
        public static int[] LischKeys;
        public static int[] LischLinks;
        public static int[] EischKeys;
        public static int[] EischLinks;
        public static int[] EichKeys;
        public static int[] EichLinks;
        public static int[] RlischKeys;
        public static int[] RlischLinks;
        public static int[] BeischKeys;
        public static int[] BeischLinks;       
        public static int keyAreaLenght;
        public static int overflowAreaLenght;
        public static int totalLenght;
        public static int secondModValue;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;

        }
        public static void LISCH()
        {
            LischKeys = new int[CalculatingPackingFactor(lenghtofArray)];
            LischLinks = new int[CalculatingPackingFactor(lenghtofArray)];
            initializeArray(LischLinks);
            initializeArray(LischKeys);
            int currentKey;
            int lastKey = lenghtofArray;
            for (int i = 0; i < array.Length; i++)
            {
                currentKey = array[i] % lenghtofTable;
                if (LischKeys[currentKey] == -1)
                    LischKeys[currentKey] = array[i];
                else
                {
                    if (LischLinks[currentKey] == -1)
                    {
                        if (LischKeys[lastKey] == -1)
                        {
                            LischKeys[lastKey] = array[i];
                            LischLinks[currentKey] = lastKey;
                        }
                        else
                        {
                            while (LischKeys[lastKey] != -1)
                            {
                                lastKey--;
                            }
                            LischKeys[lastKey] = array[i];
                            LischLinks[currentKey] = lastKey;
                        }
                    }
                    else
                    {
                        while (LischLinks[currentKey] != -1)
                            currentKey = LischLinks[currentKey];
                        if (LischKeys[lastKey] == -1)
                        {
                            LischKeys[lastKey] = array[i];
                            LischLinks[currentKey] = lastKey;
                        }
                        else
                        {
                            while (LischKeys[lastKey] != -1)
                                lastKey--;
                            LischKeys[lastKey] = array[i];
                            LischLinks[currentKey] = lastKey;
                        }
                    }

                }
            }
        }

        public static void EISCH()
        {
            EischKeys = new int[CalculatingPackingFactor(lenghtofArray)];
            EischLinks = new int[CalculatingPackingFactor(lenghtofArray)];
            initializeArray(EischLinks);
            initializeArray(EischKeys);
            int currentKey;
            int mehmet = 0;
            int lastKey = lenghtofArray;
            for (int i = 0; i < array.Length; i++)
            {
                currentKey = array[i] % lenghtofTable;
                if (EischKeys[currentKey] == -1)
                    EischKeys[currentKey] = array[i];
                else
                {
                    if (EischLinks[currentKey] == -1)
                    {
                        if (EischKeys[lastKey] == -1)
                        {
                            EischKeys[lastKey] = array[i];
                            EischLinks[currentKey] = lastKey;
                        }
                        else
                        {
                            while (EischKeys[lastKey] != -1)
                            {
                                lastKey--;
                            }
                            EischKeys[lastKey] = array[i];
                            EischLinks[currentKey] = lastKey;
                        }
                    }
                    else
                    {
                        while (EischLinks[currentKey] != -1)

                            mehmet = EischLinks[currentKey];

                        if (EischKeys[lastKey] == -1)
                        {
                            EischKeys[lastKey] = array[i];
                            EischLinks[currentKey] = lastKey;
                            EischLinks[lastKey] = mehmet;

                        }
                        else
                        {
                            while (EischKeys[lastKey] != -1)
                                lastKey--;
                            EischKeys[lastKey] = array[i];
                            EischLinks[currentKey] = lastKey;
                            EischLinks[lastKey] = mehmet;
                        }
                    }

                }
            }

        }




        public static void EICH()
        {
            keyAreaLenght = Convert.ToInt32(CalculatingPackingFactor(lenghtofArray) * 0.86);
            overflowAreaLenght = Convert.ToInt32(CalculatingPackingFactor(lenghtofArray) * 0.14);
            totalLenght = keyAreaLenght + overflowAreaLenght;
            EichKeys = new int[CalculatingPackingFactor(totalLenght)];
            EichLinks = new int[CalculatingPackingFactor(totalLenght)];
            initializeArray(EichKeys);
            initializeArray(EichLinks);
            for (int i = 0; i < lenghtofArray; i++)
            {
                int currentKey = array[i] % keyAreaLenght; // Key MOD table size without overflow
                int lastKey = totalLenght; // Last possible position for overflow key value
                if (EichKeys[currentKey] == -1)
                { // If his home adress is available
                    EichKeys[currentKey] = array[i];
                }
                else
                { // If his home adress is not available
                    lastKey--;
                    if (EichKeys[lastKey] == -1)
                    { // If last key is possible to write key
                        EichKeys[lastKey] = array[i];
                        //EichLinks[currentKey] = lastKey;
                    }
                    else
                    {// If last key is not possible to write key, looking for a place
                        while (EichKeys[lastKey] != -1)
                        {
                            lastKey--;
                            EichKeys[lastKey] = array[i];
                        }
                        //EichLinks[currentKey] = lastKey; 
                    }
                    int temp = EichLinks[currentKey];
                    EichLinks[currentKey] = lastKey;
                    EichLinks[lastKey] = temp;
                }
            }
        }
        public static void BEISCH()
        {
            BeischKeys = new int[CalculatingPackingFactor(lenghtofArray)];
            BeischLinks = new int[CalculatingPackingFactor(lenghtofArray)];
            initializeArray(BeischKeys);
            initializeArray(BeischLinks);
            int currentKey;
            int lastKey = lenghtofArray;
            int firstKey = 0;
            Boolean LastEarly = true; // True means last , false means early
            int temp;
            for (int i = 0; i < lenghtofArray; i++)
            {
                currentKey = array[i] % lenghtofTable;
                if (BeischKeys[currentKey] == -1)
                    BeischKeys[currentKey] = array[i];
                else
                {
                    if (BeischLinks[currentKey] == -1)
                    {
                        if (LastEarly)
                        {
                            while (BeischKeys[lastKey] != -1)
                                lastKey--;
                            BeischKeys[lastKey] = array[i];
                            BeischLinks[currentKey] = lastKey;
                            LastEarly = false;
                        }
                        else
                        {
                            while (BeischKeys[firstKey] != -1)
                                firstKey++;
                            BeischKeys[firstKey] = array[i];
                            BeischLinks[currentKey] = firstKey;
                            LastEarly = true;
                        }
                    }
                    else
                    {
                        temp = BeischLinks[currentKey];
                        if (LastEarly)
                        {
                            while (BeischKeys[lastKey] != -1)
                                lastKey--;
                            BeischKeys[lastKey] = array[i];
                            BeischLinks[currentKey] = lastKey;
                            BeischLinks[lastKey] = temp;
                            LastEarly = false;
                        }
                        else
                        {
                            while (BeischKeys[firstKey] != -1)
                                firstKey++;
                            BeischKeys[firstKey] = array[i];
                            BeischLinks[currentKey] = firstKey;
                            BeischLinks[firstKey] = temp;
                            LastEarly = true;
                        }
                    }
                }
            }
        }
        public static void RLISCH()
        {
            RlischKeys = new int[CalculatingPackingFactor(lenghtofArray)];
            RlischLinks = new int[CalculatingPackingFactor(lenghtofArray)];
            initializeArray(RlischKeys);
            initializeArray(RlischLinks);
            int currentKey;
            int randomPlace = random.Next(0, lenghtofTable);
            for (int i = 0; i < lenghtofArray; i++)
            {
                currentKey = array[i] % lenghtofTable;
                if (RlischKeys[currentKey] == -1) // If his home adress is available
                    RlischKeys[currentKey] = array[i];
                else
                { // If his home adress is not available
                    if (RlischLinks[currentKey] == -1)
                    {
                        while (RlischKeys[randomPlace] != -1)
                        { // Looking for a empty place
                            randomPlace = random.Next(0, lenghtofTable);
                        }
                        RlischKeys[randomPlace] = array[i];
                        RlischLinks[currentKey] = randomPlace;
                    }
                    else
                    {
                        while (RlischLinks[currentKey] != -1)
                            currentKey = RlischLinks[currentKey];
                        while (RlischKeys[randomPlace] != -1)
                            randomPlace = random.Next(0, lenghtofTable);
                        RlischKeys[randomPlace] = array[i];
                        RlischLinks[currentKey] = randomPlace;

                    }

                    /* while (RlischLinks[randomPlace] != 0)
                     { // Looking for a empty place
                         randomPlace = random.Next(0, lenghtofTable);
                     }
                     RlischKeys[randomPlace] = array[i];
                     RlischLinks[currentKey] = randomPlace;*/

                }
            }
        }
        public void RandomKeyGenerator(int[] array, int lenght)
        {

            int i = 0;
            while (i < lenght)
            {
                int instanceofNumber = random.Next(0, 900);
                if (!array.Contains(instanceofNumber))
                {
                    array[i] = instanceofNumber;
                    listbox1.Items.Add(array[i] + "\t\t" + (array[i] % lenghtofTable) + "\t\t" + (array[i] % secondModValue));
                    listbox1.Items.Add("-----------------------------------------------------------------------------------------------");
                    i++;
                }
            }
        }
        public static void initializeArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = -1;
            }
        }
        public static int CalculatingPackingFactor(int numberofKeys)
        {
            int tmp = Convert.ToInt32(Math.Ceiling((decimal)(100 * numberofKeys) / 90));

            if (isPrime(tmp))
                return tmp;
            else
            {
                int nextPrimeNumber = tmp + 1;
                while (!isPrime(nextPrimeNumber))
                {
                    nextPrimeNumber += 1;
                }
                return nextPrimeNumber;
            }
            /*
                lenghtofTable = (100 * numberofKeys) / 95;
                int squaredNumber = (int)Math.Sqrt(tmp);

                for (int i = 2; i <= squaredNumber; i++)
                {
                    double packingFactor = Convert.ToDouble(numberofKeys) / Convert.ToDouble(tmp) * 100;
                    if (tmp % i == 0)
                    {
                        tmp++;
                        isPrime = false;
                        break;
                    }
                    else
                        isPrime = true;
                    if (packingFactor >= 95)
                        return CalculatingPackingFactor(tmp);

                }
                if (isPrime)
                    return tmp;
            */

        }
        public static Boolean isPrime(int input)
        {
            int i;
            Boolean prime = true;
            if (input == 2)
                return true;

            if (input % 2 == 0 || input <= 1)
                prime = false;

            else
            {
                for (i = 3; i <= Math.Sqrt(input); i += 2)
                {
                    if (input % i == 0)
                        prime = false;
                }
            }
            return prime;
        }

        public static int SearchforStandard(int[] arrayofKeys, int[] arrayofLinks, int value)
        {
            int probe = 1;
            int currentLink = value % lenghtofTable;
            while (arrayofKeys[currentLink] != value)
            {
                probe++;
                currentLink = arrayofLinks[currentLink];
                if (currentLink == -1)
                    return -1;
            }
            return probe;
        }

        public static int SearchfornotStandard(int[] arrayofKeys, int[] arrayofLinks, int value)
        {
            int probe = 1;
            int currentLink = value % keyAreaLenght;
            while (arrayofKeys[currentLink] != value)
            {
                probe++;
                currentLink = arrayofLinks[currentLink];
                if (currentLink == -1)
                    return -1;
            }
            return probe;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            listbox1.Items.Clear();
            button2.Enabled = true;

            try
            {
                lenghtofArray = Convert.ToInt32(numberofKeys.Text);
                array = new int[lenghtofArray];
                lenghtofTable = CalculatingPackingFactor(lenghtofArray);
                secondModValue = Convert.ToInt32(CalculatingPackingFactor(lenghtofArray) * 0.86);
                RandomKeyGenerator(array, lenghtofArray);
                TableSizeLabel.Text = Convert.ToString(lenghtofTable);
                PackingFactorLabel.Text = Convert.ToString(Math.Round((Convert.ToDouble(numberofKeys.Text) / lenghtofTable), 4));
                LISCH();               
                EICH();
                RLISCH();
                BEISCH();
                EISCH();
                KeyAreaEichLabel2.Text = Convert.ToString(keyAreaLenght);
            }
            catch
            {
                MessageBox.Show("Value is not valid");
            }

            listBox2.Items.Add(lenghtofTable);
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            listBox5.Items.Clear();
            listBox7.Items.Clear();

            for (int i = 0; i < lenghtofTable; i++)
            {

                listBox2.Items.Add(i + "\t\t" + LischKeys[i] + "\t\t" + LischLinks[i]);
                listBox2.Items.Add("-----------------------------------------------------------------------------------------------");
                if (i == totalLenght - overflowAreaLenght)
                {
                    listBox3.Items.Add("---------------------------------------------------------------------------------------------------");
                }
                listBox3.Items.Add(i + "\t\t" + EichKeys[i] + "\t\t" + EichLinks[i]);
                listBox3.Items.Add("-----------------------------------------------------------------------------------------------");
                listBox4.Items.Add(i + "\t\t" + RlischKeys[i] + "\t\t" + RlischLinks[i]);
                listBox4.Items.Add("-----------------------------------------------------------------------------------------------");
                listBox5.Items.Add(i + "\t\t" + BeischKeys[i] + "\t\t" + BeischLinks[i]);
                listBox5.Items.Add("-----------------------------------------------------------------------------------------------");
                listBox7.Items.Add(i + "\t\t" + EischKeys[i] + "\t\t" + EischLinks[i]);
                listBox7.Items.Add("-----------------------------------------------------------------------------------------------");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            LischProbeLabel.Text = Convert.ToString(SearchforStandard(LischKeys, LischLinks, Convert.ToInt32(SearchValue.Text)));
            EischProbeLabel.Text = Convert.ToString(SearchforStandard(EischKeys, EischLinks, Convert.ToInt32(SearchValue.Text)));
            EichProbeLabel.Text = Convert.ToString(SearchfornotStandard(EichKeys, EichLinks, Convert.ToInt32(SearchValue.Text)));
            RlischProbeLabel.Text = Convert.ToString(SearchforStandard(RlischKeys, RlischLinks, Convert.ToInt32(SearchValue.Text)));
            BeischProbeLabel.Text = Convert.ToString(SearchforStandard(BeischKeys, BeischLinks, Convert.ToInt32(SearchValue.Text)));
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
