using System.Security.Cryptography;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Text.RegularExpressions;

namespace alignment
{
    using System;
    using System.Diagnostics; //Çalışma zamanını hesaplamak için kullanıyoruz.
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        string filePath1 = "";
        string path = "";
        char[] path1Chars = null;
        char[] path2Chars = null;

        int[,] values = new int[6, 6];
        public Form1()
        {
            InitializeComponent();
        }
        public string fileLoad()
        {
            // OpenFileDialog ile dosyayı yüklemezsiniz, sadece dosya ile ilgili bilgileri almış olursunuz.
            OpenFileDialog dialog = new OpenFileDialog();

            //dosya seçimi için başlanğıç noktası seçiyoruz.( InitialDirectory özelliği sayesinde)
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //Environment.GetFolderPath(Environment.SpecialFolder.Desktop) yazılımı bize bilgisayardaki Masaüstü klasör ismini gönderen bir yöntemdir.

            dialog.Filter = "txt Dosyası | *.txt";
            //klasörler için filtreleme yapmaktır. Bu işlem için de seçilmesini istediğiniz dosya formatlarını virgülle ayırarak eklediğimiz yerdir.
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //FileName özelliğin dosya yolunu gösterdiğidir
                filePath1 = dialog.FileName;
                return filePath1;
            }
            else
            {
                return "Dosya Seçilmedi";
            }
        }
        public string fileRead(string filePath)
        {

            // StreamReader sınıfı dosya okuma için kullanılır
            StreamReader sr = new StreamReader(filePath);
            //metni tek seferde baştan sona kadar okur.
            path = sr.ReadToEnd();
            return path;

        }
        public int oneCalculate(int i, int j, int match, int misMatch)
        {
            int deger = 0;
            int sayi1 = i - 1;
            int sayi2 = j - 1;
            int sonuc = 0;
            for (int n = 0; n < path1Chars.Length; n++)
            {
                for (int m = 0; m < path2Chars.Length; m++)
                {
                    if (path1Chars[i-1] == path2Chars[j-1])
                    {
                        for (int l = 0; l < values.Length; l++)
                        {
                            for (int k = 0; k < values.Length; k++)
                            {
                                if (l == sayi1 && k == sayi2)
                                {
                                    deger = values[sayi1, sayi2];
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }

                        }
                        sonuc = deger + match;
                        break;

                    }
                    else
                    {
                        for (int l = 0; l < values.Length; l++)
                        {
                            for (int k = 0; k < values.Length; k++)
                            {
                                if (l == sayi1 && k == sayi2)
                                {
                                    deger = values[sayi1, sayi2];
                                    break;
                                }
                                else
                                {
                                    continue;
                                }

                            }

                        }
                        sonuc = deger + misMatch;
                        break;
                    }

                }


            }
            return sonuc;
        }

        public int twoCalculate(int i, int j, int gap)
        {
            int deger = 0;
            int sayi = i - 1;
            for (int l = 0; l < values.Length; l++)
            {
                for (int k = 0; k < values.Length; k++)
                {
                    if (l == sayi && j == k)
                    {
                        deger = values[sayi, j];
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }

            }
            int sonuc = deger + gap;
            return sonuc;
        }

        public int threeCalculate(int i, int j, int gap)
        {
            int deger = 0;
            int sayi = j - 1;

            for (int l = 0; l < values.Length; l++)
            {
                for (int k = 0; k < values.Length; k++)
                {
                    if (l == i && k == sayi)
                    {
                        deger = values[i, sayi];
                        break;
                    }
                }

            }
            int sonuc = deger + gap;

            return sonuc;
        }

        public int bigNumber(int value1, int value2, int value3)
        {
            int number = 0;

            if (value1 >= value2 && value1 >= value3)
            {
                return value1;

            }
            else if (value2 >= value1 && value2 >= value3)
            {
                return value2;
            }
            else if (value3 >= value2 && value3 >= value1)
            {
                return value3;
            }
            else
            {
                return number;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch(); // çalışma zamanı için Stopwatch nesnesini kullanıyoruz
            watch.Start(); //başlatıyoruz.

            int missMatch = -1, gap = -2, match = 1;
            int path1Lenght = 0;
            int path2Lenght = 0;

            //missMatch, gap, match değerlerini boşsa değer atıyoruz değilse girilen değeri alıyoruz.
            if (matchValue.Text == "")
            {
                match = 1;
            }
            else if (matchValue.Text != null)
            {
                match = Convert.ToInt32(misMatchValue.Text.ToString());
            }
            else if (misMatchValue.Text == "")
            {
                missMatch = -1;
            }
            else if (misMatchValue.Text != null)
            {
                missMatch = Convert.ToInt32(misMatchValue.Text.ToString());
            }
            else if (gapValue.Text == "")
            {
                gap = -2;
            }
            else if (gapValue.Text != null)
            {
                gap = Convert.ToInt32(gapValue.Text.ToString());
            }

            string path1; string path2; int sayac = 0;
            string path1Read;
            string path2Read;

            //classdan nesene oluşturuyoruz.
            Form1 file1 = new Form1();
            //sayac 2 çünkü en fazla 2 dosya seçebiliriz.
            while (sayac < 2)
            {
                if (sayac == 0)
                {
                    //ilk dosyamızı seçiyoruz.
                    path1 = file1.fileLoad();
                    //dosyamızın içini okuyoruz.
                    path1Read = file1.fileRead(path1);
                    //dosya içeriğini ekrana bastırıyoruz.
                    dna1.Text = path1Read;
                    //dosya uzunluğunu alıyoruz
                    path1Lenght = path1Read.Length;
                    //char dizisine atıyoruz
                    path1Chars = path1Read.ToCharArray();
                    sayac++;
                }
                else if (sayac == 1)
                {
                    //yurıdaki işlemleri 2 dosya içinde yapıyoruz.
                    path2 = file1.fileLoad();
                    path2Read = file1.fileRead(path2);
                    //dosya içeriğini ekrana bastırıyoruz.
                    dna2.Text = path2Read;
                    path2Lenght = path2Read.Length;
                    path2Chars = path2Read.ToCharArray();
                    sayac++;
                }

            }
            //tablo gorumunu için düzenleme yapıyoruz
            dataGridView1.ColumnCount = 1 + path1Lenght;
            dataGridView1.Columns[0].Name = " ";
            dataGridView1.Columns[1].Name = " ";
            //0.0 -> alanına 0 değerini koyuyoruz

            int value1 = 0;
            int value2 = 0;
            int value3 = 0;
            int answer = 0;
            int answer2 = 0;
            int answer3 = 0;


            for (int i = 0; i <= path1Chars.Length; i++)
            {
                for (int k = 0; k <= path2Chars.Length; k++)
                {
                    if (i == 0 && k == 0)
                    {
                        values[i, k] = answer;
                        

                    }
                    else if (i == 0 && k > 0)
                    {
                        //ilk satıra direkt gap degerini yerleştiriyoruz.
                        answer += gap;
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[0].Cells[0].Value = "0";
                        dataGridView1.Rows[i].Cells[k].Value = answer.ToString();
                        values[i, k] = answer;
                        


                    }

                    else if (i > 0 && k == 0)
                    {
                        //ilk sutuna direkt gap degerini yerleştiriyoruz.
                        answer2 += gap;
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells[k].Value = answer2.ToString();
                        values[i, k] = answer2;
                       
                       
                    }
                }


            }

            for (int i = 1; i < path1Chars.Length+1; i++)
            {
                for (int k = 1; k < path2Chars.Length+1; k++)
                {

                    value1 = oneCalculate(i, k, match, missMatch);
                    value2 = twoCalculate(i, k, gap);
                    value3 = threeCalculate(i, k, gap);
                    answer3 = bigNumber(value1, value2, value3);
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[k].Value = answer3.ToString();
                    values[i,k] = answer3;
                    listBox1.Items.Add(i);
                    listBox1.Items.Add(k);
                    listBox1.Items.Add(answer3);
                                 
                }

        }


            watch.Stop(); //durduruyoruz.
                          //geçen süresi Elapsed değişkeni tutar.
            textBox1.Text = watch.Elapsed.Seconds.ToString() + "sn";

        
    
        }   
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        }
            
        }
    


