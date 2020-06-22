using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms;


namespace VIZNaloga2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<char> abeceda = new List<char>{ 'a', 'b', 'c', 'č', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'r', 's', 'š', 't', 'u', 'v', 'z', 'ž' };
        int[] SteviloSifrirano = new int[25];
        int[] SteviloOrginal = new int[25];
        int[] RešitevPolje = new int[25];

        public MainWindow()
        {
            InitializeComponent();

            //using (var beri= new StreamReader(@"C:\Users\Tomi\Desktop\Šola Feri\2. letnik\2.semester\VARNOST IN ZAŠČITA\referencna_datoteka.txt"))
            //{
            //    OrginalnoPolje.Text = beri.ReadToEnd();
            //}

            //using (var beri = new StreamReader(@"C:\Users\Tomi\Desktop\Šola Feri\2. letnik\2.semester\VARNOST IN ZAŠČITA\sifrirana_datoteka.txt"))
            //{
            //    Sifrirana.Text = beri.ReadToEnd();
            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (OrginalnoPolje.Text.Length > 0 && Sifrirana.Text.Length>0)
            {
                TestOrginal.Clear();
                testSifrirano.Clear();
                Resitev.Clear();
                for (int i = 0; i < Sifrirana.Text.Length; i++)
                {
                    for (int j = 0; j < abeceda.Count; j++)
                    {
                        if (Sifrirana.Text[i]==abeceda[j])
                        {
                            SteviloSifrirano[j]++;
                        }
                    }
                }


                for (int i = 0; i < OrginalnoPolje.Text.Length; i++)
                {
                    for (int j = 0; j < abeceda.Count; j++)
                    {
                        if (OrginalnoPolje.Text[i] == abeceda[j])
                        {
                            SteviloOrginal[j]++;
                        }
                    }
                }

                for (int i = 0; i < abeceda.Count; i++)
                {
                    string test1orginal = abeceda[i] + ": " + SteviloOrginal[i] + "\n";
                    string test2Sifrirano = abeceda[i] + ": " + SteviloSifrirano[i] +"\n";
                    TestOrginal.Text += test1orginal;
                    testSifrirano.Text += test2Sifrirano;
                }



                var stPonovitevCrke = 0;
                foreach (var item in Sifrirana.Text)
                {
                    if (char.IsLetter(item))
                    {
                        foreach (var crka in abeceda)
                        {
                            if (crka==item)
                            {
                                var indeks=abeceda.IndexOf(item);

                                for (int i = 0; i < SteviloSifrirano.Length; i++)
                                {
                                    if (indeks==i)
                                    {
                                       stPonovitevCrke = SteviloSifrirano[i];
                                    }
                                }

                                char drugaCrka = 'a';

                                for (int i = 0; i < SteviloOrginal.Length; i++)
                                {
                                    if (stPonovitevCrke==SteviloOrginal[i])
                                    {
                                        drugaCrka = abeceda[i];
                                        break;
                                    }
                                }
                                Resitev.Text = Resitev.Text + drugaCrka;

                            }
                            
                        }
                    }
                    else
                    {
                        Resitev.Text = Resitev.Text + item;
                    }
                }


            }


            for (int i = 0; i < Resitev.Text.Length; i++)
            {
                for (int j = 0; j < abeceda.Count; j++)
                {
                    if (Resitev.Text[i] == abeceda[j])
                    {
                        RešitevPolje[j]++;
                    }
                }
            }

            GrafOrginal.Visibility = Visibility.Visible;
            GrafSifriran.Visibility = Visibility.Visible;

            GrafKonecButn.Visibility = Visibility.Visible;
        }

        private void RocnaMenjava_Click(object sender, RoutedEventArgs e)
        {
            var besedilo = "";
            var primerjaj = Resitev.Text;
            var primerjaj2 = besedilo;

            if (RocnaMenjava1.Text!=null || RocnaMenjava2.Text!=null)
            {
                char iz=RocnaMenjava1.Text[0];
                char v = RocnaMenjava2.Text[0];

                foreach (var item in Resitev.Text)
                {
                    if (item == iz)
                    {
                        besedilo = besedilo + v;
                    }
                    else if (item == v)
                    {
                        besedilo= besedilo+ iz;
                    }
                    else
                    {
                        besedilo = besedilo+ item;
                    }
                }

                Resitev.Clear();

                Resitev.Text = besedilo;


            }
            else
            {
                MessageBox.Show("Napaka!");
            }
        }
        
        private void ShraniBesedilo_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Text file (*.txt)|*.txt|C# file (*.cs)|*.cs";
            if (saveDialog.ShowDialog()==true)
            {
                File.WriteAllText(saveDialog.FileName,Resitev.Text);
            }
        }

        private void OdpriOrginalno_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog()==true)
            {
                OrginalnoPolje.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void OdpriSifrirano_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Sifrirana.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }




        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //graf koncani

            //Dictionary char int
            GrafKonec.Visibility = Visibility.Visible;
            ZapriGrafbtn.Visibility = Visibility.Visible;


            a.Value = RešitevPolje[0];
            b.Value = RešitevPolje[1];
            c.Value = RešitevPolje[2];
            č.Value = RešitevPolje[2];
            d.Value = RešitevPolje[3];
            ee.Value = RešitevPolje[4];
            f.Value = RešitevPolje[5];
            g.Value = RešitevPolje[6];
            h.Value = RešitevPolje[7];
            i.Value = RešitevPolje[8];
            j.Value = RešitevPolje[9];
            k.Value = RešitevPolje[10];
            l.Value = RešitevPolje[11];
            m.Value = RešitevPolje[12];
            n.Value = RešitevPolje[13];
            o.Value = RešitevPolje[14];
            p.Value = RešitevPolje[15];
            r.Value = RešitevPolje[16];
            s.Value = RešitevPolje[17];
            š.Value = RešitevPolje[18];
            t.Value = RešitevPolje[19];
            u.Value = RešitevPolje[20];
            v.Value = RešitevPolje[21];
            z.Value = RešitevPolje[22];
            ž.Value = RešitevPolje[23];


            ZapriGrafbtn.Visibility = Visibility.Visible;



           

        }

        private void GrafOrginal_Click(object sender, RoutedEventArgs e)
        {
            GrafOrginal.Visibility = Visibility.Visible;
            GrafKonec.Visibility = Visibility.Visible;

            a.Value = SteviloOrginal[0];
            b.Value = SteviloOrginal[1];
            c.Value = SteviloOrginal[2];
            č.Value = SteviloOrginal[2];
            d.Value = SteviloOrginal[3];
            ee.Value = SteviloOrginal[4];
            f.Value = SteviloOrginal[5];
            g.Value = SteviloOrginal[6];
            h.Value = SteviloOrginal[7];
            i.Value = SteviloOrginal[8];
            j.Value = SteviloOrginal[9];
            k.Value = SteviloOrginal[10];
            l.Value = SteviloOrginal[11];
            m.Value = SteviloOrginal[12];
            n.Value = SteviloOrginal[13];
            o.Value = SteviloOrginal[14];
            p.Value = SteviloOrginal[15];
            r.Value = SteviloOrginal[16];
            s.Value = SteviloOrginal[17];
            š.Value = SteviloOrginal[18];
            t.Value = SteviloOrginal[19];
            u.Value = SteviloOrginal[20];
            v.Value = SteviloOrginal[21];
            z.Value = SteviloOrginal[22];
            ž.Value = SteviloOrginal[23];
            ZapriGrafbtn.Visibility = Visibility.Visible;

        }

        private void GrafSifriran_Click(object sender, RoutedEventArgs e)
        {
            GrafSifriran.Visibility = Visibility.Visible;
            GrafKonec.Visibility = Visibility.Visible;

            a.Value = SteviloSifrirano[0];
            b.Value = SteviloSifrirano[1];
            c.Value = SteviloSifrirano[2];
            č.Value = SteviloSifrirano[2];
            d.Value = SteviloSifrirano[3];
            ee.Value = SteviloSifrirano[4];
            f.Value = SteviloSifrirano[5];
            g.Value = SteviloSifrirano[6];
            h.Value = SteviloSifrirano[7];
            i.Value = SteviloSifrirano[8];
            j.Value = SteviloSifrirano[9];
            k.Value = SteviloSifrirano[10];
            l.Value = SteviloSifrirano[11];
            m.Value = SteviloSifrirano[12];
            n.Value = SteviloSifrirano[13];
            o.Value = SteviloSifrirano[14];
            p.Value = SteviloSifrirano[15];
            r.Value = SteviloSifrirano[16];
            s.Value = SteviloSifrirano[17];
            š.Value = SteviloSifrirano[18];
            t.Value = SteviloSifrirano[19];
            u.Value = SteviloSifrirano[20];
            v.Value = SteviloSifrirano[21];
            z.Value = SteviloSifrirano[22];
            ž.Value = SteviloSifrirano[23];

            ZapriGrafbtn.Visibility = Visibility.Visible;
        }

        private void ZapriGrafbtn_Click(object sender, RoutedEventArgs e)
        {
            ZapriGrafbtn.Visibility = Visibility.Hidden;
            GrafKonec.Visibility = Visibility.Hidden;
        }
    }
}
