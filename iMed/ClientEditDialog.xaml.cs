﻿using iMedShared;
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
using System.Windows.Shapes;

namespace iMed
{
    /// <summary>
    /// Interaction logic for MyWindow.xaml
    /// </summary>
    public partial class ClientEditDialog:
                DialogBase
    {
        /// <summary>
        /// 
        /// </summary>
        public ClientEditDialog( Object data )
        {
            InitializeComponent();

            this.DataContext = data;
        }
    }
}
