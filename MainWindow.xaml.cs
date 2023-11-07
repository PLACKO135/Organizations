﻿using ConsoleAppOrgBig;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace wpforganizations
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Organization> szervezetek = new List<Organization>();

        private void Betoltes(string filename)
        {
            foreach (var sor in File.ReadLines(filename).Skip(1))
            {
                szervezetek.Add(new Organization(sor.Split(';')));
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            Betoltes("C:\\Users\\papp.laszlo.levente\\Desktop\\Papp László Levente\\2023-11-07\\wpforganizations\\wpforganizations\\source\\organizations-100000.csv");
            dgAdatok.ItemsSource = szervezetek;
            cborszag.ItemsSource = szervezetek.Select(x => x.Country).OrderBy(x => x).Distinct().ToList();
            cbEv.ItemsSource = szervezetek.Select(x => x.Founded).OrderBy(x => x).Distinct().ToList();
            labTotalEmpl.Content = szervezetek.Sum(x => x.EmployeesNumber);
        }



        private void dgAdatok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAdatok.SelectedItem is Organization)
            {
                Organization valasztottObjektum = dgAdatok.SelectedItem as Organization;
                labID.Content= valasztottObjektum.Id.ToString();
                labWEB.Content = valasztottObjektum.Website;
                labISM.Content = valasztottObjektum.Description;

            }
        }

        
            private void cborszag_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
            var szurtLista = szervezetek.Where(x => x.Country == cborszag.SelectedItem.ToString()).ToList();
            dgAdatok.ItemsSource = szurtLista;
            labTotalEmpl.Content = szurtLista.Sum(x => x.EmployeesNumber);
            }

            private void cbEv_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
            var szurtLista = szervezetek.Where(x => x.Founded == int.Parse( cbEv.SelectedItem.ToString())).ToList();
            dgAdatok.ItemsSource = szurtLista;
            labTotalEmpl.Content = szurtLista.Sum(x => x.EmployeesNumber).ToString();
        }
    }

} 

